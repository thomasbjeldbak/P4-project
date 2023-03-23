using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASTNodes;

namespace CobraCompiler
{
    internal class EvaluateExpressionVisitor : ASTVisitor<int>
    {
        public override int Visit(AdditionNode node)
        {
            return Visit(node.Left) + Visit(node.Right);
        }

        public override int Visit(SubtractionNode node)
        {
            return Visit(node.Left) - Visit(node.Right);
        }

        public override int Visit(MultiplicationNode node)
        {
            return Visit(node.Left) * Visit(node.Right);
        }

        public override int Visit(DivisionNode node)
        {
            return Visit(node.Left) / Visit(node.Right);
        }

        public override int Visit(NumberNode node)
        {
            return node.Value;
        }

        public override int Visit(IdentifierNode node)
        {
            return Visit(node.Type);
        }

        public override int Visit(DeclarationNode node)
        {
            return Visit(node.Expression);
        }

        public override int Visit(ExpressionNode node)
        {
            return node.Value;
        }
    }
}
