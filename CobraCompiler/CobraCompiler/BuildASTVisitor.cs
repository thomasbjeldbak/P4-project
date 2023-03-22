using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CobraCompiler;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Xml.Linq;
using static Antlr4.Runtime.Atn.SemanticContext;
using static ASTNodes;

internal class BuildASTVisitor : ExprParserBaseVisitor<ASTNode>
{
    private string _indent;

    #region PrettyPrinter Methods
    private void prettyPrint(string nodeName, ParserRuleContext context)
    {
        Console.WriteLine($"{_indent}{nodeName}");
    }

    private void incrIndent()
    {
        _indent += "\t";
    }

    private void decrIndent()
    {
        _indent = _indent.Substring(0, _indent.Length - 1);
    }

    #endregion


    //program: cmds;
    public override ASTNode VisitProgram([NotNull] ExprParser.ProgramContext context)
    {
        var cmds = context.cmds();

        var programNode = new ProgramNode();

        prettyPrint("ProgramNode", context);
        incrIndent();

        if (cmds != null)
        {
            var blockNode = (BlockNode)Visit(cmds);
            programNode.Commands = blockNode.Commands;
        }

        return programNode;
    }

    //cmds: cmd cmds | /*epsilon*/;
    public override ASTNode VisitCmds([NotNull] ExprParser.CmdsContext context)
    {
        var cmd = context.cmd();
        var cmds = context.cmds();

        var outputNode = new BlockNode();
        outputNode.Commands = new List<CommandNode>();

        var command = (CommandNode)Visit(cmd);
        outputNode.Commands.Add(command);

        if (cmds != null && cmds.ChildCount > 0)
        {
            var cmdsNode = (BlockNode)Visit(cmds);
            outputNode.Commands.AddRange(cmdsNode.Commands);
        }

        return outputNode;
    }

    //cmd: stmt | dcl;
    public override ASTNode VisitCmd([NotNull] ExprParser.CmdContext context)
    {
        var stmt = context.stmt();
        var dcl = context.dcl();

        if (stmt != null)
        {
            return Visit(stmt);
        }
        else if (dcl != null)
        {
            return Visit(dcl);
        }
        else
        {
            throw new Exception("Invalid command");
        }
    }

    //dcl: type ID ass SEMI;
    public override ASTNode VisitDcl([NotNull] ExprParser.DclContext context)
    {
        var type = context.type();
        var ID = context.ID();
        var ass = context.ass();
        var SEMI = context.SEMI();

        var declarationNode = new DeclarationNode();

        prettyPrint("DeclarationNode", context);
        incrIndent();

        if ((type != null) &&
            (ID != null) &&
            (ass != null) &&
            (SEMI != null))
        {
            var identifier = new IdentifierNode();
            prettyPrint("IdentifierNode", context);
            incrIndent();

            identifier.Name = ID.ToString();
            identifier.Type = (TypeNode)Visit(type);

            if (ass.ChildCount > 0)
            {
                declarationNode.Expression = (ExpressionNode)Visit(ass);
            }

            declarationNode.Identifier = identifier;
        }

        decrIndent();
        return declarationNode;
    }

    //ass: ASSIGN expr | /*epsilon*/;
    public override ASTNode VisitAss([NotNull] ExprParser.AssContext context)
    {
        return Visit(context.expr());
    }

