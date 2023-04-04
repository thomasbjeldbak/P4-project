using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static ASTNodes;

namespace CobraCompiler
{
    internal class Emitter : ASTVisitor<ILGenerator>
{
        private readonly ILGenerator _ilGenerator;
        private readonly Dictionary<string, LocalBuilder> _locals;

        public Emitter(ILGenerator ilGenerator)
        {
            _ilGenerator = ilGenerator;
            _locals = new Dictionary<string, LocalBuilder>();
        }

        public override ILGenerator Visit(NumberNode node)
        {
            _ilGenerator.Emit(OpCodes.Ldc_I4, node.Value);
            return _ilGenerator;
        }

        public override ILGenerator Visit(AdditionNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            _ilGenerator.Emit(OpCodes.Add);
            return _ilGenerator;
        }
        public override ILGenerator Visit(SubtractionNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            _ilGenerator.Emit(OpCodes.Sub);
            return _ilGenerator;
        }

        public override ILGenerator Visit(MultiplicationNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            _ilGenerator.Emit(OpCodes.Mul);
            return _ilGenerator;
        }

        public override ILGenerator Visit(DivisionNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            _ilGenerator.Emit(OpCodes.Div);
            return _ilGenerator;
        }

        public override ILGenerator Visit(ExpressionNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(IdentifierNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(DeclarationNode node)
        {
            throw new NotImplementedException();
        }



    }
}
