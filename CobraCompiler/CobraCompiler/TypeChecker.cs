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

        internal void VisitProgramNode(ProgramNode node)
        {
            _currentBlock = node;

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

        private void VisitDeclarationNode(DeclarationNode node)
        {
            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum exprNode = VisitExpressionNode(node.Expression);

            if (symbol.Type != exprNode)
                throw new Exception($"Initialization of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
        }

        internal void VisitStatementNode(StatementNode node)
        {

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
                    type = VisitInfixExpression(infixExpressionNode);
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

        private TypeEnum VisitInfixExpression(InfixExpressionNode node)
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

        private TypeEnum VisitAdditionNode(AdditionNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.boolean) 
                throw new Exception();

            if (type == TypeEnum.list)
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

            if (type == TypeEnum.list)
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

            if (type == TypeEnum.list)
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

            if (type == TypeEnum.list)
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

            if (type == TypeEnum.number)
                throw new Exception();

            if (type == TypeEnum.list)
                throw new Exception();

            if (type == TypeEnum.text)
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

            if (type == TypeEnum.number)
                throw new Exception();

            if (type == TypeEnum.list)
                throw new Exception();

            if (type == TypeEnum.text)
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

            if (type == TypeEnum.list)
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

            if (type == TypeEnum.list)
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

            if (type == TypeEnum.list)
                throw new Exception();

            if (type == TypeEnum.text)
                throw new Exception();

            if (type == TypeEnum.boolean)
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

            if (type == TypeEnum.list)
                throw new Exception();

            if (type == TypeEnum.text)
                throw new Exception();

            if (type == TypeEnum.boolean)
                throw new Exception();

            return type;
        }

        private TypeEnum VisitGreaterEqualNode(GreaterEqualNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.list)
                throw new Exception();

            if (type == TypeEnum.text)
                throw new Exception();

            if (type == TypeEnum.boolean)
                throw new Exception();

            return type;
        }

        private TypeEnum VisitLessEqualNode(LessEqualNode node)
        {
            TypeEnum leftType = VisitExpressionNode(node.Left);
            TypeEnum rightType = VisitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.list)
                throw new Exception();

            if (type == TypeEnum.text)
                throw new Exception();

            if (type == TypeEnum.boolean)
                throw new Exception();

            return type;
        }
    }
}
