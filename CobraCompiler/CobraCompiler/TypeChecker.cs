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
    internal class TypeChecker : ASTVisitor<TypeEnum?>
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

        public override TypeEnum? Visit(ProgramNode node)
        {
            _currentBlock = node;

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
        public override TypeEnum? Visit(BlockNode node)
        {
            //Visits all of it's commands
            _currentBlock = node;

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
        public override TypeEnum? Visit(DeclarationNode node)
        {
            //Gets symbol for identifier & Visits expression
            //Check if Identifier Type matches expression

            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            if (node.Expression != null)
            {
                TypeEnum? exprNode = Visit(node.Expression);

                if (exprNode != null && symbol.Type != exprNode)
                {
                    TypeError(node, $"Initialization of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
                }

                if (exprNode != null && isList(symbol.Type))
                {
                    TypeError(node, $"Initialization of type 'list' must occur on a seperate line from assignment");
                }

                return exprNode;
            }

            return symbol.Type;

        }

        public override TypeEnum? Visit(StatementNode node)
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
        public override TypeEnum? Visit(AssignNode node)
        {
            //Gets symbol for identifier & Visits expression
            //Check if Identifier Type matches expression

            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            TypeEnum? exprNode = Visit(node.Expression);

            if (symbol.Type != exprNode)
            {
                TypeError(node, $"Assignment of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
            }

            return exprNode;
        }

        public override TypeEnum? Visit(ExpressionNode node)
        {
            //Visits based on the type of ExpressionNode

            TypeEnum? type = null;
            switch (node)
            {
                case InfixExpressionNode infixExpressionNode:
                    type = Visit(infixExpressionNode);
                    break;
                case IdentifierNode identifierNode:
                    Symbol symbol = _symbolTable.Lookup(identifierNode.Name, _currentBlock);
                    type = symbol.Type;
                    break;
                case ListOprExpressionNode listOprExpressionNode:
                    type = Visit(listOprExpressionNode);
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
                case DecimalNode decimalNode:
                    type = decimalNode.Type;
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

        public override TypeEnum? Visit(InfixExpressionNode node)
        {
            //Visits based on the type of InfixExpressionNode

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

        //AdditionNode -> Left, Right
        public override TypeEnum? Visit(AdditionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Addition

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            
            if (leftType == TypeEnum.boolean || rightType == TypeEnum.boolean) 
            {
                TypeError(node, $"Addition of type 'boolean' is not allowed.");
                return null;
            }
            else if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Addition of type 'list' is not allowed.");
                return null;
            }
            else if ((leftType == TypeEnum.text && rightType != TypeEnum.text) ||
                     (leftType != TypeEnum.text && rightType == TypeEnum.text))
            {
                TypeError(node, $"type 'text' can only be added with another value of type 'text.");
                return null;
            }

            if (leftType == TypeEnum.number && rightType == TypeEnum.number)
                return TypeEnum.number;
            else if (leftType == TypeEnum.text && rightType == TypeEnum.text)
                return TypeEnum.text;
            else if (leftType == TypeEnum._decimal || rightType == TypeEnum._decimal)
                return TypeEnum._decimal;
            else
                throw new Exception();
        }

        //SubtractionNode -> Left, Right
        public override TypeEnum? Visit(SubtractionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Subtraction

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (leftType == TypeEnum.boolean || rightType == TypeEnum.boolean)
            {
                TypeError(node, $"Subtraction of type 'boolean' is not allowed.");
                return null;
            }
            else if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Subtraction of type 'list' is not allowed.");
                return null;
            }
            else if (leftType == TypeEnum.text || rightType == TypeEnum.text)
            {
                TypeError(node, $"Subtraction of type 'text' is not allowed.");
                return null;
            }

            if (leftType == TypeEnum.number && rightType == TypeEnum.number)
                return TypeEnum.number;
            else if (leftType == TypeEnum._decimal || rightType == TypeEnum._decimal)
                return TypeEnum._decimal;
            else
                throw new Exception();
        }

        //MultiplicationNode -> Left, Right
        public override TypeEnum? Visit(MultiplicationNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Multiplication

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (leftType == TypeEnum.boolean || rightType == TypeEnum.boolean)
            {
                TypeError(node, $"Multiplication of type 'boolean' is not allowed.");
                return null;
            }
            else if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Multiplication of type 'list' is not allowed.");
                return null;
            }
            else if (leftType == TypeEnum.text || rightType == TypeEnum.text)
            {
                TypeError(node, $"Multiplication of type 'text' is not allowed.");
                return null;
            }

            if (leftType == TypeEnum.number && rightType == TypeEnum.number)
                return TypeEnum.number;
            else if (leftType == TypeEnum._decimal || rightType == TypeEnum._decimal)
                return TypeEnum._decimal;
            else
                throw new Exception();
        }

        //DivisionNode -> Left, Right
        public override TypeEnum? Visit(DivisionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Division

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (leftType == TypeEnum.boolean || rightType == TypeEnum.boolean)
            {
                TypeError(node, $"Division of type 'boolean' is not allowed.");
                return null;
            }
            else if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Division of type 'list' is not allowed.");
                return null;
            }
            else if (rightType.Value == 0)
            {
                TypeError(node, $"Dividing by 0 is not allowed.");
                return null;
            }
            return TypeEnum._decimal;
        }

        //AndNode -> Left, Right
        public override TypeEnum? Visit(AndNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for AndNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (leftType != TypeEnum.boolean)
            {
                TypeError(node, $"Type '{leftType}' does not match type 'boolean' on the left hand side of the logic 'and' expression.");
                return null;
            }
            if (rightType != TypeEnum.boolean)
            {
                TypeError(node, $"Type '{rightType}' does not match type 'boolean' on the right hand side of the logic 'and' expression.");
                return null;
            }
            return TypeEnum.boolean;
        }

        //OrNode -> Left, Right
        public override TypeEnum? Visit(OrNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for OrNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (leftType != TypeEnum.boolean)
            {
                TypeError(node, $"Type '{leftType}' does not match type 'boolean' on the left hand side of the logic 'or' expression.");
                return null;
            }
            if (rightType != TypeEnum.boolean)
            {
                TypeError(node, $"Type '{leftType}' does not match type 'boolean' on the right hand side of the logic 'or' expression.");
                return null;
            }
            return TypeEnum.boolean;
        }

        //EqualNode -> Left, Right
        public override TypeEnum? Visit(EqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for EqualNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if ((leftType == TypeEnum.boolean && rightType != TypeEnum.boolean) ||
                     (leftType != TypeEnum.boolean && rightType == TypeEnum.boolean))
            {
                TypeError(node, $"Type 'boolean' can only use 'equal' with another value of type 'boolean'.");
                return null;
            }
            else if ((leftType == TypeEnum.text && rightType != TypeEnum.text) ||
                    (leftType != TypeEnum.text && rightType == TypeEnum.text))
            {
                TypeError(node, $"Type 'text' can only use 'equal' with another value of type 'text'.");
                return null;
            }
            return TypeEnum.boolean;
        }

        //NotEqualNode -> Left, Right
        public override TypeEnum? Visit(NotEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for NotEqualNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if ((leftType == TypeEnum.boolean && rightType != TypeEnum.boolean) ||
                     (leftType != TypeEnum.boolean && rightType == TypeEnum.boolean))
            {
                TypeError(node, $"Type 'boolean' can only use 'not equal' with another value of type 'boolean'.");
                return null;
            }
            else if ((leftType == TypeEnum.text && rightType != TypeEnum.text) ||
                    (leftType != TypeEnum.text && rightType == TypeEnum.text))
            {
                TypeError(node, $"Type 'text' can only use 'not equal' with another value of type 'text'.");
                return null;
            }
            return TypeEnum.boolean;
        }

        //GreaterNode -> Left, Right
        public override TypeEnum? Visit(GreaterNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for GreaterNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if (leftType == TypeEnum.boolean || rightType == TypeEnum.boolean)
            {
                TypeError(node, $"Type 'boolean' is not allowed in a 'greater' expression.");
                return null;
            }
            else if (leftType == TypeEnum.text || rightType == TypeEnum.text)
            {
                TypeError(node, $"Type 'text' is not allowed in a 'greater' expression.");
                return null;
            }
            return TypeEnum.boolean;
        }

        //LessNode -> Left, Right
        public override TypeEnum? Visit(LessNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for LessNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if (leftType == TypeEnum.boolean || rightType == TypeEnum.boolean)
            {
                TypeError(node, $"Type 'boolean' is not allowed in a 'less' expression.");
                return null;
            }
            else if (leftType == TypeEnum.text || rightType == TypeEnum.text)
            {
                TypeError(node, $"Type 'text' is not allowed in a 'less' expression.");
                return null;
            }
            return TypeEnum.boolean;
        }

        //GreaterEqualNode -> Left, Right
        public override TypeEnum? Visit(GreaterEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for EqualNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if (leftType == TypeEnum.boolean || rightType == TypeEnum.boolean)
            {
                TypeError(node, $"Type 'boolean' is not allowed in a 'greater or equal' expression.");
                return null;
            }
            else if (leftType == TypeEnum.text || rightType == TypeEnum.text)
            {
                TypeError(node, $"Type 'text' is not allowed in a 'greater or equal' expression.");
                return null;
            }
            return TypeEnum.boolean;
        }

        //LessEqualNode -> Left, Right
        public override TypeEnum? Visit(LessEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for LessEqualNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (isList(leftType) || isList(rightType))
            {
                TypeError(node, $"Type 'list' is not allowed in boolean expressions.");
                return null;
            }
            else if (leftType == TypeEnum.boolean || rightType == TypeEnum.boolean)
            {
                TypeError(node, $"Type 'boolean' is not allowed in a 'less or equal' expression.");
                return null;
            }
            else if (leftType == TypeEnum.text || rightType == TypeEnum.text)
            {
                TypeError(node, $"Type 'text' is not allowed in a 'less or equal' expression.");
                return null;
            }
            return TypeEnum.boolean;
        }

        #endregion

        //IfNode -> Condition, Block, ElseIfs
        public override TypeEnum? Visit(IfNode node)
        {
            //Visit Condition & Block
            //If any - visit ElseIfs and/Or Else
            //Check if Condition type is boolean

            TypeEnum? type = Visit(node.Condition);
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
            {
                TypeError(node, $"Only boolean expression is allowed in an 'if' condition");
            }

            return null;
        }

        //ElseIfNode -> Condition, Block
        public override TypeEnum? Visit(ElseIfNode node)
        {
            //Visit Condition & Block
            //Check if Condition type is boolean

            TypeEnum? type = Visit(node.Condition); 
            Visit(node.Block);

            if (type != TypeEnum.boolean)
            {
                TypeError(node, $"Only boolean expression is allowed in an 'else if' condition");
            }

            return null;
        }

        //ElseNode -> Block
        public override TypeEnum? Visit(ElseNode node)
        {
            Visit(node.Block);
            return null;
        }

        //RepeatNode -> Expression, Block
        public override TypeEnum? Visit(RepeatNode node)
        {
            //Visit Expression & Block
            //Check if Expression type is Number
            TypeEnum? type = Visit(node.Expression);
            Visit(node.Block);

            if (type != TypeEnum.number)
            {
                TypeError(node, "The repeat condition is type '{type}', but has to be a number");
            }

            return null;
        }

        //WhileNode -> Condition, Block
        public override TypeEnum? Visit(WhileNode node)
        {
            //Visit Condition & Block
            //Check if Condition type is boolean

            TypeEnum? type = Visit(node.Condition);
             Visit(node.Block);

            if (type != TypeEnum.boolean)
            {
                TypeError(node, $"The while expression is type '{type}', but has to be a boolean");
            }

            return null;
        }

        //ForeachNode -> DeclarationNode, IdentifierNode, Block
        public override TypeEnum? Visit(ForeachNode node)
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
        
        public override TypeEnum? Visit(ListOprStatementNode node)
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

        public override TypeEnum? Visit(ListOprExpressionNode node)
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
        public override TypeEnum? Visit(ListAddNode node)
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

            foreach (var arg in node.Arguments.Expressions)
            {
                TypeEnum? argument = Visit(arg);

                if (argument != getListType(sym.Type))
                {
                    TypeError(node, $"'{sym.Name}:Add()' expects type '{getListType(sym.Type)}'");
                    return null;
                }
            }

            //var functionBlock = _symbolTable.GetFunctionBlock(_currentBlock);
            //if (functionBlock != null && functionBlock.IsFunctionCall)
            //{
            //    list.Value.AddRange(arguments);
            //    list.Size += (ushort)arguments.Count;
            //}

            return null;
        }

        //ListReplaceNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListReplaceNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if expression is a number

            Symbol? sym = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

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

            var expression0 = node.Arguments.Expressions[0];
            TypeEnum? argument0 = Visit(expression0);

            if (argument0 != getListType(list.Type))
            {
                TypeError(node, $"'{sym.Name}:Replace()' expects type '{getListType(sym.Type)}'");
                return null;
            }

            var expression1 = node.Arguments.Expressions[1];
            TypeEnum? argument1 = Visit(expression1);

            if (argument1 != TypeEnum.number)
            {
                TypeError(node, $"'{sym.Name}:Replace()' expects type '{TypeEnum.number}'");
                return null;
            }


            return null;
        }

        //ListValueOfNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListValueOfNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symbol table
            //Check if the identifier is a list
            //Check if Expression is type number

            Symbol? sym = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

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
            TypeEnum? argument0 = Visit(expression0);

            if (argument0 != TypeEnum.number)
            {
                TypeError(node, $"'{sym.Name}:Add()' expects type '{TypeEnum.number}'");
                return null;
            }

            return TypeEnum.number;
        }

        //ListIndexOfNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListIndexOfNode node)
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

            if (node.Arguments.Expressions.Count != 1)
            {
                TypeError(node, $"'{sym.Name}':ValueOf() requires one argument");
                return null;
            }

            var expression0 = node.Arguments.Expressions[0];
            TypeEnum? argument0 = Visit(expression0);

            if (argument0 != TypeEnum.number)
            {
                TypeError(node, $"'{sym.Name}:Add()' expects type '{TypeEnum.number}'");
                return null;
            }

            return getListType(sym.Type);
        }

        public override TypeEnum? Visit(CommentNode node)
        {
            return null;
        }

        public override TypeEnum? Visit(FunctionCallExprNode node)
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
                TypeEnum? argType;

                if (arguments[i] is IdentifierNode)
                    argType = _symbolTable.Lookup(((IdentifierNode)arguments[i]).Name, _currentBlock).Type;
                else
                    argType = Visit(arguments[i]);

                if (paramSymbol.Type != argType)
                {
                    TypeError(node, $"{sym.Name} parameter of type {paramSymbol.Type} doesn't match argument of type {argType}.");
                    return null;
                }
            }
            return sym.Type;
        }

        public override TypeEnum? Visit(InputExprNode node)
        {
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (arguments.Count != 0)
            {
                TypeError(node, $"'input()' takes 0 arguments");
            }

            return node.Type;
        }

        public override TypeEnum? Visit(OutputExprNode node)
        {
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (arguments.Count != 1)
            {
                TypeError(node, $"'output()' takes 1 arguments");
            }

            var expression0 = arguments[0];
            TypeEnum? argument0 = Visit(expression0);

            if (isList(argument0))
            {
                TypeError(node, $"'output()' does not support type {argument0}");
            }

            return null;
        }

        public override TypeEnum? Visit(FunctionDeclarationNode node)
        {
            TypeEnum? blockType = Visit(node.Block);

            if (node.ReturnType == TypeEnum.nothing && blockType != TypeEnum.nothing)
            {
                TypeError(node, $"function '{node.Name}()' expects no return statement");
                return null;
            }
            else if (node.ReturnType != TypeEnum.nothing && node.ReturnType != blockType)
            {
                TypeError(node, $"function '{node.Name}()' expects to return type {node.ReturnType} but returns type {blockType}");
                return null;
            }

            return blockType;
        }

        public override TypeEnum? Visit(FunctionCallStmtNode node)
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
                TypeEnum? argType;

                if (arguments[i] is IdentifierNode)
                    argType = _symbolTable.Lookup(((IdentifierNode)arguments[i]).Name, _currentBlock).Type;
                else
                    argType = Visit(arguments[i]);

                if (paramSymbol.Type != argType)
                {
                    TypeError(node, $"{sym.Name} parameter of type {paramSymbol.Type} doesn't match argument of type {argType}.");
                    return null;
                }
            }
            return sym.Type;
        }

        public override TypeEnum? Visit(InputStmtNode node)
        {
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (arguments.Count != 0)
            {
                TypeError(node, $"'input()' takes 0 arguments");
            }

            return node.Type;

            //string input = Console.ReadLine();

            //switch (node.Type)
            //{
            //    case NumberNode numberNode:
            //        int number = 0;
            //        if (!int.TryParse(input, out number))
            //        {
            //            TypeError(node, $"'input()' Expected type {numberNode.Type}");
            //            return null;
            //        }
            //        numberNode.Value = number;
            //        return numberNode;
            //    case DecimalNode decimalNode:
            //        float _decimal = 0;
            //        if (!float.TryParse(input, CultureInfo.InvariantCulture, out _decimal))
            //        {
            //            TypeError(node, $"'input()' Expected type {decimalNode.Type}");
            //            return null;
            //        }
            //        decimalNode.Value = _decimal;
            //        return decimalNode;
            //    case BooleanNode boolNode:
            //        bool boolean = false;
            //        if (!bool.TryParse(input, out boolean))
            //        {
            //            TypeError(node, $"'input()' Expected type {boolNode.Type}");
            //            return null;
            //        }
            //        boolNode.Value = boolean;
            //        return boolNode;
            //    case TextNode textNode:
            //        textNode.Value = input;
            //        return textNode;
            //    default:
            //        TypeError(node, "Invalid typing for 'input()'");
            //        return null;
            //}
        }

        public override TypeEnum? Visit(OutputStmtNode node)
        {
            List<ExpressionNode> arguments = node.Arguments.Expressions;

            if (arguments.Count != 1)
            {
                TypeError(node, $"'output()' takes 1 arguments");
                return null;
            }

            var expression0 = arguments[0];
            TypeEnum? argument0 = Visit(expression0);

            if (isList(argument0))
            {
                TypeError(node, $"'output()' does not support type {argument0}");
                return null;
            }

            return null;
        }

        public override TypeEnum? Visit(ReturnNode node)
        {
            return Visit(node.Expression);
        }

        public override TypeEnum? Visit(FunctionBlockNode node)
        {
            _currentBlock = node;

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

            TypeEnum? returnExpr = Visit(node.ReturnExpression);

            if (returnExpr == null)
                returnExpr = TypeEnum.nothing;

            _currentBlock = node;

            return returnExpr;
        }

        public override TypeEnum? Visit(ForeachBlockNode node)
        {
            //Visits all of it's commands
            _currentBlock = node;

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

        public void TypeError(ASTNode node, string error)
        {
            typeErrorhandler.TypeErrorMessages.Add($"Error line {node.Line}: {error}");
        }

    }
}