    //stmt: ID ASSIGN expr SEMI | ctrlStrct | listStmt SEMI | funcDef | funcCall SEMI;
    public override ASTNode VisitStmt([NotNull] ExprParser.StmtContext context)
    {
        var ID = context.ID();
        var ASSIGN = context.ASSIGN();
        var expr = context.expr();
        var SEMI = context.SEMI();
        var ctrlStrct = context.ctrlStrct();
        var listStmt = context.listStmt();
        var funcDef = context.funcDef();
        var funcCall = context.funcCall();

        ASTNode outputNode = null;

        if ((ID != null) && 
            (ASSIGN != null) &&
            (expr != null && expr.ChildCount > 0) &&
            SEMI != null)
        {
            var assignNode = new AssignNode();
            prettyPrint("AssignNode", context);
            incrIndent();

            var identifier = new IdentifierNode();
            prettyPrint("IdentifierNode", context);
            identifier.Name = ID.ToString();

            var expression = (ExpressionNode)Visit(expr);
            assignNode.Expression = expression;
            assignNode.Identifier = identifier;
            outputNode = assignNode;
        }
        else if (ctrlStrct != null && ctrlStrct.ChildCount > 0)
        {
            outputNode = Visit(ctrlStrct);
        }
        else if (listStmt != null && listStmt.ChildCount > 0)
        {
            outputNode = Visit(listStmt);
        }
        else if (funcDef != null && funcDef.ChildCount > 0)
        {
            Visit(funcDef);
        }
        else if ((funcCall != null && funcCall.ChildCount > 0) &&
                 (SEMI != null))
        {
            Visit(funcCall);
        }
        else
            throw new Exception();

        return outputNode;
    }

    //expr: logicOr oprOr;
    public override ASTNode VisitExpr([NotNull] ExprParser.ExprContext context)
    {
        var logicOr = context.logicOr();
        var oprOr = context.oprOr();

        if (oprOr != null && oprOr.ChildCount > 0)
        {
            var expressionNode = (InfixExpressionNode)Visit(oprOr);

            incrIndent();
            expressionNode.Left = (ExpressionNode)Visit(logicOr);
            decrIndent();

            return expressionNode;
        }
        else
            return Visit(logicOr);
    }

    //oprOr: OR logicOr oprOr | /*epsilon*/;
    public override ASTNode VisitOprOr([NotNull] ExprParser.OprOrContext context)
    {
        var oprOr = context.oprOr();
        var logicOr = context.logicOr();
        var OR = context.OR();

        InfixExpressionNode outputNode;

        if (OR != null)
        {
            prettyPrint("OrNode", context);
            outputNode = new OrNode();
        }
        else
            throw new Exception();

        incrIndent();

        if (oprOr != null && oprOr.ChildCount > 0)
        {
            InfixExpressionNode rightSide;
            rightSide = (InfixExpressionNode)Visit(oprOr);
            rightSide.Left = (ExpressionNode)Visit(logicOr);

            outputNode.Right = rightSide;

            decrIndent();
            return outputNode;
        }
        else
        {
            outputNode.Right = (ExpressionNode)Visit(logicOr);

            decrIndent();
            return outputNode;
        }
    }

    //logicOr: logicAnd oprAnd;
    public override ASTNode VisitLogicOr([NotNull] ExprParser.LogicOrContext context)
    {
        var logicAnd = context.logicAnd();
        var oprAnd = context.oprAnd();

        if (oprAnd != null && oprAnd.ChildCount > 0)
        {
            var expressionNode = (InfixExpressionNode)Visit(oprAnd);

            incrIndent();
            expressionNode.Left = (ExpressionNode)Visit(logicAnd);
            decrIndent();

            return expressionNode;
        }
        else
            return Visit(logicAnd);
    }

    //oprAnd: AND logicAnd oprAnd | /*epsilon*/; 
    public override ASTNode VisitOprAnd([NotNull] ExprParser.OprAndContext context)
    {
        var oprAnd = context.oprAnd();
        var logicAnd = context.logicAnd();
        var AND = context.AND();

        InfixExpressionNode outputNode;

        //Check if it's a addition or subtraction
        if (AND != null)
        {
            prettyPrint("AndNode", context);
            outputNode = new AndNode();
        }
        else
            throw new Exception();

        incrIndent();

        /*If oprTerm != null, that must mean that the infixExpressions right-side has another
        infixExpression. We Return an Infix which has a right-side infix*/
        if (oprAnd != null && oprAnd.ChildCount > 0)
        {
            InfixExpressionNode rightSide;
            rightSide = (InfixExpressionNode)Visit(oprAnd);
            rightSide.Left = (ExpressionNode)Visit(logicAnd);

            outputNode.Right = rightSide;

            decrIndent();
            return outputNode;
        }
        else
        {
            outputNode.Right = (ExpressionNode)Visit(logicAnd);

            decrIndent();
            return outputNode;
        }
    }

