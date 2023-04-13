using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASTNodes;

namespace CobraCompiler
{
    internal class TypeChecker : ASTVisitor<TypeEnum?>
    {
        private readonly SymbolTable _symbolTable;
        private BlockNode _currentBlock;

        public TypeChecker(SymbolTable symbolTable) 
        {
            _symbolTable = symbolTable;
        }

        public override TypeEnum? Visit(ProgramNode node)
        {
            _currentBlock = node;

            if (node.Commands == null)
                return null;

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
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
            return null;
        }
        public override TypeEnum? Visit(BlockNode node)
        {
            _currentBlock = node;

            if (node.Commands == null)
                return null;

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
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
            return null;
        }

        public override TypeEnum? Visit(DeclarationNode node)
        {
            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            if (node.Expression != null)
            {
                TypeEnum? exprNode = Visit(node.Expression);

                if (symbol.Type != exprNode)
                    throw new Exception($"Initialization of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
            }

            return symbol.Type;

        }

        public override TypeEnum? Visit(StatementNode node)
        {
            switch (node)
            {
                case IfNode ifNode:
                    Visit(ifNode);
                    break;
                case RepeatNode repeatNode:
                    Visit(repeatNode);
                    break;
                case WhileNode whileNode:
                    Visit(whileNode);
                    break;
                case ListOperationNode listOperationNode:
                    Visit(listOperationNode);
                    break;
                case ForeachNode foreachNode:
                    Visit(foreachNode);
                    break;
                default:
                    throw new Exception();
            }
            return null;
        }

        public override TypeEnum? Visit(AssignNode node)
        {
            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            if (node.Expression != null)
            {
                TypeEnum? exprNode = Visit(node.Expression);

                if (symbol.Type != exprNode)
                    throw new Exception($"Assignment of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
            }

            return symbol.Type;
        }

        public override TypeEnum? Visit(ExpressionNode node)
        {
            TypeEnum? type;
            switch (node)
            {
                case InfixExpressionNode infixExpressionNode:
                    type = Visit(infixExpressionNode);
                    break;
                case IdentifierNode identifierNode:
                    Symbol symbol = _symbolTable.Lookup(identifierNode.Name, _currentBlock);
                    type = symbol.Type;
                    break;
                case NumberNode numberNode:
                    type = numberNode.Type; 
                    break;
                case TextNode textNode:
                    type = textNode.Type; 
                    break;
                case BooleanNode booleanNode:
                    type = booleanNode.Type;
                    break;
                case ListNode listNode:
                    type = listNode.Type;
                    break;
                default:
                    throw new Exception();
            }

            return type;
        }

        public override TypeEnum? Visit(InfixExpressionNode node)
        {
            TypeEnum? type;
            switch (node)
            {
                case AdditionNode additionNode:
                    type = Visit(additionNode);
                    break;
                case SubtractionNode subtractionNode:
                    type = Visit(subtractionNode);
                    break;
                case MultiplicationNode multiplicationNode:
                    type = Visit(multiplicationNode);
                    break;
                case DivisionNode divideNode:
                    type = Visit(divideNode);
                    break;
                case AndNode andNode:
                    type = Visit(andNode);
                    break;
                case OrNode orNode:
                    type = Visit(orNode);
                    break;
                case EqualNode equalNode:
                    type = Visit(equalNode);
                    break;
                case NotEqualNode notEqualNode:
                    type = Visit(notEqualNode);
                    break;
                case GreaterNode greaterNode:
                    type = Visit(greaterNode);
                    break;
                case LessNode lessNode:
                    type = Visit(lessNode);
                    break;
                case GreaterEqualNode greaterEqualNode:
                    type = Visit(greaterEqualNode);
                    break;
                case LessEqualNode lessEqualNode:
                    type = Visit(lessEqualNode);
                    break;
                default:
                    throw new Exception();
            }

            return type;
        }

        #region Visit TypeNodes
        public override TypeEnum? Visit(AdditionNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type == TypeEnum.boolean) 
                throw new Exception();

            if (isList(type))
                throw new Exception();

            return type;
        }

        public override TypeEnum? Visit(SubtractionNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type == TypeEnum.boolean)
                throw new Exception();

            if (isList(type))
                throw new Exception();

            return type;
        }

        public override TypeEnum? Visit(MultiplicationNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type == TypeEnum.boolean)
                throw new Exception();

            if (isList(type))
                throw new Exception();

            return type;
        }

        public override TypeEnum? Visit(DivisionNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type == TypeEnum.boolean)
                throw new Exception();

