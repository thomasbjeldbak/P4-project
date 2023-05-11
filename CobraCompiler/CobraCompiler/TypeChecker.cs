using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ASTNodes;

namespace CobraCompiler
{
    internal class TypeChecker : ASTVisitor<TypeNode?>
    {
        //The TypeChecker Needs the symbolTable to lookUp the types of the variables
        private readonly SymbolTable _symbolTable;
        private ErrorHandler typeErrorhandler;
        //Each time we enter a scope, we update the current block
        private BlockNode _currentBlock;

        public TypeChecker(SymbolTable symbolTable, ErrorHandler errorHandler) 
        {
            _symbolTable = symbolTable;
            typeErrorhandler = errorHandler;
        }

        public override TypeNode? Visit(ProgramNode node)
        {
            _currentBlock = node;

            if (node.Commands == null)
            {
                _currentBlock = node;
                return null;
            }

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
                _currentBlock = node;
            }
            _currentBlock = node;
            return null;
        }
        //BlockNode -> Commands
        public override TypeNode? Visit(BlockNode node)
        {
            //Visits all of it's commands
            _currentBlock = node;

            if (node.Commands == null)
            {
                _currentBlock = node;
                return null;
            }

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case ReturnNode returnNode:
                        return Visit(returnNode);
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
                _currentBlock = node;
            }
            _currentBlock = node;
            return null;
        }

        //DeclarationNode -> IdentifierNode, Expression
        public override TypeNode? Visit(DeclarationNode node)
        {
            //Gets symbol for identifier & Visits expression
            //Check if Identifier Type matches expression

            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            if (node.Expression != null)
            {
                TypeNode? exprNode = Visit(node.Expression);

                if (exprNode != null && symbol.Type != exprNode.Type)
                {
                    TypeError(node, $"Initialization of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
                }

                if (exprNode != null && isList(symbol.Type))
                {
                    TypeError(node, $"Initialization of type 'list' must occur on a seperate line from assignment");
                }

                return exprNode;
            }

            return getTypeNode(symbol.Type);

        }

        public override TypeNode? Visit(StatementNode node)
        {
            //Visits based on the type of StatementNode

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
                case ListOprStatementNode listOprStatementNode:
                    Visit(listOprStatementNode);
                    break;
                case ForeachNode foreachNode:
                    Visit(foreachNode);
                    break;
                case CommentNode commentNode:
                    Visit(commentNode);
                    break;
                case FunctionDeclarationNode functionDeclarationNode:
                    Visit(functionDeclarationNode);
                    break;
                case InputStmtNode inputStmtNode:
                    Visit(inputStmtNode);
                    break;
                case OutputStmtNode outputStmtNode:
                    Visit(outputStmtNode);
                    break;
                case FunctionCallStmtNode functionCallStmtNode:
                    Visit(functionCallStmtNode);
                    break;
            }
            return null;
        }

        //AssignNode -> Identifier, Expression
        public override TypeNode? Visit(AssignNode node)
        {
            //Gets symbol for identifier & Visits expression
            //Check if Identifier Type matches expression

            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            TypeNode? exprNode = Visit(node.Expression);

            if (exprNode == null)
                return null;

            if (symbol.Type != exprNode.Type)
            {
                TypeError(node, $"Assignment of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
            }

            return exprNode;
        }

        public override TypeNode? Visit(ExpressionNode node)
        {
            //Visits based on the type of ExpressionNode

            TypeNode? type = null;
            switch (node)
            {
                case InfixExpressionNode infixExpressionNode:
                    type = Visit(infixExpressionNode);
                    break;
                case IdentifierNode identifierNode:
                    Symbol symbol = _symbolTable.Lookup(identifierNode.Name, _currentBlock);
                    var declarationNode = ((DeclarationNode)symbol.Reference);
                    type = getTypeNode(symbol.Type);
                    switch (type)
                    {
                        case NumberNode typeNum when declarationNode.Expression is NumberNode expr:
                            typeNum.Value = expr.Value;
                            break;
                        case DecimalNode typeDec when declarationNode.Expression is DecimalNode expr:
                            typeDec.Value = expr.Value;
                            break;
                        case TextNode typeText when declarationNode.Expression is TextNode expr:
                            typeText.Value = expr.Value;
                            break;
                        case BooleanNode typeBool when declarationNode.Expression is BooleanNode expr:
                            typeBool.Value = expr.Value;
                            break;
                        case ListNode typeBool when declarationNode.Expression is ListNode expr:
                            typeBool.Value = expr.Value;
                            break;
                    }
                    break;
                case ListOprExpressionNode listOprExpressionNode:
                    type = Visit(listOprExpressionNode);
                    break;
                case NumberNode numberNode:
                    type = numberNode; 
                    break;
                case TextNode textNode:
                    type = textNode; 
                    break;
                case BooleanNode booleanNode:
                    type = booleanNode;
                    break;
                case ListNode listNode:
                    type = listNode;
                    break;
                case DecimalNode decimalNode:
                    type = decimalNode;
                    break;
                case InputExprNode inputExprNode:
                    type = Visit(inputExprNode);
                    break;
                case OutputExprNode outputExprNode:
                    type = Visit(outputExprNode);
                    break;
                case FunctionCallExprNode functionCallExprNode:
                    type = Visit(functionCallExprNode);
                    break;
                default:
                    return null;
            }

            return type;
        }

        public override TypeNode? Visit(InfixExpressionNode node)
        {
            //Visits based on the type of InfixExpressionNode

            TypeNode? type;
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

        //AdditionNode -> Left, Right
        public override TypeNode? Visit(AdditionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Addition

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);
            
            if (leftType.Type == TypeEnum.boolean || rightType.Type == TypeEnum.boolean) 
            {
                TypeError(node, $"Addition of type 'boolean' is not allowed.");
                return null;
            }
            else if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Addition of type 'list' is not allowed.");
                return null;
            }
            else if ((leftType.Type == TypeEnum.text && rightType.Type != TypeEnum.text) ||
                     (leftType.Type != TypeEnum.text && rightType.Type == TypeEnum.text))
            {
                TypeError(node, $"type 'text' can only be added with another value of type 'text.");
                return null;
            }

            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new NumberNode() { Value = left.Value + right.Value };
                case TextNode left when rightType is TextNode right:
                    return new TextNode() { Value = left.Value + right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new DecimalNode() { Value = left.Value + right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new DecimalNode() { Value = left.Value + right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new DecimalNode() { Value = left.Value + right.Value };
                default:
                    throw new Exception();
            }
        }

        //SubtractionNode -> Left, Right
        public override TypeNode? Visit(SubtractionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Subtraction

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (leftType.Type == TypeEnum.boolean || rightType.Type == TypeEnum.boolean)
            {
                TypeError(node, $"Subtraction of type 'boolean' is not allowed.");
                return null;
            }
            else if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Subtraction of type 'list' is not allowed.");
                return null;
            }
            else if (leftType.Type == TypeEnum.text || rightType.Type == TypeEnum.text)
            {
                TypeError(node, $"Subtraction of type 'text' is not allowed.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new NumberNode() { Value = left.Value - right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new DecimalNode() { Value = left.Value - right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new DecimalNode() { Value = left.Value - right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new DecimalNode() { Value = left.Value - right.Value };
                default:
                    throw new Exception();
            }
        }

        //MultiplicationNode -> Left, Right
        public override TypeNode? Visit(MultiplicationNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Multiplication

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (leftType.Type == TypeEnum.boolean || rightType.Type == TypeEnum.boolean)
            {
                TypeError(node, $"Multiplication of type 'boolean' is not allowed.");
                return null;
            }
            else if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Multiplication of type 'list' is not allowed.");
                return null;
            }
            else if (leftType.Type == TypeEnum.text || rightType.Type == TypeEnum.text)
            {
                TypeError(node, $"Multiplication of type 'text' is not allowed.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new NumberNode() { Value = left.Value * right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new DecimalNode() { Value = left.Value * right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new DecimalNode() { Value = left.Value * right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new DecimalNode() { Value = left.Value * right.Value };
                default:
                    throw new Exception();
            }
        }

        //DivisionNode -> Left, Right
        public override TypeNode? Visit(DivisionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Division

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);
            TypeNode type;

            if (leftType.Type == TypeEnum.boolean || rightType.Type == TypeEnum.boolean)
            {
                TypeError(node, $"Division of type 'boolean' is not allowed.");
                return null;
            }
            else if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Division of type 'list' is not allowed.");
                return null;
            }
            else if (rightType.Value == 0)
            {
                TypeError(node, $"Dividing by 0 is not allowed.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new DecimalNode() { Value = left.Value / right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new DecimalNode() { Value = left.Value / right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new DecimalNode() { Value = left.Value / right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new DecimalNode() { Value = left.Value / right.Value };
                default:
                    throw new Exception();
            }
        }

        //AndNode -> Left, Right
        public override TypeNode? Visit(AndNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for AndNode

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (leftType.Type != TypeEnum.boolean)
            {
                TypeError(node, $"Type '{leftType}' does not match type 'boolean' on the left hand side of the logic 'and' expression.");
                return null;
            }
            if (rightType.Type != TypeEnum.boolean)
            {
                TypeError(node, $"Type '{rightType}' does not match type 'boolean' on the right hand side of the logic 'and' expression.");
                return null;
            }
            switch (leftType)
            {
                case BooleanNode left when rightType is BooleanNode right:
                    return new BooleanNode() { Value = left.Value && right.Value };
                default:
                    throw new Exception();
            }
        }

        //OrNode -> Left, Right
        public override TypeNode? Visit(OrNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for OrNode

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (leftType.Type != TypeEnum.boolean)
            {
                TypeError(node, $"Type '{leftType}' does not match type 'boolean' on the left hand side of the logic 'or' expression.");
                return null;
            }
            if (rightType.Type != TypeEnum.boolean)
            {
                TypeError(node, $"Type '{leftType}' does not match type 'boolean' on the right hand side of the logic 'or' expression.");
                return null;
            }
            switch (leftType)
            {
                case BooleanNode left when rightType is BooleanNode right:
                    return new BooleanNode() { Value = left.Value || right.Value };
                default:
                    throw new Exception();
            }
        }

        //EqualNode -> Left, Right
        public override TypeNode? Visit(EqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for EqualNode

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if ((leftType.Type == TypeEnum.boolean && rightType.Type != TypeEnum.boolean) ||
                     (leftType.Type != TypeEnum.boolean && rightType.Type == TypeEnum.boolean))
            {
                TypeError(node, $"Type 'boolean' can only use 'equal' with another value of type 'boolean'.");
                return null;
            }
            else if ((leftType.Type == TypeEnum.text && rightType.Type != TypeEnum.text) ||
                    (leftType.Type != TypeEnum.text && rightType.Type == TypeEnum.text))
            {
                TypeError(node, $"Type 'text' can only use 'equal' with another value of type 'text'.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value == right.Value };
                case TextNode left when rightType is TextNode right:
                    return new BooleanNode() { Value = left.Value == right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value == right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value == right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value == right.Value };
                default:
                    throw new Exception();
            }
        }

        //NotEqualNode -> Left, Right
        public override TypeNode? Visit(NotEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for NotEqualNode

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if ((leftType.Type == TypeEnum.boolean && rightType.Type != TypeEnum.boolean) ||
                     (leftType.Type != TypeEnum.boolean && rightType.Type == TypeEnum.boolean))
            {
                TypeError(node, $"Type 'boolean' can only use 'not equal' with another value of type 'boolean'.");
                return null;
            }
            else if ((leftType.Type == TypeEnum.text && rightType.Type != TypeEnum.text) ||
                    (leftType.Type != TypeEnum.text && rightType.Type == TypeEnum.text))
            {
                TypeError(node, $"Type 'text' can only use 'not equal' with another value of type 'text'.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value != right.Value };
                case TextNode left when rightType is TextNode right:
                    return new BooleanNode() { Value = left.Value != right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value != right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value != right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value != right.Value };
                default:
                    throw new Exception();
            }
        }

        //GreaterNode -> Left, Right
        public override TypeNode? Visit(GreaterNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for GreaterNode

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if (leftType.Type == TypeEnum.boolean || rightType.Type == TypeEnum.boolean)
            {
                TypeError(node, $"Type 'boolean' is not allowed in a 'greater' expression.");
                return null;
            }
            else if (leftType.Type == TypeEnum.text || rightType.Type == TypeEnum.text)
            {
                TypeError(node, $"Type 'text' is not allowed in a 'greater' expression.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value > right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value > right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value > right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value > right.Value };
                default:
                    throw new Exception();
            }
        }

        //LessNode -> Left, Right
        public override TypeNode? Visit(LessNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for LessNode

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if (leftType.Type == TypeEnum.boolean || rightType.Type == TypeEnum.boolean)
            {
                TypeError(node, $"Type 'boolean' is not allowed in a 'less' expression.");
                return null;
            }
            else if (leftType.Type == TypeEnum.text || rightType.Type == TypeEnum.text)
            {
                TypeError(node, $"Type 'text' is not allowed in a 'less' expression.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value < right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value < right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value < right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value < right.Value };
                default:
                    throw new Exception();
            }
        }

        //GreaterEqualNode -> Left, Right
        public override TypeNode? Visit(GreaterEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for EqualNode

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if (leftType.Type == TypeEnum.boolean || rightType.Type == TypeEnum.boolean)
            {
                TypeError(node, $"Type 'boolean' is not allowed in a 'greater or equal' expression.");
                return null;
            }
            else if (leftType.Type == TypeEnum.text || rightType.Type == TypeEnum.text)
            {
                TypeError(node, $"Type 'text' is not allowed in a 'greater or equal' expression.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value >= right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value >= right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value >= right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value >= right.Value };
                default:
                    throw new Exception();
            }
        }

        //LessEqualNode -> Left, Right
        public override TypeNode? Visit(LessEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for LessEqualNode

            TypeNode? leftType = Visit(node.Left);
            TypeNode? rightType = Visit(node.Right);

            if (isList(leftType.Type) || isList(rightType.Type))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if (leftType.Type == TypeEnum.boolean || rightType.Type == TypeEnum.boolean)
            {
                TypeError(node, $"Type 'boolean' is not allowed in a 'less or equal' expression.");
                return null;
            }
            else if (leftType.Type == TypeEnum.text || rightType.Type == TypeEnum.text)
            {
                TypeError(node, $"Type 'text' is not allowed in a 'less or equal' expression.");
                return null;
            }
            switch (leftType)
            {
                case NumberNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value <= right.Value };
                case DecimalNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value <= right.Value };
                case DecimalNode left when rightType is NumberNode right:
                    return new BooleanNode() { Value = left.Value <= right.Value };
                case NumberNode left when rightType is DecimalNode right:
                    return new BooleanNode() { Value = left.Value <= right.Value };
                default:
                    throw new Exception();
            }
        }

        #endregion

        //IfNode -> Condition, Block, ElseIfs
        public override TypeNode? Visit(IfNode node)
        {
            //Visit Condition & Block
            //If any - visit ElseIfs and/Or Else
            //Check if Condition type is boolean

            TypeNode? type = Visit(node.Condition);
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

            if (type.Type != TypeEnum.boolean)
            {
                TypeError(node, $"Only boolean expression is allowed in an 'if' condition");
            }

            return null;
        }

        //ElseIfNode -> Condition, Block
        public override TypeNode? Visit(ElseIfNode node)
        {
            //Visit Condition & Block
            //Check if Condition type is boolean

            TypeNode? type = Visit(node.Condition); 
            Visit(node.Block);

            if (type.Type != TypeEnum.boolean)
            {
                TypeError(node, $"Only boolean expression is allowed in an 'else if' condition");
            }

            return null;
        }

        //ElseNode -> Block
        public override TypeNode? Visit(ElseNode node)
        {
            Visit(node.Block);
            return null;
        }

        //RepeatNode -> Expression, Block
        public override TypeNode? Visit(RepeatNode node)
        {
            //Visit Expression & Block
            //Check if Expression type is Number
            TypeNode? type = Visit(node.Expression);
            Visit(node.Block);

            if (type.Type != TypeEnum.number)
            {
                TypeError(node, "The repeat condition is type '{type}', but has to be a number");
            }

            return null;
        }

        //WhileNode -> Condition, Block
        public override TypeNode? Visit(WhileNode node)
        {
            //Visit Condition & Block
            //Check if Condition type is boolean

            TypeNode? type = Visit(node.Condition);
             Visit(node.Block);

            if (type.Type != TypeEnum.boolean)
            {
                TypeError(node, $"The while expression is type '{type}', but has to be a boolean");
            }

            return null;
        }

        //ForeachNode -> DeclarationNode, IdentifierNode, Block
        public override TypeNode? Visit(ForeachNode node)
        {
            //Visit LocalVariable & Block
            //Get symbol for Identifier in symboltable
            //Check if identifier is list
            //Check if list inner type matches Local Variable type

            Symbol? list = _symbolTable.Lookup(node.List.Name, _currentBlock);
            TypeNode? localVarType = node.Block.LocalVariable.Identifier.TypeNode;

            Visit(node.Block);

            if (!isList(list.Type))
            {
                TypeError(node, $"'{list.Name}' is not a list");
            } 
            else if (getListType(list.Type) != localVarType.Type)
            {
                TypeError(node, $"For each local variable type error. Expects type '{getListType(list.Type)}'");
            }

            return null;
        }
        
        public override TypeNode? Visit(ListOprStatementNode node)
        {
            //Visits based on type of ListOprStatementNode

            switch (node)
            {
                case ListAddNode listAddNode:
                    Visit(listAddNode);
                    break;
                case ListReplaceNode listReplaceNode:
                    Visit(listReplaceNode);
                    break;
            }
            return null;
        }

        public override TypeNode? Visit(ListOprExpressionNode node)
        {
            //Visits based on type of ListOprExpressionNode

            switch (node)
            {
                case ListValueOfNode listValueOfNode:
                    return Visit(listValueOfNode);
                case ListIndexOfNode listIndexOfNode:
                    return Visit(listIndexOfNode);
                default:
                    throw new Exception();
            }
        }


        //ListAddNode -> IdentifierNode, Expression
        public override TypeNode? Visit(ListAddNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if list inner type matches Expression

            Symbol? sym = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            if (!isList(sym.Type))
            {
                TypeError(node, $"'{sym.Name}' is not a list");
                return null;
            }

            var declaration = (DeclarationNode)sym.Reference;
            var list = (ListNode)declaration.Identifier.TypeNode;

            if (node.Arguments.Expressions.Count == 0)
            {
                TypeError(node, $"'{sym.Name}:Add()' requires atleast one argument");
                return null;
            }

            List<TypeNode> arguments = new List<TypeNode>();
            foreach (var arg in node.Arguments.Expressions)
            {
                TypeNode argument = Visit(arg);

                if (argument == null || argument.Type != getListType(sym.Type))
                {
                    TypeError(node, $"'{sym.Name}:Add()' expects type '{getListType(sym.Type)}'");
                    return null;
                }
                arguments.Add(argument);

            }

            var functionBlock = _symbolTable.GetFunctionBlock(_currentBlock);
            if (functionBlock != null && functionBlock.IsFunctionCall)
            {
                list.Value.AddRange(arguments);
                list.Size += (ushort)arguments.Count;
            }

            return null;
        }

        //ListReplaceNode -> IdentifierNode, Expression
        public override TypeNode? Visit(ListReplaceNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if expression is a number

            Symbol? sym = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            TypeNode argument1 = null;
            TypeNode argument0 = null;

            if (!isList(sym.Type))
            {
                TypeError(node, $"'{sym.Name}' is not a list");
                return null;
            }

            var declaration = (DeclarationNode)sym.Reference;
            var list = (ListNode)declaration.Identifier.TypeNode;

            if (node.Arguments.Expressions.Count != 2)
            {
                TypeError(node, $"'{sym.Name}':Replace() requires two argument");
                return null;
            }

            var expression1 = node.Arguments.Expressions[1];
            argument1 = Visit(expression1);

            if (argument1.Type != TypeEnum.number)
            {
                TypeError(node, $"'{sym.Name}:Replace()' expects type '{TypeEnum.number}'");
                return null;
            }

            var expression0 = node.Arguments.Expressions[0];
            argument0 = Visit(expression0);

            if (argument0.Type != getListType(list.Type))
            {
                TypeError(node, $"'{sym.Name}:Replace()' expects type '{getListType(sym.Type)}'");
                return null;
            }

            var functionBlock = _symbolTable.GetFunctionBlock(_currentBlock);
            if (functionBlock != null && functionBlock.IsFunctionCall)
            {
                int index = ((NumberNode)argument1).Value;

                if (index > list.Size - 1 || index < 0)
                {
                    TypeError(node, $"'{sym.Name}' has no value at index '{index}'");
                    return null;
                }

                if (!list.Value.Any(x => x.Value == argument0.Value))
                {
                    TypeError(node, $"'{sym.Name}' doesn't contain '{argument0.Value}'");
                    return null;
                }

                list.Value[index] = argument0;
            }

            return null;
        }

        //ListValueOfNode -> IdentifierNode, Expression
        public override TypeNode? Visit(ListValueOfNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symbol table
            //Check if the identifier is a list
            //Check if Expression is type number

            Symbol? sym = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeNode argument0 = null;

            if (!isList(sym.Type))
            {
                TypeError(node, $"'{sym.Name}' is not a list");
                return null;
            }

            var declaration = (DeclarationNode)sym.Reference;
            var list = (ListNode)declaration.Identifier.TypeNode;

            if (node.Arguments.Expressions.Count != 1)
            {
                TypeError(node, $"'{sym.Name}':ValueOf() requires one argument");
                return null;
            }

            var expression0 = node.Arguments.Expressions[0];
            argument0 = Visit(expression0);

            if (argument0.Type != TypeEnum.number)
            {
                TypeError(node, $"'{sym.Name}:Add()' expects type '{TypeEnum.number}'");
                return null;
            }

            var functionBlock = _symbolTable.GetFunctionBlock(_currentBlock);
            if (functionBlock != null && functionBlock.IsFunctionCall)
            {
                int index = ((NumberNode)argument0).Value;

                if (index > list.Size - 1 || index < 0)
                {
                    TypeError(node, $"'{sym.Name}' has no value at index '{index}'");
                    return null;
                }

                return list.Value[index];
            }

            return null;
        }

        //ListIndexOfNode -> IdentifierNode, Expression
        public override TypeNode? Visit(ListIndexOfNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if list inner type matches Expression

            Symbol? sym = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeNode argument0 = null;

            if (!isList(sym.Type))
            {
                TypeError(node, $"'{sym.Name}' is not a list");
                return null;
            }

            var declaration = (DeclarationNode)sym.Reference;
            var list = (ListNode)declaration.Identifier.TypeNode;

            if (node.Arguments.Expressions.Count != 1)
            {
                TypeError(node, $"'{sym.Name}':ValueOf() requires one argument");
                return null;
            }

            var expression0 = node.Arguments.Expressions[0];
            argument0 = Visit(expression0);

            if (argument0.Type != TypeEnum.number)
            {
                TypeError(node, $"'{sym.Name}:Add()' expects type '{TypeEnum.number}'");
                return null;
            }

            var functionBlock = _symbolTable.GetFunctionBlock(_currentBlock);
            if (functionBlock != null && functionBlock.IsFunctionCall)
            {
                if (list.Size > 0)
                {
                    TypeError(node, $"'{sym.Name}' is empty");
                    return null;
                }

                if (!list.Value.Any(x => x.Value == argument0.Value))
                {
                    TypeError(node, $"'{sym.Name}' doesn't contain '{argument0.Value}'");
                    return null;
                }

                NumberNode number = new NumberNode();
                number.Value = list.Value.FindIndex(x => x.Value == argument0.Value);
                return number;

            }

            return null;
        }

        public override TypeNode? Visit(CommentNode node)
        {
            return null;
        }

        public override TypeNode? Visit(FunctionCallExprNode node)
        {
            Symbol? sym = _symbolTable.Lookup(node.Name, _currentBlock);
            FunctionDeclarationNode declaration = (FunctionDeclarationNode)sym.Reference;

            List<DeclarationNode> parameters = declaration.Block.Parameters.Declarations;
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (parameters.Count != arguments.Count)
            {
                TypeError(node, $"{sym.Name} expects {parameters.Count} arguments");
                return null;
            }

            for (int i = 0; i < parameters.Count; i++)
            {
                Symbol? paramSymbol = _symbolTable.Lookup(parameters[i].Identifier.Name, declaration.Block);
                TypeEnum argType = TypeEnum.nothing;

                if (arguments[i] is IdentifierNode)
                    argType = _symbolTable.Lookup(((IdentifierNode)arguments[i]).Name, _currentBlock).Type;
                else
                    argType = Visit(arguments[i]).Type;

                if (paramSymbol.Type != argType)
                {
                    TypeError(node, $"{sym.Name} parameter of type {paramSymbol.Type} doesn't match argument of type {argType}.");
                    return null;
                }
            }

            declaration.Block.Arguments = new ArgumentsNode();
            declaration.Block.Arguments.Expressions = arguments;

            declaration.Block.IsFunctionCall = true;

            var block = Visit(declaration.Block);

            declaration.Block.IsFunctionCall = false;
            return block;
        }

        public override TypeNode? Visit(InputExprNode node)
        {
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (arguments.Count != 0)
            {
                TypeError(node, $"'input()' takes 0 arguments");
            }

            string input = Console.ReadLine();

            switch (node.Type)
            {
                case NumberNode numberNode:
                    int number = 0;
                    if (!int.TryParse(input, out number))
                    {
                        TypeError(node, $"'input()' Expected of type {numberNode.Type}");
                        return null;
                    }
                    numberNode.Value = number;
                    return numberNode;
                case DecimalNode decimalNode:
                    float _decimal = 0;
                    if (!float.TryParse(input, CultureInfo.InvariantCulture, out _decimal))
                    {
                        TypeError(node, $"'input()' Expected type {decimalNode.Type}");
                        return null;
                    }
                    decimalNode.Value = _decimal;
                    return decimalNode;
                case BooleanNode boolNode:
                    bool boolean = false;
                    if (!bool.TryParse(input, out boolean))
                    {
                        TypeError(node, $"'input()' Expected type {boolNode.Type}");
                        return null;
                    }
                    boolNode.Value = boolean;
                    return boolNode;
                case TextNode textNode:
                    textNode.Value = input;
                    return textNode;
                default:
                    TypeError(node, "Invalid typing for 'input()'");
                    return null;
            }
        }

        public override TypeNode? Visit(OutputExprNode node)
        {
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (arguments.Count != 1)
            {
                TypeError(node, $"'output()' takes 1 arguments");
            }

            var expression0 = arguments[0];
            TypeNode argument0 = Visit(expression0);

            if (argument0 == null)
                return null;

            if (isList(argument0.Type))
            {
                TypeError(node, $"'output()' does not support type {getTypeNode(argument0.Type)}");
            }

            return null;
        }

        public override TypeNode? Visit(FunctionDeclarationNode node)
        {
            var block = Visit(node.Block);

            TypeEnum blockType = TypeEnum.nothing;

            if (block != null)
                blockType = block.Type;

            List<string> reservedFunctionNames = new List<string>()
            {
                "concat", "AddToList", "ReplaceInList", "IndexOfList", "ValueOfList"
            };

            if (node.ReturnType.Type == TypeEnum.nothing && blockType != TypeEnum.nothing)
            {
                TypeError(node, $"function '{node.Name}()' expects no return statement");
                return null;
            }
            else if (node.ReturnType.Type != TypeEnum.nothing && node.ReturnType.Type != blockType)
            {
                TypeError(node, $"function '{node.Name}()' expects to return type {node.ReturnType.Type} but returns type {blockType}");
                return null;
            }
            else if (reservedFunctionNames.Contains(node.Name))
            {
                TypeError(node, $"function '{node.Name}()' is a reserved name");
                return null;
            }

            return block;
        }

        public override TypeNode? Visit(FunctionCallStmtNode node)
        {
            Symbol? sym = _symbolTable.Lookup(node.Name, _currentBlock);
            FunctionDeclarationNode declaration = ((FunctionDeclarationNode)sym.Reference);

            List<DeclarationNode> parameters = declaration.Block.Parameters.Declarations;
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (parameters.Count != arguments.Count)
            {
                TypeError(node, $"{sym.Name} expects {parameters.Count} arguments");
                return null;
            }

            for (int i = 0; i < parameters.Count; i++)
            {
                Symbol? paramSymbol = _symbolTable.Lookup(parameters[i].Identifier.Name, declaration.Block);
                TypeEnum argType = TypeEnum.nothing;

                if (arguments[i] is IdentifierNode)
                    argType = _symbolTable.Lookup(((IdentifierNode)arguments[i]).Name, _currentBlock).Type;
                else
                    argType = Visit(arguments[i]).Type;

                if (paramSymbol.Type != argType)
                {
                    TypeError(node, $"{sym.Name} parameter of type {paramSymbol.Type} doesn't match argument of type {argType}.");
                    return null;
                }
            }

            declaration.Block.Arguments = new ArgumentsNode();
            declaration.Block.Arguments.Expressions = arguments;
            declaration.Block.IsFunctionCall = true;

            var block = Visit(declaration.Block);

            declaration.Block.IsFunctionCall = false;
            return block;
        }

        public override TypeNode? Visit(InputStmtNode node)
        {
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (arguments.Count != 0)
            {
                TypeError(node, $"'input()' takes 0 arguments");
            }

            string input = Console.ReadLine();

            switch (node.Type)
            {
                case NumberNode numberNode:
                    int number = 0;
                    if (!int.TryParse(input, out number))
                    {
                        TypeError(node, $"'input()' Expected type {numberNode.Type}");
                        return null;
                    }
                    numberNode.Value = number;
                    return numberNode;
                case DecimalNode decimalNode:
                    float _decimal = 0;
                    if (!float.TryParse(input, CultureInfo.InvariantCulture, out _decimal))
                    {
                        TypeError(node, $"'input()' Expected type {decimalNode.Type}");
                        return null;
                    }
                    decimalNode.Value = _decimal;
                    return decimalNode;
                case BooleanNode boolNode:
                    bool boolean = false;
                    if (!bool.TryParse(input, out boolean))
                    {
                        TypeError(node, $"'input()' Expected type {boolNode.Type}");
                        return null;
                    }
                    boolNode.Value = boolean;
                    return boolNode;
                case TextNode textNode:
                    textNode.Value = input;
                    return textNode;
                default:
                    TypeError(node, "Invalid typing for 'input()'");
                    return null;
            }
        }

        public override TypeNode? Visit(OutputStmtNode node)
        {
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (arguments.Count != 1)
            {
                TypeError(node, $"'output()' takes 1 arguments");
                return null;
            }

            var expression0 = arguments[0];
            TypeNode argument0 = Visit(expression0);

            if (argument0 == null)
                return null;

            if (isList(argument0.Type))
            {
                TypeError(node, $"'output()' does not support type {getTypeNode(argument0.Type)}");
                return null;
            }

            return null;
        }

        public override TypeNode? Visit(ReturnNode node)
        {
            return Visit(node.Expression);
        }

        public override TypeNode? Visit(FunctionBlockNode node)
        {
            _currentBlock = node;

            var parameters = node.Parameters.Declarations;
            TypeNode returnExpr;

            if (node.Arguments != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    var sym = _symbolTable.Lookup(parameters[i].Identifier.Name, _currentBlock);
                    var declarationSymbol = (DeclarationNode)sym.Reference;
                    declarationSymbol.Expression = Visit(node.Arguments.Expressions[i]);
                    parameters[i].Identifier = parameters[i].Identifier;

                    _currentBlock = node;
                }
            }

            if (node.Commands == null)
            {
                returnExpr = Visit(node.ReturnExpression);
                _currentBlock = node;
                return returnExpr;
            }

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                }
                _currentBlock = node;
            }

            returnExpr = Visit(node.ReturnExpression);
            _currentBlock = node;

            return returnExpr;
        }

        public override TypeNode? Visit(ForeachBlockNode node)
        {
            //Visits all of it's commands
            _currentBlock = node;

            if (node.Commands == null)
            {
                _currentBlock = node;
                return null;
            }

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case ReturnNode returnNode:
                        return Visit(returnNode);
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                    default:
                        throw new Exception($"Command was not valid");

                }
                _currentBlock = node;
            }
            _currentBlock = node;
            return null;
        }

        #region List helper functions

        //Takes TypeNode (list_[TYPE]) and returns the Type
        //e.g. list_number -> return number
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
                case TypeEnum.list_decimal:
                    return TypeEnum._decimal;
                default:
                    throw new Exception();
            }
        }

        //Checks if TypeNode is a list
        //e.g. list_number -> return true,
        //  number -> return false;
        private bool isList(TypeEnum? listType)
        {
            switch (listType)
            {
                case TypeEnum.list_number:
                case TypeEnum.list_text:
                case TypeEnum.list_boolean:
                case TypeEnum.list_decimal:
                    return true;
                default:
                    return false;
            }
        }
        #endregion

        private TypeNode getTypeNode(TypeEnum type)
        {
            switch (type)
            {
                case TypeEnum.number:
                    return new NumberNode();
                case TypeEnum._decimal:
                    return new DecimalNode();
                case TypeEnum.text:
                    return new TextNode();
                case TypeEnum.boolean:
                    return new BooleanNode();
                case TypeEnum.list_number:
                    return new ListNode(TypeEnum.number);
                case TypeEnum.list_decimal:
                    return new ListNode(TypeEnum._decimal);
                case TypeEnum.list_text:
                    return new ListNode(TypeEnum.text);
                case TypeEnum.list_boolean:
                    return new ListNode(TypeEnum.boolean);
                default:
                    throw new Exception();
            }
        }

        public void TypeError(ASTNode node, string error)
        {
            typeErrorhandler.TypeErrorMessages.Add($"Error line {node.Line}: {error}");
        }

    }
}
