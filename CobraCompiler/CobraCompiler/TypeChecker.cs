using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASTNodes;

namespace CobraCompiler
{
    internal class TypeChecker
    {
        private readonly SymbolTable _symbolTable;
        private BlockNode _currentBlock;

        public TypeChecker(SymbolTable symbolTable) 
        {
            _symbolTable = symbolTable;
        }

        internal void visitBlockNode(BlockNode node)
        {
            _currentBlock = node;

            if (node.Commands == null)
                return;

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        VisitDeclarationNode(declarationNode);
                        break;
                    case StatementNode statementNode:
                        VisitStatementNode(statementNode);
                        break;
                    case AssignNode assignNode:
                        VisitAssignNode(assignNode);
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
            }
        }

        internal TypeEnum visitDeclarationNode(DeclarationNode node)
        {
            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            if (node.Expression != null)
            {
                TypeEnum exprNode = visitExpressionNode(node.Expression);

                if (symbol.Type != exprNode)
                    throw new Exception($"Initialization of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
            }

            return symbol.Type;

        }

        internal void visitStatementNode(StatementNode node)
        {
            switch (node)
            {
                case IfNode ifNode:
                    visitIfNode(ifNode);
                    break;
                case RepeatNode repeatNode:
                    visitRepeatNode(repeatNode);
                    break;
                case WhileNode whileNode:
                    visitWhileNode(whileNode);
                    break;
                case ListOperationNode listOperationNode:
                    visitListOperationNode(listOperationNode);
                    break;
                default:
                    throw new Exception();
            }
        }

        internal void VisitAssignNode(AssignNode node)
        {

        }

        private TypeEnum VisitExpressionNode(ExpressionNode node)
        {
            TypeEnum type;
            switch (node)
            {
                case InfixExpressionNode infixExpressionNode:
                    type = visitInfixExpressionNode(infixExpressionNode);
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

        internal TypeEnum visitInfixExpressionNode(InfixExpressionNode node)
        {
            TypeEnum type;
            switch (node)
            {
                case AdditionNode additionNode:
                    type = VisitAdditionNode(additionNode);
                    break;
                case SubtractionNode subtractionNode:
                    type = VisitSubtractionNode(subtractionNode);
                    break;
                case MultiplicationNode multiplicationNode:
                    type = VisitMultiplicationNode(multiplicationNode);
                    break;
                case DivisionNode divideNode:
                    type = VisitDivisionNode(divideNode);
                    break;
                case AndNode andNode:
                    type = VisitAndNode(andNode);
                    break;
                case OrNode orNode:
                    type = VisitOrNode(orNode);
                    break;
                case EqualNode equalNode:
                    type = VisitEqualNode(equalNode);
                    break;
                case NotEqualNode notEqualNode:
                    type = VisitNotEqualNode(notEqualNode);
                    break;
                case GreaterNode greaterNode:
                    type = VisitGreaterNode(greaterNode);
                    break;
                case LessNode lessNode:
                    type = VisitLessNode(lessNode);
                    break;
                case GreaterEqualNode greaterEqualNode:
                    type = VisitGreaterEqualNode(greaterEqualNode);
                    break;
                case LessEqualNode lessEqualNode:
                    type = VisitLessEqualNode(lessEqualNode);
                    break;
                default:
                    throw new Exception();
            }

            return type;
        }
 
        #region Visit TypeNodes
        internal TypeEnum visitAdditionNode(AdditionNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.boolean) 
                throw new Exception();

            if (isList(type))
                throw new Exception();

            return type;
        }

        private TypeEnum VisitSubtractionNode(SubtractionNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.boolean)
                throw new Exception();

            if (isList(type))
                throw new Exception();

            return type;
        }

        private TypeEnum VisitMultiplicationNode(MultiplicationNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.boolean)
                throw new Exception();

            if (isList(type))
                throw new Exception();

            return type;
        }

        private TypeEnum VisitDivisionNode(DivisionNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.boolean)
                throw new Exception();

            if (isList(type))
                throw new Exception();

