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
        private readonly SymbolTable _symbolTable;
        private BlockNode _currentBlock;

        public Emitter(ILGenerator ilGenerator, SymbolTable symbolTable)
        {
            _ilGenerator = ilGenerator;
            _locals = new Dictionary<string, LocalBuilder>();
            _symbolTable = symbolTable;
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

        //public override ILGenerator Visit(IdentifierNode node)
        //{
        //    throw new NotImplementedException();
        //}

        public override ILGenerator Visit(DeclarationNode node)
        {
            Console.WriteLine("GIGA");
            var symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            var test = ConvertType(symbol.Type);
            var local = _ilGenerator.DeclareLocal(typeof(int));

            _locals[node.Identifier.Name] = local;

            if (node.Expression != null)
            {
                Visit(node.Expression);
                _ilGenerator.Emit(OpCodes.Stloc, local);
            }

            return _ilGenerator;

        }

        public override ILGenerator Visit(BlockNode node)
        {
            //abstract "Command" is either a declaration, assignment or statement
            _currentBlock = node;
            foreach (var command in node.Commands)
            {
                switch (command)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
            }

            return _ilGenerator;
        }

        public override ILGenerator Visit(StatementNode node)
        {
            //Check structure of statementNode
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(AssignNode node)
        {
            _locals.TryGetValue(node.Identifier.Name, out var local);

            Visit(node.Expression);
            _ilGenerator.Emit(OpCodes.Stloc, local);

            return _ilGenerator;

        }

        public override ILGenerator Visit(InfixExpressionNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(IfNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(ElseIfNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(ElseNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(RepeatNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(WhileNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(ForeachNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(ListOperationNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(ListAddNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(ListDeleteNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(ListIndexOfNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(ListValueOfNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(AndNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(OrNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(GreaterNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(LessNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(GreaterEqualNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(LessEqualNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(EqualNode node)
        {
            throw new NotImplementedException();
        }

        public override ILGenerator Visit(NotEqualNode node)
        {
            throw new NotImplementedException();
        }

        private Type ConvertType(TypeEnum inputType)
        {
            Type type = null;
            switch (inputType)
            {
                case TypeEnum.number:
                    type = typeof(int);
                    break;
                case TypeEnum.text:
                    type = typeof(string);
                    break;
                case TypeEnum.boolean:
                    type = typeof(bool);
                    break;
                case TypeEnum.list_number:
                    type = typeof(List<int>);
                    break;
                case TypeEnum.list_text:
                    type = typeof(List<string>);
                    break;
                case TypeEnum.list_boolean:
                    type = typeof(List<bool>);
                    break;
                default:
                    throw new Exception("Type is null");
            }

            return type;

        }


    }

}
