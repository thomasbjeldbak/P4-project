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
    //Contains indents for the pretty printer
    private string _indent;

    #region PrettyPrinter Methods

    //Pretty prints a node and what text is currently in the context
    private void prettyPrint(string nodeName, ParserRuleContext context)
    {
        Console.WriteLine($"{_indent}{nodeName}");
    }

    //Increment identing for pretty printer
    private void incrIndent()
    {
        _indent += "\t";
    }

    //Decrement indenting for pretty printer
    private void decrIndent()
    {
        _indent = _indent.Substring(0, _indent.Length - 1);
    }

    #endregion

    //ExprParserBaseVisitor handles visiting the nodes as long as
    //the correct context is given to the 'Visit' function

    //All context belonging to the node in the parser being visited is
    //intialized at the beginning of the function.
    //If an OutputNode is intialized, this node i also the one being returned
    //What will be explained in each function is what is visited, and what the
    //returned node is used for


    //program: cmds;
    public override ASTNode VisitProgram([NotNull] ExprParser.ProgramContext context)
    {
        //outputNode = ProgramNode
        //visit(cmds) -> returns BlockNode.
        //  The BlockNodes commands is assigned to the programNodes commands
        var cmds = context.cmds();

        var outputNode = new ProgramNode() { Line = cmds.start.Line };

            prettyPrint("ProgramNode", context);
            incrIndent();

        if (cmds != null)
        {
            var blockNode = (BlockNode)Visit(cmds);
            outputNode.Commands = blockNode.Commands;
        }

        return outputNode;
    }

    //cmds: cmd cmds | /*epsilon*/;
    public override ASTNode VisitCmds([NotNull] ExprParser.CmdsContext context)
    {
        //outputNode = BlockNode
        //visit(cmd) -> returns a CommandNode. This node is
        //  assigned to the BlockNodes Commands
        //visit(cmds) -> returns A BlockNode. This nodes commands
        //  is assigned to the outputNodes Commands
        var cmd = context.cmd();
        var cmds = context.cmds();

        var outputNode = new BlockNode() { Line = cmd.start.Line };
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
        //returns either a StatementNode or a DeclarationNode
        //visit(stmt), visit(dcl) -> visits them if they aren't null and returns the result
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
        //outputNode = DelcarationNode
        //visit(type) -> gets TypeNode
        //visit(ass) -> gets ExpressionNode
        //Type and Expression is assigned to an identifierNode
        //The identifierNode is assigned to the OutputNode
        var type = context.type();
        var ID = context.ID();
        var ass = context.ass();
        var SEMI = context.SEMI();

        var outputNode = new DeclarationNode() { Line = ass.start.Line };

        prettyPrint("DeclarationNode", context);
        incrIndent();

        if ((type != null) &&
            (ID != null) &&
            (ass != null) &&
            (SEMI != null))
        {
            var identifier = new IdentifierNode() { Line = ID.Symbol.Line };
            prettyPrint("IdentifierNode", context);
            incrIndent();

            identifier.Name = ID.ToString();
            var typeNode = (TypeNode)Visit(type);
            identifier.TypeNode = typeNode;

            if (ass.ChildCount > 0)
            {
                outputNode.Expression = (ExpressionNode)Visit(ass);
            }

            outputNode.Identifier = identifier;
        }

        decrIndent();
        return outputNode;
    }

    //ass: ASSIGN expr | /*epsilon*/;
    public override ASTNode VisitAss([NotNull] ExprParser.AssContext context)
    {
        //visit(expr) -> gets ExpressionNode and returns it
        return Visit(context.expr());
    }

    //stmt: ID ASSIGN expr SEMI | ctrlStrct | listStmt SEMI | funcDef | funcCall SEMI;
    public override ASTNode VisitStmt([NotNull] ExprParser.StmtContext context)
    {
        //outputNode = StatementNode
        //Visit(expr) -> Get ExpressionNode
        //  ExpressionNode and ID is assigned to an AssignNode which is returned
        //Visit(ctrlStrct) -> Gets and returns a ControlStructureNode
        //Visit(listStmt) -> Gets and returns a ListOperationNode
        //Visit(funcDef) -> Gets and returns a FunctionDefinitionNode
        //Visit(funcCall) -> Gets and returns a FunctionCallNode

        var ID = context.ID();
        var ASSIGN = context.ASSIGN();
        var expr = context.expr();
        var SEMI = context.SEMI();
        var ctrlStrct = context.ctrlStrct();
        var listStmt = context.listStmt();
        var funcDef = context.funcDef();
        var funcCall = context.funcCall();

        StatementNode outputNode = null;

        if ((ID != null) && 
            (ASSIGN != null) &&
            (expr != null && expr.ChildCount > 0) &&
            SEMI != null)
        {
            var assignNode = new AssignNode() { Line = expr.start.Line };
            prettyPrint("AssignNode", context);
            incrIndent();

            var identifier = new IdentifierNode() { Line = expr.start.Line };
            prettyPrint("IdentifierNode", context);
            identifier.Name = ID.ToString();

            var expression = (ExpressionNode)Visit(expr);
            assignNode.Expression = expression;
            assignNode.Identifier = identifier;
            outputNode = assignNode;
        }
        else if (ctrlStrct != null && ctrlStrct.ChildCount > 0)
        {
            outputNode = (StatementNode)Visit(ctrlStrct);
        }
        else if (listStmt != null && listStmt.ChildCount > 0)
        {
            outputNode = (StatementNode)Visit(listStmt);
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
        //Returns an ExpressionNode
        //Visit(oprOr) -> Gets InfixExpression (with a right-side)
        //Visit(logicOr) -> If an oprOr exists, Get ExpressionNode and assigns it
        //to the left side of an expression. Else get and return ExpressionNode

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
        //Returns an InfixExpression
        //Visit(oprOr) -> Gets a InfixExpression (with a right-side)
        //Visit(logicOr) -> Gets an ExpressionNode and assigns it
        //to the left-side of the InfixExpression.
        //This Infix Expression is assigned to the right-side of another InfixExpression
        //which is returned

        var oprOr = context.oprOr();
        var logicOr = context.logicOr();
        var OR = context.OR();

        InfixExpressionNode outputNode;

        if (OR != null)
        {
            prettyPrint("OrNode", context);
            outputNode = new OrNode() { Line = OR.Symbol.Line };
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
        //Returns an ExpressionNode
        //Visit(oprAnd) -> Gets InfixExpression (with a right-side)
        //Visit(logicAnd) -> If an oprAnd exists, Get ExpressionNode and assigns it
        //to the left side of an expression. Else get and return ExpressionNode

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
        //Returns an InfixExpression
        //Visit(oprAnd) -> Gets a InfixExpression (with a right-side)
        //Visit(logicAnd) -> Gets an ExpressionNode and assigns it
        //to the left-side of the InfixExpression.
        //This Infix Expression is assigned to the right-side of another InfixExpression
        //which is returned

        var oprAnd = context.oprAnd();
        var logicAnd = context.logicAnd();
        var AND = context.AND();

        InfixExpressionNode outputNode;

        //Check if it's a addition or subtraction
        if (AND != null)
        {
            prettyPrint("AndNode", context);
            outputNode = new AndNode() { Line = AND.Symbol.Line };
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
        //Returns an ExpressionNode
        //Visit(oprEql) -> Gets InfixExpression (with a right-side)
        //Visit(equal) -> If an oprEql exists, Get ExpressionNode and assigns it
        //to the left side of an expression. Else get and return ExpressionNode

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
        //Returns an InfixExpression
        //Visit(oprEql) -> Gets a InfixExpression (with a right-side)
        //Visit(equal) -> Gets an ExpressionNode and assigns it
        //to the left-side of the InfixExpression.
        //This Infix Expression is assigned to the right-side of another InfixExpression
        //which is returned

        var oprEql = context.oprEql();
        var equal = context.equal();
        var EQUAL = context.EQUAL();
        var NOT = context.NOT();

        InfixExpressionNode outputNode;

        if (EQUAL != null)
        {
            prettyPrint("EqualNode", context);
            outputNode = new EqualNode() { Line = EQUAL.Symbol.Line };
        }
        else if (NOT != null)
        {
            prettyPrint("NotEqualNode", context);
            outputNode = new NotEqualNode() { Line = NOT.Symbol.Line };
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
        //Returns an ExpressionNode
        //Visit(oprBool) -> Gets InfixExpression (with a right-side)
        //Visit(@bool) -> If an oprBool exists, Get ExpressionNode and assigns it
        //to the left side of an expression. Else get and return ExpressionNode

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
        //Returns an InfixExpression
        //Visit(oprBool) -> Gets a InfixExpression (with a right-side)
        //Visit(@bool) -> Gets an ExpressionNode and assigns it
        //to the left-side of the InfixExpression.
        //This Infix Expression is assigned to the right-side of another InfixExpression
        //which is returned

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
            outputNode = new GreaterNode() { Line = GREAT.Symbol.Line };
        }
        else if (GREATEQL != null)
        {
            prettyPrint("GreaterEqualNode", context);
            outputNode = new GreaterEqualNode() { Line = GREATEQL.Symbol.Line };
        }
        else if (LESS != null)
        {
            prettyPrint("LessNode", context);
            outputNode = new LessNode() { Line = LESS.Symbol.Line };
        }
        else if (LESSEQL != null)
        {
            prettyPrint("LessEqualNode", context);
            outputNode = new LessEqualNode() { Line = LESSEQL.Symbol.Line };
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
        //Returns an ExpressionNode
        //Visit(oprExpr) -> Gets InfixExpression (with a right-side)
        //Visit(term) -> If an oprExpr exists, Get ExpressionNode and assigns it
        //to the left side of an expression. Else get and return ExpressionNode

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
        //Returns an InfixExpression
        //Visit(oprExpr) -> Gets a InfixExpression (with a right-side)
        //Visit(term) -> Gets an ExpressionNode and assigns it
        //to the left-side of the InfixExpression.
        //This Infix Expression is assigned to the right-side of another InfixExpression
        //which is returned

        var oprExpr = context.oprExpr();
        var term = context.term();
        var ADD = context.ADD();
        var SUB = context.SUB();

        InfixExpressionNode outputNode;

        if (ADD != null)
        {
            prettyPrint("AdditionNode", context);
            outputNode = new AdditionNode() { Line = ADD.Symbol.Line };
        }
        else if (SUB != null)
        {
            prettyPrint("SubtractionNode", context);
            outputNode = new SubtractionNode() { Line = SUB.Symbol.Line };
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
        //Returns an ExpressionNode
        //Visit(oprTerm) -> Gets InfixExpression (with a right-side)
        //Visit(factor) -> If an oprTerm exists, Get ExpressionNode and assigns it
        //to the left side of an expression. Else get and return ExpressionNode

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
        //Returns an InfixExpression
        //Visit(oprTerm) -> Gets a InfixExpression (with a right-side)
        //Visit(factor) -> Gets an ExpressionNode and assigns it
        //to the left-side of the InfixExpression.
        //This Infix Expression is assigned to the right-side of another InfixExpression
        //which is returned

        var oprTerm = context.oprTerm();
        var factor = context.factor();
        var MUL = context.MUL();
        var DIV = context.DIV();

        InfixExpressionNode outputNode;

        if (MUL != null)
        {
            prettyPrint("MultiplicationNode", context);
            outputNode = new MultiplicationNode() { Line = MUL.Symbol.Line };
        }
        else if (DIV != null)
        {
            prettyPrint("DivisionNode", context);
            outputNode = new DivisionNode() { Line = DIV.Symbol.Line };
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
        //Returns an ExpressionNode
        //Visit(expr) -> Get and return ExpressionNode
        //Visit(boolean) -> Get and return BooleanNode
        //If ID != null -> Get and return IdentifierNode
        //If INT != null -> Get and return NumberNode
        //If STR != null -> Get and return TextNode

        var INT = context.INT();
        var STR = context.STR();
        var ID = context.ID();
        var LPAREN = context.LPAREN();
        var RPAREN = context.RPAREN();
        var expr = context.expr();
        var boolean = context.boolean();

        ExpressionNode expression;

        if (INT != null)
        {
            prettyPrint("NumberNode", context);
            var numberNode = new NumberNode() { Line = INT.Symbol.Line };
            numberNode.Value = int.Parse(INT.ToString());
            return numberNode;
        }

        if (ID != null)
        {
            prettyPrint("IdentifierNode", context);
            var identifierNode = new IdentifierNode() { Line = ID.Symbol.Line };
            identifierNode.Name = ID.ToString();
            return identifierNode;
        }

        if ((LPAREN != null) &&
            (expr != null && expr.ChildCount > 0) &&
            (RPAREN != null))
        {
            return (ExpressionNode)Visit(expr);
        }

        if (boolean != null)
        {
            prettyPrint("booleanNode", context);
            var booleanNode = Visit(boolean);
            return booleanNode;
        }

        if (STR != null)
        {
            prettyPrint("TextNode", context);
            var textNode = new TextNode() { Line = STR.Symbol.Line };
            textNode.Value = STR.ToString();
            return textNode;
        }

        return null;
    }

    //boolean: TRUE | FALSE; 
    public override ASTNode VisitBoolean([NotNull] ExprParser.BooleanContext context)
    {
        //outputNode = BooleanNode
        //If TRUE != null -> set value to true
        //If FALSE != null -> set value to false

        var TRUE = context.TRUE();
        var FALSE = context.FALSE();

        var outputNode = new BooleanNode();

        if (TRUE != null)
        {
            outputNode.Value = true;
            outputNode.Line = TRUE.Symbol.Line;
        }
        else if (FALSE != null)
        {
            outputNode.Value = false;
            outputNode.Line = FALSE.Symbol.Line;
        }
        else
            throw new Exception();

        return outputNode;
    }

    //block: LCURLY cmds RCURLY;
    public override ASTNode VisitBlock([NotNull] ExprParser.BlockContext context)
    {
        //outputNode = BlockNode
        //Visit(cmds) -> Gets and returns BlockNode

        var LCURLY = context.LCURLY();
        var cmds = context.cmds();
        var RCURLY = context.RCURLY();

        var outputNode = new BlockNode() { Line = LCURLY.Symbol.Line };

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
        //Visit(ifStmt) -> Get and return IfNode
        //Visit(loop) -> Get and return ControlStructureNode

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
        //outputNode = IfNode
        //Visit(expr) -> Get ExpressionNode, assign it to IfNode
        //Visit(block) -> Get BlockNode, assign it to IfNode
        //Visit(elseIfStmt) -> Get IfNode, and assign the IfNodes ElseIfs, to
        //the outputNodes ElseIfs

        var IF = context.IF();
        var LPAREN = context.LPAREN();
        var expr = context.expr();
        var RPAREN = context.RPAREN();
        var block = context.block();
        var elseIfStmt = context.elseIfStmt();

        var outputNode = new IfNode() { Line = IF.Symbol.Line };

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
        //outputNode = IfNode
        //Visit(@else) -> Get ElseNode, and Add to outputNodes ElseIfs
        //Visit(elseIfStmt) -> Get IfNode, and Add IfNodes ElseIfs to
        //the outputNodes ElseIfs

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

            elseNode.Line = @else.start.Line;
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

            elseIfNode.Line = ELSE.Symbol.Line;
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
        //outputNode = ElseNode
        //visit(block) -> get BlockNoded and assign to outputNode

        var ELSE = context.ELSE();
        var block = context.block();

        var outputNode = new ElseNode();

        if ((ELSE != null) &&
            (block != null))
        {
            outputNode.Line = ELSE.Symbol.Line;
            outputNode.Block = (BlockNode)Visit(block);
        }
        else
            throw new Exception();

        return outputNode;
    }

    //loop: REPEAT loops;
    public override ASTNode VisitLoop([NotNull] ExprParser.LoopContext context)
    {
        //Visit(loops) -> Get and return ControlStructureNode

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
        //Visit(loopSmt) -> Get and return RepeatNode
        //Visit(whileStmt) -> Get and return WhileNode
        //Visit(foreachStmt) -> Get and return ForeachNode

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
        //outputNode = RepeatNode
        //Visit(Block) -> Get BlockNode and assign to outputNode
        //Visit(expr) -> Get ExpressionNode and assign to outputNode

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
            outputNode.Line = LPAREN.Symbol.Line;
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
        //outputNode = WhileNode
        //Visit(expr) -> Get ExpressionNode and assign to outputNode
        //Visit(block) -> Get BlockNode and assign to outputNode

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
            outputNode.Line = LPAREN.Symbol.Line;
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
        //outputNode = ForeachNode
        //Visit(type) -> Get TypeNode
        //ID != null -> Set Name of IdentifierNode of local variable.
        //Assign TypeNode and IdentifierNode to a DeclarationNode
        //ID2 != null -> Set name of IdentifierNode of list
        //  Set type of this identifer to List.
        //Assign IdentifierNode to ouputNode (as list)
        //Assign DeclarationNode to outputNode (as local variable)
        //Visit(block) -> Get BlockNode and assign to outputNode

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
            prettyPrint("DeclarationNode", context);
            var localVar = new DeclarationNode() { Line = type.start.Line };
            var typeNode = (TypeNode)Visit(type);
            var localVarIdentifier = new IdentifierNode() { Line = ID.Symbol.Line };
            localVarIdentifier.TypeNode = typeNode;
            localVarIdentifier.Name = ID.ToString();
            localVar.Identifier = localVarIdentifier;

            prettyPrint("IdentifierNode", context);
            var identifierNode = new IdentifierNode() { Line = ID2.Symbol.Line };
            var listNode = new ListNode(typeNode.Type) { Line = ID2.Symbol.Line };
            identifierNode.TypeNode = listNode;
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
        //BOOl != null -> Return empty BooleanNode
        //TEXT != null -> Return empty TextNode
        //NUM != null -> Return empty NumberNode
        //LIST != null -> Get empty ListNode
            //Visit(type) -> Get TypeNode and assign its Type to ListNode

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
            var outputNode = new BooleanNode() { Line = BOOl.Symbol.Line };
            decrIndent();
            return outputNode;
        }
        else if (TEXT != null)
        {
            prettyPrint("TextNode", context);
            var outputNode = new TextNode() { Line = TEXT.Symbol.Line };
            decrIndent();
            return outputNode;
        }
        else if (NUM != null)
        {
            prettyPrint("NumberNode", context);
            var outputNode = new NumberNode() { Line = NUM.Symbol.Line };
            decrIndent();
            return outputNode;
        }
        else if ((LIST != null) &&
                 (LPAREN != null) &&
                 (type != null) &&
                 (RPAREN != null))
        {
            prettyPrint("ListNode", context);
            incrIndent();
            var listType = (TypeNode)Visit(type);
            var outputNode = new ListNode(listType.Type) { Line = LIST.Symbol.Line };
            decrIndent();
            return outputNode;

        }
        else
            throw new Exception();
    }

    //listStmt: ID COLON listOpr;
    public override ASTNode VisitListStmt([NotNull] ExprParser.ListStmtContext context)
    {
        //outputNode = ListOperationNode
        //Visit(listOpr) -> Get ListOperationNode and assing outputNode to it
        //ID != null -> Set Name of an IdentifierNode
        //Set TypeNode of IdentifierNode to ListNode
        //Assign IdentifierNode to outputNode

        var ID = context.ID();
        var COLON = context.COLON();
        var listOpr = context.listOpr();

        ListOperationNode outputNode;

        if (ID != null && COLON != null && listOpr != null)
        {
            var identifierNode = new IdentifierNode() { Line = ID.Symbol.Line };
            identifierNode.Name = ID.ToString();
            identifierNode.TypeNode = new ListNode(TypeEnum.list_object) { Line = ID.Symbol.Line };

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
        //outputNode = ListOperationNode
        //Visit(expr) -> Get ExpressionNode and assign to outputNode

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

                outputNode = new ListAddNode() { Line = LISTADD.Symbol.Line };
                outputNode.Expression = (ExpressionNode)Visit(expr);
            }
            else if (LISTDEL != null)
            {
                prettyPrint("ListDeleteNode", context);
                incrIndent();

                outputNode = new ListDeleteNode() { Line = LISTDEL.Symbol.Line };
                outputNode.Expression = (ExpressionNode)Visit(expr);
            }
            else if (LISTIDXOF != null)
            {
                prettyPrint("ListIndexOfNode", context);
                incrIndent();

                outputNode = new ListIndexOfNode() { Line = LISTIDXOF.Symbol.Line };
                outputNode.Expression = (ExpressionNode)Visit(expr);
            }
            else if (LISTVALOF != null)
            {
                prettyPrint("ListValueOfNode", context);
                incrIndent();

                outputNode = new ListValueOfNode() { Line = LISTVALOF.Symbol.Line };
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