            return type;
        }

        private TypeEnum VisitAndNode(AndNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type != TypeEnum.boolean)
                throw new Exception();

            return type;
        }

        private TypeEnum VisitOrNode(OrNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type != TypeEnum.boolean)
                throw new Exception();

            return type;
        }

        private TypeEnum VisitEqualNode(EqualNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (isList(type))
                throw new Exception();

            return type;
        }

        private TypeEnum VisitNotEqualNode(NotEqualNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (isList(type))
                throw new Exception();

            return type;
        }

        private TypeEnum VisitGreaterNode(GreaterNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return type;
        }

        private TypeEnum VisitLessNode(LessNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return type;
        }

        internal TypeEnum visitGreaterEqualNode(GreaterEqualNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return type;
        }

        internal TypeEnum visitLessEqualNode(LessEqualNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return type;
        }

        #endregion

        internal void visitIfNode(IfNode node)
        {
            TypeEnum type = visitExpressionNode(node.Condition);
            
            if (node.Block != null)
                visitBlockNode(node.Block);

            foreach (var @else in node.ElseIfs)
            {
                switch (@else)
                {
                    case ElseIfNode elseIf:
                        visitElseIfNode(elseIf);
                        break;
                    case ElseNode:
                        visitElseNode(@else);
                        break;
                    default:
                        throw new Exception();
                }
            }

            if (type != TypeEnum.boolean)
                throw new Exception();
        }

        internal void visitElseIfNode(ElseIfNode node)
        {
            TypeEnum type = visitExpressionNode(node.Condition);
            
            if (node.Block != null)
                visitBlockNode(node.Block);

            if (type != TypeEnum.boolean)
                throw new Exception();
        }

        internal void visitElseNode(ElseNode node)
        {
            if (node.Block != null)
                visitBlockNode(node.Block);
        }
        
        internal void visitRepeatNode(RepeatNode node)
        {
            TypeEnum type = visitExpressionNode(node.Expression);
            
            if (node.Block != null)
                visitBlockNode(node.Block);

            if (type != TypeEnum.number)
                throw new Exception();
        }

        internal void visitWhileNode(WhileNode node)
        {
            TypeEnum type = visitExpressionNode(node.Condition);

            if (node.Block != null)
                visitBlockNode(node.Block);

            if (type != TypeEnum.boolean)
                throw new Exception();
        }

        internal void visitForeachNode(ForeachNode node)
        {
            TypeEnum localVarType = visitDeclarationNode(node.LocalVariable);
            Symbol list = _symbolTable.Lookup(node.List.Name, _currentBlock);

            visitBlockNode(node.Block);

            if (!isList(list.Type))
                throw new Exception();

            if (getListType(list.Type) != localVarType)
                throw new Exception();
        }

        internal void visitListOperationNode(ListOperationNode node)
        {
            switch (node)
            {
                case ListAddNode listAddNode:
                    visitListAddNode(listAddNode);
                    break;
                case ListDeleteNode listDeleteNode:
                    visitListDeleteNode(listDeleteNode);
                    break;
                case ListValueOfNode listValueOfNode:
                    visitListValueOfNode(listValueOfNode);
                    break;
                case ListIndexOfNode listIndexOfNode:
                    visitListIndexOfNode(listIndexOfNode);
                    break;
                default:
                    throw new Exception();
            }
        }

        internal void visitListAddNode(ListAddNode node)
        {
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum type = visitExpressionNode(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (getListType(list.Type) != type)
                throw new Exception();
        }

        internal void visitListDeleteNode(ListDeleteNode node)
        {
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum type = visitExpressionNode(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (type != TypeEnum.number)
                throw new Exception();
        }

        internal void visitListValueOfNode(ListValueOfNode node)
        {
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum type = visitExpressionNode(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (type != TypeEnum.number)
                throw new Exception();
        }

        internal void visitListIndexOfNode(ListIndexOfNode node)
        {
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum type = visitExpressionNode(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (getListType(list.Type) != type)
                throw new Exception();
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
                    throw new Exception();
            }
        }

        #endregion
    }
}
