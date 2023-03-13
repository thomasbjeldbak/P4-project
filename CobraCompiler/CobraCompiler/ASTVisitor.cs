using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASTNodes;

namespace CobraCompiler
{
    internal abstract class ASTVisitor<T>
    {
        public abstract T Visit(ExpressionNode node);
        public abstract T Visit(NumberNode node);
        public abstract T Visit(IdentifierNode node);
        public abstract T Visit(DeclarationNode node);
        public abstract T Visit(AdditionNode node);
        public abstract T Visit(SubtractionNode node);
        public abstract T Visit(MultiplicationNode node);
        public abstract T Visit(DivisionNode node);

        public T Visit(ASTNode node)
        {
            return Visit((dynamic)node);
        }
    }
}
