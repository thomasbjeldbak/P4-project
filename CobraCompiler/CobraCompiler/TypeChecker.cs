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

        internal void visitProgramNode(ProgramNode node)
        {
            _currentBlock = node;

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        visitDeclarationNode(declarationNode);
                        break;
                    case StatementNode statementNode:
                        visitStatementNode(statementNode);
                        break;
                    case AssignNode assignNode:
                        visitAssignNode(assignNode);
                        break;
                    default:
                        throw new Exception($"Command wasn't valid");
                }
                
            }
        }

        internal void visitDeclarationNode(DeclarationNode node)
        {
            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum exprNode = visitExpressionNode(node.Expression);

            if (symbol.Type != exprNode)
                throw new Exception($"Initialization of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
        }

        internal void visitStatementNode(StatementNode node)
        {

        }

        internal void visitAssignNode(AssignNode node)
        {

        }

        internal TypeEnum visitExpressionNode(ExpressionNode node)
        {
            TypeEnum type;
            switch (node)
            {
                case InfixExpressionNode infixExpressionNode:
                    type = visitInfixExpression(infixExpressionNode);
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

        internal TypeEnum visitInfixExpression(InfixExpressionNode node)
        {
            TypeEnum type;
            switch (node)
            {
                case AdditionNode additionNode:
                    type = visitAdditionNode(additionNode);
                    break;
                case SubtractionNode subtractionNode:
                    type = visitSubtractionNode(subtractionNode);
                    break;
                case MultiplicationNode multiplicationNode:
                    type = visitMultiplicationNode(multiplicationNode);
                    break;
                case DivisionNode divideNode:
                    type = visitDivisionNode(divideNode);
                    break;
                case AndNode andNode:
                    type = visitAndNode(andNode);
                    break;
                case OrNode orNode:
                    type = visitOrNode(orNode);
                    break;
                case EqualNode equalNode:
                    type = visitEqualNode(equalNode);
                    break;
                case NotEqualNode notEqualNode:
                    type = visitNotEqualNode(notEqualNode);
                    break;
                case GreaterNode greaterNode:
                    type = visitGreaterNode(greaterNode);
                    break;
                case LessNode lessNode:
                    type = visitLessNode(lessNode);
                    break;
                case GreaterEqualNode greaterEqualNode:
                    type = visitGreaterEqualNode(greaterEqualNode);
                    break;
                case LessEqualNode lessEqualNode:
                    type = visitLessEqualNode(lessEqualNode);
                    break;
                default:
                    throw new Exception();
            }

            return type;
        }

        internal TypeEnum visitAdditionNode(AdditionNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitSubtractionNode(SubtractionNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitMultiplicationNode(MultiplicationNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitDivisionNode(DivisionNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitAndNode(AndNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitOrNode(OrNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitEqualNode(EqualNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.list)
                throw new Exception();

            return type;
        }

        internal TypeEnum visitNotEqualNode(NotEqualNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = leftType;

            if (type == TypeEnum.list)
                throw new Exception();

            return type;
        }

        internal TypeEnum visitGreaterNode(GreaterNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitLessNode(LessNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitGreaterEqualNode(GreaterEqualNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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

        internal TypeEnum visitLessEqualNode(LessEqualNode node)
        {
            TypeEnum leftType = visitExpressionNode(node.Left);
            TypeEnum rightType = visitExpressionNode(node.Right);
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
