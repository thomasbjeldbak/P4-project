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
        public EvaluateExpressionVisitor(ErrorHandler errorHandler)
        {
            throw new NotImplementedException();
            
            //In addition to computing the result of an expression, the evaluator can also perform other tasks such as
            //validating the correctness of the expression or detecting errors. For example, the evaluator could detect
            //divide-by-zero errors or type errors, such as trying to add a number to a string.

            //In the example program we have been discussing, the EvaluateExpressionVisitor class is responsible for
            //computing the value of the expression represented by the AST. However, you could modify this class to
            //perform other tasks as well, such as detecting errors or validating the correctness of the expression.
        }

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
            return Visit(node.TypeNode);
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
