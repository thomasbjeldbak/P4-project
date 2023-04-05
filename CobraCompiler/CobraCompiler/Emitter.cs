using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASTNodes;

namespace CobraCompiler {
    internal class Emitter: ASTVisitor<StringBuilder> {

        private readonly StringBuilder _stringBuilder;
        private readonly SymbolTable _symbolTable;
        private BlockNode _currentBlock;


        public Emitter(StringBuilder stringbuilder, SymbolTable symbolTable)
        {
            _stringBuilder = stringbuilder;
            _symbolTable = symbolTable;
        }

        public override StringBuilder Visit(ProgramNode node)
        {
            _currentBlock = node;
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("static void Main(string[] args)");
            stringBuilder.AppendLine("{");

            foreach (var command in node.Commands)
            {
                switch (command)
                {
                    case DeclarationNode declarationNode:
                        stringBuilder.Append(Visit(declarationNode));
                        break;
                    case StatementNode statementNode:
                        stringBuilder.Append(Visit(statementNode));
                        break;
                    case AssignNode assignNode:
                        stringBuilder.Append(Visit(assignNode));
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
            }

            stringBuilder.AppendLine("}");

            return stringBuilder;
        }
        public override StringBuilder Visit(BlockNode node)
        {
            _currentBlock = node;
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("{");

            foreach(var command in node.Commands)
            {
                switch(command)
                {
                    case DeclarationNode declarationNode:
                        stringBuilder.Append(Visit(declarationNode));
                        break;
                    case StatementNode statementNode:
                        stringBuilder.Append(Visit(statementNode));
                        break;
                    case AssignNode assignNode:
                        stringBuilder.Append(Visit(assignNode));
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
            }

            stringBuilder.AppendLine("}");

            return stringBuilder;
        }

        public override StringBuilder Visit(DeclarationNode node)
        {
            var symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(_typeAlias[ConvertType(symbol.Type)]);
            stringBuilder.Append(' ');
            stringBuilder.Append(node.Identifier.Name);

            if(node.Expression != null)
            {
                stringBuilder.Append(" = ");
                stringBuilder.Append(Visit(node.Expression));
            }

            stringBuilder.AppendLine(";");

            return stringBuilder;
        }

        public override StringBuilder Visit(StatementNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(AssignNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(node.Identifier.Name);
            stringBuilder.Append(" = ");
            stringBuilder.Append(Visit(node.Expression));
            stringBuilder.AppendLine(";");
            
            return stringBuilder;
        }

        public override StringBuilder Visit(ExpressionNode node)
        {
            var stringBuilder = new StringBuilder();

            switch (node)
            {
                case InfixExpressionNode infixExpressionNode:
                    stringBuilder.Append(Visit(infixExpressionNode));
                    break;
                case IdentifierNode identifierNode:
                    stringBuilder.Append(identifierNode.Value.ToString());
                    break;
                case NumberNode numberNode:
                    stringBuilder.Append(numberNode.Value.ToString());
                    break;
                case TextNode textNode:
                    stringBuilder.Append(textNode.Value.ToString()); //R To.String ikke irrellevant i den her?
                    break;
                case BooleanNode booleanNode:
                    stringBuilder.Append(booleanNode.Value.ToString());
                    break;
                case ListNode listNode:
                    //_stringBuilder.Append("new List<");
                    //_stringBuilder.Append(_typeAlias[ConvertType(listNode.Type)]);
                    //_stringBuilder.Append("> { ");
                    //for (int i = 0; i < listNode.Values.Count; i++)
                    //{
                    //    Visit(listNode.Values[i]);
                    //    if (i < listNode.Values.Count - 1)
                    //    {
                    //        _builder.Append(", ");
                    //    }
                    //}
                    //_stringBuilder.Append(" }");
                    throw new NotImplementedException();
                    break;
                default:
                    throw new Exception($"ExpressionNode type not valid");
            }

            return stringBuilder;
        }

        public override StringBuilder Visit(InfixExpressionNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            stringBuilder.Append(Visit(node.Left));

            switch (node)
            {
                case AdditionNode additionNode:
                    stringBuilder.Append(Visit(additionNode));
                    break;
                case SubtractionNode subtractionNode:
                    stringBuilder.Append(Visit(subtractionNode));
                    break;
                case MultiplicationNode multiplicationNode:
                    stringBuilder.Append(Visit(multiplicationNode));
                    break;
                case DivisionNode divideNode:
                    stringBuilder.Append(Visit(divideNode));
                    break;
                case AndNode andNode:
                    stringBuilder.Append(Visit(andNode));
                    break;
                case OrNode orNode:
                    stringBuilder.Append(Visit(orNode));
                    break;
                case EqualNode equalNode:
                    stringBuilder.Append(Visit(equalNode));
                    break;
                case NotEqualNode notEqualNode:
                    stringBuilder.Append(Visit(notEqualNode));
                    break;
                case GreaterNode greaterNode:
                    stringBuilder.Append(Visit(greaterNode));
                    break;
                case LessNode lessNode:
                    stringBuilder.Append(Visit(lessNode));
                    break;
                case GreaterEqualNode greaterEqualNode:
                    stringBuilder.Append(Visit(greaterEqualNode));
                    break;
                case LessEqualNode lessEqualNode:
                    stringBuilder.Append(Visit(lessEqualNode));
                    break;
                default:
                    throw new Exception();
            }

            stringBuilder.Append(Visit(node.Right));
            stringBuilder.Append(")");

            return stringBuilder;
        }

        public override StringBuilder Visit(IfNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(ElseIfNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(ElseNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(RepeatNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(WhileNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(ForeachNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(ListOperationNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(ListAddNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(ListDeleteNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(ListIndexOfNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(ListValueOfNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(AdditionNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(" + ");

            return stringBuilder;
        }

        public override StringBuilder Visit(SubtractionNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(" - ");

            return stringBuilder;
        }

        public override StringBuilder Visit(MultiplicationNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(" * ");

            return stringBuilder;
        }

        public override StringBuilder Visit(DivisionNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(" / ");

            return stringBuilder;
        }

        public override StringBuilder Visit(AndNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(OrNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(GreaterNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(LessNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(GreaterEqualNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(LessEqualNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(EqualNode node)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder Visit(NotEqualNode node)
        {
            throw new NotImplementedException();
        }

        private Type ConvertType(TypeEnum inputType)
        {
            Type type = null;
            switch(inputType)
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


        // This is the set of types from the C# keyword list.
        static Dictionary<Type, string> _typeAlias = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(int), "int" },
            { typeof(string), "string" },

            { typeof(void), "void" }
        };

    }
}