    //logicAnd: equal oprEql;
    public override ASTNode VisitLogicAnd([NotNull] ExprParser.LogicAndContext context)
    {
        var equal = context.equal();
        var oprEql = context.oprEql();

        if (oprEql != null && oprEql.ChildCount > 0)
        {
            var expressionNode = (InfixExpressionNode)Visit(oprEql);

            incrIndent();
            expressionNode.Left = (ExpressionNode)Visit(equal);
            decrIndent();

            return expressionNode;
        }
        else
            return Visit(equal);
    }

    //oprEql: EQUAL equal oprEql | NOT equal oprEql | /*epsilon*/;
    public override ASTNode VisitOprEql([NotNull] ExprParser.OprEqlContext context)
    {
        var oprEql = context.oprEql();
        var equal = context.equal();
        var EQUAL = context.EQUAL();
        var NOT = context.NOT();

        InfixExpressionNode outputNode;

        if (EQUAL != null)
        {
            prettyPrint("EqualNode", context);
            outputNode = new EqualNode();
        }
        else if (NOT != null)
        {
            prettyPrint("NotEqualNode", context);
            outputNode = new NotEqualNode();
        }
        else
            throw new Exception();

        incrIndent();

        if (oprEql != null && oprEql.ChildCount > 0)
        {
            InfixExpressionNode rightSide;
            rightSide = (InfixExpressionNode)Visit(oprEql);
            rightSide.Left = (ExpressionNode)Visit(equal);

            outputNode.Right = rightSide;

            decrIndent();
            return outputNode;
        }
        else
        {
            outputNode.Right = (ExpressionNode)Visit(equal);

            decrIndent();
            return outputNode;
        }
    }

    //equal: bool oprBool;
    public override ASTNode VisitEqual([NotNull] ExprParser.EqualContext context)
    {
        var @bool = context.@bool();
        var oprBool = context.oprBool();

        if (oprBool != null && oprBool.ChildCount > 0)
        {
            var expressionNode = (InfixExpressionNode)Visit(oprBool);

            incrIndent();
            expressionNode.Left = (ExpressionNode)Visit(@bool);
            decrIndent();

            return expressionNode;
        }
        else
            return Visit(@bool);
    }

    //oprBool: GREAT bool oprBool | LESS bool oprBool | GREATEQL bool oprBool| LESSEQL bool oprBool | /*epsilon*/;
    public override ASTNode VisitOprBool([NotNull] ExprParser.OprBoolContext context)
    {
        var oprBool = context.oprBool();
        var @bool = context.@bool();
        var GREAT = context.GREAT();
        var GREATEQL = context.GREATEQL();
        var LESS = context.LESS();
        var LESSEQL = context.LESSEQL();

        InfixExpressionNode outputNode;

        if (GREAT != null)
        {
            prettyPrint("GreaterNode", context);
            outputNode = new GreaterNode();
        }
        else if (GREATEQL != null)
        {
            prettyPrint("GreaterEqualNode", context);
            outputNode = new GreaterEqualNode();
        }
        else if (LESS != null)
        {
            prettyPrint("LessNode", context);
            outputNode = new LessNode();
        }
        else if (LESSEQL != null)
        {
            prettyPrint("LessEqualNode", context);
            outputNode = new LessEqualNode();
        }
        else
            throw new Exception();

        incrIndent();

        if (oprBool != null && oprBool.ChildCount > 0)
        {
            InfixExpressionNode rightSide;
            rightSide = (InfixExpressionNode)Visit(oprBool);
            rightSide.Left = (ExpressionNode)Visit(@bool);

            outputNode.Right = rightSide;

            decrIndent();
            return outputNode;
        }
        else
        {
            outputNode.Right = (ExpressionNode)Visit(@bool);

            decrIndent();
            return outputNode;
        }
    }

