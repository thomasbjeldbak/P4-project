using Antlr4.Runtime.Misc;
using System;
using static ASTNodes;

internal class BuildASTVisitor : ExprParserBaseVisitor<ProgramNode>
{
    public override ProgramNode VisitProgram(ExprParser.ProgramContext context)
    {
        foreach (var cmd in context.cmds())
        {
            Visit(cmd);
        }
        return null;
    }

    public override ProgramNode VisitDcl([NotNull] ExprParser.DclContext context)
    {
        return new DeclarationNode
        {
            Expression = (ExpressionNode)Visit(context.ass()),
            Identifier = (IdentifierNode)Visit(context.ID())
        };
    }
    public override ProgramNode VisitAss([NotNull] ExprParser.AssContext context)
    {
        return base.VisitAss(context);
    }

}
