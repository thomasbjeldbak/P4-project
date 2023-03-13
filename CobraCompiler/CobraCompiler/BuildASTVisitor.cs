using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CobraCompiler;
using System;
using System.Linq.Expressions;
using System.Xml.Linq;

internal class BuildASTVisitor : ExprParserBaseVisitor<ASTNodes.ASTNode>
{
    public override ASTNodes.ASTNode VisitProgram([NotNull] ExprParser.ProgramContext context)
    {
        Console.WriteLine("Visited Program" + context.GetText());
        return Visit(context.cmds());
    }
    public override ASTNodes.ASTNode VisitCmds([NotNull] ExprParser.CmdsContext context)
    {
        Console.WriteLine("Visited Cmds " + context.GetText());
        return Visit(context.cmd());
        return Visit(context.cmds());
    }

    public override ASTNodes.ASTNode VisitCmd([NotNull] ExprParser.CmdContext context)
    {
        Console.WriteLine("Visited Cmd " + context.GetText());
        return Visit(context.dcl());
    }

    public override ASTNodes.ASTNode VisitDcl([NotNull] ExprParser.DclContext context)
    {
        Console.WriteLine("Visited Dcl" + context.GetText());
        var declarationNode = new ASTNodes.DeclarationNode();

        var identifier = new ASTNodes.IdentifierNode();

        ASTNodes.ASTNode expression = VisitAss(context.ass());

        declarationNode.Expression = (ASTNodes.InfixExpressionNode)expression;

            identifier.Name = context.ID().ToString();
        declarationNode.Identifier = identifier;

        switch (context.type().children[0].ToString())
        {
            case ("number"):
                var type = new ASTNodes.NumberNode();
                identifier.Type = type;
                break;
        }


        return declarationNode;
    }

    public override ASTNodes.ASTNode VisitAss([NotNull] ExprParser.AssContext context)
    {
        Console.WriteLine("Visited Ass" + context.GetText());
        return Visit(context.expr());
    }

    public override ASTNodes.ASTNode VisitExpr([NotNull] ExprParser.ExprContext context)
    {
        Console.WriteLine("Visited Expr" + context.GetText());
        //var infixExpressionNode = new InfixExpressionNode();

        //var oprOr = (ExpressionNode)Visit(context.oprOr());

        //if (oprOr == null)
        //    return Visit(context.logicOr());

        //var logicOr = (ExpressionNode)Visit(context.logicOr());

        //infixExpressionNode.Left = logicOr;
        //infixExpressionNode.Right = oprOr;

        //return infixExpressionNode;

        return Visit(context.logicOr());

    }

    public override ASTNodes.ASTNode VisitLogicOr([NotNull] ExprParser.LogicOrContext context)
    {
        Console.WriteLine("Visited LogicOr" + context.GetText());
        return Visit(context.logicAnd());
    }
    public override ASTNodes.ASTNode VisitLogicAnd([NotNull] ExprParser.LogicAndContext context)
    {
        Console.WriteLine("Visited LogicAnd" + context.GetText());
        return Visit(context.equal());
    }
    public override ASTNodes.ASTNode VisitEqual([NotNull] ExprParser.EqualContext context)
    {
        Console.WriteLine("Visited VisitEqual" + context.GetText());
        return Visit(context.@bool());
    }

    public override ASTNodes.ASTNode VisitBool([NotNull] ExprParser.BoolContext context)
    {
        Console.WriteLine("Visited Bool" + context.GetText());
        return Visit(context.term());
    }

    public override ASTNodes.ASTNode VisitTerm([NotNull] ExprParser.TermContext context)
    {
        Console.WriteLine("Visited Term" + context.GetText());
        ASTNodes.MultiplicationNode multiplication = new ASTNodes.MultiplicationNode();
        multiplication.Left = (ASTNodes.ExpressionNode)Visit(context.factor());
        multiplication.Right = (ASTNodes.ExpressionNode)Visit(context.oprTerm());

        return multiplication;
    }

    public override ASTNodes.ASTNode VisitOprTerm([NotNull] ExprParser.OprTermContext context)
    {
        Console.WriteLine("Visited OprTerm" + context.GetText());
        var expression = new ASTNodes.ExpressionNode();
        expression.Value = Visit(context.factor());

        return expression;
    }

    public override ASTNodes.ASTNode VisitFactor([NotNull] ExprParser.FactorContext context)
    {
        Console.WriteLine("Visited Factor" + context.GetText());
        var expression = new ASTNodes.ExpressionNode();
        expression.Value = int.Parse(context.INT().ToString());

        return expression;
    }
}