    //bool: term oprExpr;
    public override ASTNode VisitBool([NotNull] ExprParser.BoolContext context)
    {
        var term = context.term();
        var oprExpr = context.oprExpr();

        if (oprExpr != null && oprExpr.ChildCount > 0)
        {
            var expressionNode = (InfixExpressionNode)Visit(oprExpr);

            incrIndent();
            expressionNode.Left = (ExpressionNode)Visit(term);
            decrIndent();

            return expressionNode;
        }
        else
            return Visit(term);
    }

    //oprExpr: ADD term oprExpr | SUB term oprExpr | /*epsilon*/;
    public override ASTNode VisitOprExpr([NotNull] ExprParser.OprExprContext context)
    {
        var oprExpr = context.oprExpr();
        var term = context.term();
        var ADD = context.ADD();
        var SUB = context.SUB();

        InfixExpressionNode outputNode;

        if (ADD != null)
        {
            prettyPrint("AdditionNode", context);
            outputNode = new AdditionNode();
        }
        else if (SUB != null)
        {
            prettyPrint("SubtractionNode", context);
            outputNode = new SubtractionNode();
        }
        else
            throw new Exception();

        incrIndent();

        if (oprExpr != null && oprExpr.ChildCount > 0)
        {
            InfixExpressionNode rightSide;
            rightSide = (InfixExpressionNode)Visit(oprExpr);
            rightSide.Left = (ExpressionNode)Visit(term);

            outputNode.Right = rightSide;

            decrIndent();
            return outputNode;
        }
        else
        {
            outputNode.Right = (ExpressionNode)Visit(term);

            decrIndent();
            return outputNode;
        }
    }

    //term: factor oprTerm;
    public override ASTNode VisitTerm([NotNull] ExprParser.TermContext context)
    {
        var factor = context.factor();
        var oprTerm = context.oprTerm();

        if (oprTerm != null && oprTerm.ChildCount > 0)
        {
            var expressionNode = (InfixExpressionNode)Visit(oprTerm);
            
            incrIndent();
            expressionNode.Left = (ExpressionNode)Visit(factor);
            decrIndent();

            return expressionNode;
        }
        else
            return Visit(factor);
    }

    //oprTerm: MUL factor oprTerm | DIV factor oprTerm | /*epsilon*/; 
    public override ASTNode VisitOprTerm([NotNull] ExprParser.OprTermContext context)
    {
        var oprTerm = context.oprTerm();
        var factor = context.factor();
        var MUL = context.MUL();
        var DIV = context.DIV();

        InfixExpressionNode outputNode;

        if (MUL != null)
        {
            prettyPrint("MultiplicationNode", context);
            outputNode = new MultiplicationNode();
        }
        else if (DIV != null)
        {
            prettyPrint("DivisionNode", context);
            outputNode = new DivisionNode();
        }
        else
            throw new Exception();

        incrIndent();

        if (oprTerm != null && oprTerm.ChildCount > 0)
        {
            InfixExpressionNode rightSide;
            rightSide = (InfixExpressionNode)Visit(oprTerm);
            rightSide.Left = (ExpressionNode)Visit(factor);

            outputNode.Right = rightSide;

            decrIndent();
            return outputNode;
        }
        else
        {
            outputNode.Right = (ExpressionNode)Visit(factor);

            decrIndent();
            return outputNode;
        }
    }

    //factor: LPAREN expr RPAREN | INT | STR | ID | boolean;
    public override ASTNode VisitFactor([NotNull] ExprParser.FactorContext context)
    {
        var expression = new ExpressionNode();

        var INT = context.INT();
        var ID = context.ID();
        var LPAREN = context.LPAREN();
        var RPAREN = context.RPAREN();
        var expr = context.expr();
        var boolean = context.boolean();

        if (INT != null)
        {
            prettyPrint("ExpressionNode", context);
            expression.Value = int.Parse(INT.ToString());
        }
        else if (ID != null)
        {
            prettyPrint("ExpressionNode", context);
            expression.Value = ID.ToString();
        }
        else if ((LPAREN != null) && 
                (expr != null && expr.ChildCount > 0) &&
                (RPAREN != null))
        {
            expression = (ExpressionNode)Visit(expr);
        }
        else if (boolean != null)
        {
            prettyPrint("ExpressionNode", context);
            expression.Value = bool.Parse(boolean.ToString());
        }

        return expression;
    }