            if (isList(type))
                throw new Exception();

            return type;
        }

        public override TypeEnum? Visit(AndNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.boolean)
                throw new Exception();

            return type;
        }

        public override TypeEnum? Visit(OrNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.boolean)
                throw new Exception();

            return type;
        }

        public override TypeEnum? Visit(EqualNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (isList(type))
                throw new Exception();

            return TypeEnum.boolean;
        }

        public override TypeEnum? Visit(NotEqualNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (isList(type))
                throw new Exception();

            return TypeEnum.boolean;
        }

        public override TypeEnum? Visit(GreaterNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return TypeEnum.boolean;
        }

        public override TypeEnum? Visit(LessNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return TypeEnum.boolean;
        }

        public override TypeEnum? Visit(GreaterEqualNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return TypeEnum.boolean;
        }

        public override TypeEnum? Visit(LessEqualNode node)
        {
            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return TypeEnum.boolean;
        }

        #endregion

        public override TypeEnum? Visit(IfNode node)
        {
            TypeEnum? type = Visit(node.Condition);
            
            if (node.Block != null)
                Visit(node.Block);

            foreach (var @else in node.ElseIfs)
            {
                switch (@else)
                {
                    case ElseIfNode elseIf:
                        Visit(elseIf);
                        break;
                    case ElseNode:
                        Visit(@else);
                        break;
                    default:
                        throw new Exception();
                }
            }

            if (type != TypeEnum.boolean)
                throw new Exception();

            return null;
        }

        public override TypeEnum? Visit(ElseIfNode node)
        {
            TypeEnum? type = Visit(node.Condition);
            
            if (node.Block != null)
                Visit(node.Block);

            if (type != TypeEnum.boolean)
                throw new Exception();

            return null;
        }

        public override TypeEnum? Visit(ElseNode node)
        {
            if (node.Block != null)
                Visit(node.Block);

            return null;
        }

        public override TypeEnum? Visit(RepeatNode node)
        {
            TypeEnum? type = Visit(node.Expression);
            
            if (node.Block != null)
                Visit(node.Block);

            if (type != TypeEnum.number)
                throw new Exception();

            return null;
        }

        public override TypeEnum? Visit(WhileNode node)
        {
            TypeEnum? type = Visit(node.Condition);

            if (node.Block != null)
                Visit(node.Block);

            if (type != TypeEnum.boolean)
                throw new Exception();

            return null;
        }

        public override TypeEnum? Visit(ForeachNode node)
        {
            TypeEnum? localVarType = Visit(node.LocalVariable);
            Symbol list = _symbolTable.Lookup(node.List.Name, _currentBlock);

            Visit(node.Block);

            if (!isList(list.Type))
                throw new Exception();

            if (getListType(list.Type) != localVarType)
                throw new Exception();

            return null;
        }

        public override TypeEnum? Visit(ListOperationNode node)
        {
            switch (node)
            {
                case ListAddNode listAddNode:
                    Visit(listAddNode);
                    break;
                case ListDeleteNode listDeleteNode:
                    Visit(listDeleteNode);
                    break;
                case ListValueOfNode listValueOfNode:
                    Visit(listValueOfNode);
                    break;
                case ListIndexOfNode listIndexOfNode:
                    Visit(listIndexOfNode);
                    break;
                default:
                    throw new Exception();
            }

            return null;
        }

        public override TypeEnum? Visit(ListAddNode node)
        {
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (getListType(list.Type) != type)
                throw new Exception();

            return null;
        }

        public override TypeEnum? Visit(ListDeleteNode node)
        {
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (type != TypeEnum.number)
                throw new Exception();

            return null;
        }

        public override TypeEnum? Visit(ListValueOfNode node)
        {
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (type != TypeEnum.number)
                throw new Exception();

            return null;
        }

        public override TypeEnum? Visit(ListIndexOfNode node)
        {
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (getListType(list.Type) != type)
                throw new Exception();

            return null;
        }


        #region List helper functions
        private TypeEnum getListType(TypeEnum listType)
        {
            switch (listType)
            {
                case TypeEnum.list_number:
                    return TypeEnum.number;
                case TypeEnum.list_text:
                    return TypeEnum.text;
                case TypeEnum.list_boolean:
                    return TypeEnum.boolean;
                default:
                    throw new Exception();
            }
        }

        private bool isList(TypeEnum listType)
        {
            switch (listType)
            {
                case TypeEnum.list_number:
                case TypeEnum.list_text:
                case TypeEnum.list_boolean:
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}
