using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CobraCompiler;
using System;
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
        Console.WriteLine($"{_indent}{nodeName}: {context.GetText()}");
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

        if (cmds != null && cmds.ChildCount > 0)
        {
            var cmdsNode = (BlockNode)Visit(cmds);
            outputNode.Commands.AddRange(cmdsNode.Commands);
        }

        var command = (CommandNode)Visit(cmd);
        outputNode.Commands.Add(command);

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
        var declarationNode = new DeclarationNode();

        prettyPrint("DeclarationNode", context);
        incrIndent();

        var identifier = new IdentifierNode();
        prettyPrint("IdentifierNode", context);
        incrIndent();

        identifier.Name = context.ID().ToString();

        TypeNode type;

        switch (context.type().children[0].ToString())
        {
            case ("number"):
                prettyPrint("NumberNode", context);
                type = new NumberNode();
                identifier.Type = type;
                break;
            case ("boolean"):
                prettyPrint("BooleanNode", context);
                type = new BooleanNode();
                identifier.Type = type;
                break;
        }
        decrIndent();

        ASTNode expression = Visit(context.ass());

        declarationNode.Expression = (ExpressionNode)expression;
        declarationNode.Identifier = identifier;

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
            Visit(ctrlStrct);
        }
        else if (listStmt != null && listStmt.ChildCount > 0)
        {
            Visit(listStmt);
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

        decrIndent();
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
            (cmds != null && cmds.ChildCount > 0) &&
            (RCURLY != null))
        {
            return Visit(cmds);
        }
        else
            throw new Exception();
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
            var blockNode = (BlockNode)Visit(block);
            var predicateNode = (ExpressionNode)Visit(expr);
            outputNode.Block = blockNode;
            outputNode.Predicate = predicateNode;
        }
        else
            throw new Exception();

        return outputNode;
    }
}