    //block: LCURLY cmds RCURLY;
    public override ASTNode VisitBlock([NotNull] ExprParser.BlockContext context)
    {
        var LCURLY = context.LCURLY();
        var cmds = context.cmds();
        var RCURLY = context.RCURLY();

        var outputNode = new BlockNode();

        if ((LCURLY != null) &&
            (cmds != null) &&
            (RCURLY != null))
        {
            prettyPrint("BlockNode", context);
            incrIndent();

            if (cmds.ChildCount > 0)
                outputNode = (BlockNode)Visit(cmds);
        }
        else
            throw new Exception();

        decrIndent();
        return outputNode;
    }

    //ctrlStrct: ifStmt | loop;
    public override ASTNode VisitCtrlStrct([NotNull] ExprParser.CtrlStrctContext context)
    {
        var ifStmt = context.ifStmt();
        var loop = context.loop();

        if (ifStmt != null && ifStmt.ChildCount > 0)
        {
            return Visit(ifStmt);
        }
        else if (loop != null && loop.ChildCount > 0)
        {
            return Visit(loop);
        }
        else
            throw new Exception();
    }

    //ifStmt: IF LPAREN expr RPAREN block elseIfStmt; 
    public override ASTNode VisitIfStmt([NotNull] ExprParser.IfStmtContext context)
    {
        var IF = context.IF();
        var LPAREN = context.LPAREN();
        var expr = context.expr();
        var RPAREN = context.RPAREN();
        var block = context.block();
        var elseIfStmt = context.elseIfStmt();

        var outputNode = new IfNode();

        if ((IF != null) &&
            (LPAREN != null) &&
            (expr != null && expr.ChildCount > 0) &&
            (RPAREN != null))
        {
            prettyPrint("IfNode", context);
            incrIndent();

            outputNode.Condition = (ExpressionNode)Visit(expr);
            outputNode.Block = (BlockNode)Visit(block);

            if (elseIfStmt != null && elseIfStmt.ChildCount > 0)
            {
                var ifNode = (IfNode)Visit(elseIfStmt);
                outputNode.ElseIfs = ifNode.ElseIfs;
            }
        }
        else
            throw new Exception();
        
        decrIndent();
        return outputNode;
    }

    //ElseIfStmt: ELSE IF LPAREN expr RPAREN block elseIfStmt | else | /*epsilon*/; 
    public override ASTNode VisitElseIfStmt([NotNull] ExprParser.ElseIfStmtContext context)
    {
        var ELSE = context.ELSE();
        var IF = context.IF();
        var LPAREN = context.LPAREN();
        var expr = context.expr();
        var RPAREN = context.RPAREN();
        var block = context.block();
        var elseIfStmt = context.elseIfStmt();
        var @else = context.@else();

        var outputNode = new IfNode();
        outputNode.ElseIfs = new List<ElseNode>();

        var elseIfNode = new ElseIfNode();
        var elseNode = new ElseNode();

        if (@else != null && @else.ChildCount > 0)
        {
            prettyPrint("ElseNode", context);
            incrIndent();

            elseNode = (ElseNode)Visit(@else);
            outputNode.ElseIfs.Add(elseNode);
            decrIndent();
        }
        else if ((ELSE != null) &&
            (IF != null) &&
            (LPAREN != null) &&
            (expr != null && expr.ChildCount > 0) &&
            (RPAREN != null))
        {
            prettyPrint("ElseIfNode", context);
            incrIndent();

            elseIfNode.Condition = (ExpressionNode)Visit(expr);
            elseIfNode.Block = (BlockNode)Visit(block);
            outputNode.ElseIfs.Add(elseIfNode);
            
            if (elseIfStmt != null && elseIfStmt.ChildCount > 0)
            {
                decrIndent();
                var ifNode = (IfNode)Visit(elseIfStmt);
                outputNode.ElseIfs.AddRange(ifNode.ElseIfs);
            }
        }

        return outputNode;
    }

    //else: ELSE block | /*epsilon*/;
    public override ASTNode VisitElse([NotNull] ExprParser.ElseContext context)
    {
        var ELSE = context.ELSE();
        var block = context.block();

        var outputNode = new ElseNode();

        if ((ELSE != null) &&
            (block != null))
        {
            outputNode.Block = (BlockNode)Visit(block);
        }
        else
            throw new Exception();

        return outputNode;
    }

    //loop: REPEAT loops;
    public override ASTNode VisitLoop([NotNull] ExprParser.LoopContext context)
    {
        var REPEAT = context.REPEAT();
        var loops = context.loops();

        if ((REPEAT != null) &&
            (loops != null) && loops.ChildCount > 0)
        {
            return Visit(loops);
        }
        else
            throw new Exception();
    }

    //loops: loopStmt | whileStmt | foreachStmt;
    public override ASTNode VisitLoops([NotNull] ExprParser.LoopsContext context)
    {
        var loopStmt = context.loopStmt();
        var whileStmt = context.whileStmt();
        var foreachStmt = context.foreachStmt();

        if (loopStmt != null)
        {
            return Visit(loopStmt);
        }
        else if (whileStmt != null)
        {
            return Visit(whileStmt);
        }
        else if (foreachStmt != null)
        {
            return Visit(foreachStmt);
        }
        else
            throw new Exception();
    }

    //loopStmt: LPAREN expr RPAREN TIMES block;
    public override ASTNode VisitLoopStmt([NotNull] ExprParser.LoopStmtContext context)
    {
        var LPAREN = context.LPAREN();
        var expr = context.expr();
        var RPAREN = context.RPAREN();
        var TIMES = context.TIMES();
        var block = context.block();

        var outputNode = new RepeatNode();

        prettyPrint("RepeatNode", context);
        incrIndent();

        if ((LPAREN != null) &&
            (expr != null) &&
            (RPAREN != null) &&
            (TIMES != null) &&
            (block != null))
        {
            outputNode.Block = (BlockNode)Visit(block);
            outputNode.Expression = (ExpressionNode)Visit(expr); 
        }
        else
            throw new Exception();

        decrIndent();
        return outputNode;
    }

    //whileStmt: WHILE LPAREN expr RPAREN block;
    public override ASTNode VisitWhileStmt([NotNull] ExprParser.WhileStmtContext context)
    {
        var WHILE = context.WHILE();
        var LPAREN = context.LPAREN();
        var expr = context.expr();
        var RPAREN = context.RPAREN();
        var block = context.block();

        var outputNode = new WhileNode();

        prettyPrint("WhileNode", context);
        incrIndent();

        if ((WHILE != null) &&
            (LPAREN != null) &&
            (expr != null) &&
            (RPAREN != null) &&
            (block != null))
        {
            outputNode.Condition = (ExpressionNode)Visit(expr);
            outputNode.Block = (BlockNode)Visit(block);
        }
        else
            throw new Exception();

        decrIndent();
        return outputNode;
    }

    //foreachStmt: FOREACH LPAREN type ID IN ID RPAREN block;
    public override ASTNode VisitForeachStmt([NotNull] ExprParser.ForeachStmtContext context)
    {
        var FOREACH = context.FOREACH();
        var LPAREN = context.LPAREN();
        var type = context.type();
        var ID = context.ID(0);
        var IN = context.IN();
        var ID2 = context.ID(1);
        var RPAREN = context.RPAREN();
        var block = context.block();

        var outputNode = new ForeachNode();

        prettyPrint("ForeachNode", context);
        incrIndent();

        if ((FOREACH != null) &&
            (LPAREN != null) &&
            (type != null) &&
            (ID != null) &&
            (IN != null) &&
            (ID2 != null) &&
            (RPAREN != null) &&
            (block != null))
        {
            prettyPrint("IdentifierNode", context);
            var localVar = new IdentifierNode();
            localVar.Type = (TypeNode)Visit(type);
            localVar.Name = ID.ToString();

            prettyPrint("IdentifierNode", context);
            var identifierNode = new IdentifierNode();
            identifierNode.Type = new ListNode();
            identifierNode.Name = ID2.ToString();

            outputNode.List = identifierNode;
            outputNode.LocalVariable = localVar;
            outputNode.Block = (BlockNode)Visit(block);
        }
        else
            throw new Exception();
        decrIndent();
        return outputNode;
    }

    //type: BOOL | TEXT | NUM | LIST LPAREN type RPAREN;
    public override ASTNode VisitType([NotNull] ExprParser.TypeContext context)
    {
        var BOOl = context.BOOL();
        var TEXT = context.TEXT();
        var NUM = context.NUM();
        var LIST = context.LIST();
        var LPAREN = context.LPAREN();
        var type = context.type();
        var RPAREN = context.RPAREN();

        if (BOOl != null)
        {
            prettyPrint("BooleanNode",context);
            var outputNode = new BooleanNode();
            decrIndent();
            return outputNode;
        }
        else if (TEXT != null)
        {
            prettyPrint("TextNode", context);
            var outputNode = new TextNode();
            decrIndent();
            return outputNode;
        }
        else if (NUM != null)
        {
            prettyPrint("NumberNode", context);
            var outputNode = new NumberNode();
            decrIndent();
            return outputNode;
        }
        else if ((LIST != null) &&
                 (LPAREN != null) &&
                 (type != null) &&
                 (RPAREN != null))
        {
            prettyPrint("ListNode", context);
            var outputNode = new ListNode();
            decrIndent();
            return outputNode;

        }
        else
            throw new Exception();
    }

    //listStmt: ID COLON listOpr;
    public override ASTNode VisitListStmt([NotNull] ExprParser.ListStmtContext context)
    {
        var ID = context.ID();
        var COLON = context.COLON();
        var listOpr = context.listOpr();

        ListOperationNode outputNode;

        if (ID != null && COLON != null && listOpr != null)
        {
            var identifierNode = new IdentifierNode();
            identifierNode.Name = ID.ToString();
            identifierNode.Type = new ListNode();

            outputNode = (ListOperationNode)Visit(listOpr);
            outputNode.Identifier = identifierNode;

            incrIndent();
            prettyPrint("IdentifierNode", context);
            decrIndent();
        }
        else
            throw new Exception();

        return outputNode;
    }

    //listOpr: LISTADD LPAREN expr RPAREN | LISTDEL LPAREN expr RPAREN | LISTIDXOF LPAREN expr RPAREN | LISTVALOF LPAREN expr RPAREN;
    public override ASTNode VisitListOpr([NotNull] ExprParser.ListOprContext context)
    {
        var LISTADD = context.LISTADD();
        var LPAREN = context.LPAREN();
        var expr = context.expr();
        var RPAREN = context.RPAREN();
        var LISTDEL = context.LISTDEL();
        var LISTIDXOF = context.LISTIDXOF();
        var LISTVALOF = context.LISTVALOF();

        ListOperationNode outputNode;

        if (LPAREN != null && expr != null && RPAREN != null)
        {
            if (LISTADD != null)
            {
                prettyPrint("ListAddNode", context);
                incrIndent();

                outputNode = new ListAddNode();
                outputNode.Expression = (ExpressionNode)Visit(expr);
            }
            else if (LISTDEL != null)
            {
                prettyPrint("ListDeleteNode", context);
                incrIndent();

                outputNode = new ListDeleteNode();
                outputNode.Expression = (ExpressionNode)Visit(expr);
            }
            else if (LISTIDXOF != null)
            {
                prettyPrint("ListIndexOfNode", context);
                incrIndent();

                outputNode = new ListIndexOfNode();
                outputNode.Expression = (ExpressionNode)Visit(expr);
            }
            else if (LISTVALOF != null)
            {
                prettyPrint("ListValueOfNode", context);
                incrIndent();

                outputNode = new ListValueOfNode();
                outputNode.Expression = (ExpressionNode)Visit(expr);
            }
            else
                throw new Exception();
        }
        else
            throw new Exception();

        decrIndent();
        return outputNode;
            
            
    }
}
