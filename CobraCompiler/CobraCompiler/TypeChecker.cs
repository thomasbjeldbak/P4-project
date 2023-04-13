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

        //BlockNode -> Commands
        public override TypeEnum? Visit(BlockNode node)
        {
            //Visits all of it's commands
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
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
            }
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

                if (symbol.Type != exprNode)
                {
                    var error = $"Error: Initialization of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}.";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
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

        //AssignNode -> Identifier, Expression
        public override TypeEnum? Visit(AssignNode node)
        {
            //Gets symbol for identifier & Visits expression
            //Check if Identifier Type matches expression

            Symbol symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            if (node.Expression != null)
            {
                TypeEnum? exprNode = Visit(node.Expression);

                if (symbol.Type != exprNode)
                {
                    var error = $"Error: Assignment of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}.";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
            } 

            return symbol.Type;
        }

        public override TypeEnum? Visit(ExpressionNode node)
        {
            //Visits based on the type of ExpressionNode

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
            
            if (leftType != rightType)
            {
                var error = $"Error: Addition of '{leftType}' and '{rightType}' does not match.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (leftType == TypeEnum.boolean) 
            {
                var error = $"Error: Addition of type boolean is not allowed.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (isList(leftType))
            {
                var error = $"Error: Addition of type list is not allowed.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }

            return leftType;
        }

        //SubtractionNode -> Left, Right
        public override TypeEnum? Visit(SubtractionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Subtraction

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (leftType != rightType)            
            {
                var error = $"Error: Subtraction of '{leftType}' and '{rightType}' does not match.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (leftType == TypeEnum.boolean)
            {
                var error = $"Error: Subtraction of type boolean is not allowed.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (isList(leftType))
            {
                var error = $"Error: Subtraction of type list is not allowed.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            
            return leftType;
        }

        //MultiplicationNode -> Left, Right
        public override TypeEnum? Visit(MultiplicationNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Multiplication

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);

            if (leftType != rightType)
            {
                var error = $"Error: Multiplication of '{leftType}' and '{rightType}' does not match.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (leftType == TypeEnum.boolean)
            {
                var error = $"Error: Multiplication of type boolean is not allowed.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (isList(leftType))
            {
                var error = $"Error: Multiplication of type list is not allowed.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }

            return leftType;
        }

        //DivisionNode -> Left, Right
        public override TypeEnum? Visit(DivisionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Division

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;
            if (leftType != rightType)
            {
                var error = $"Error: Division of '{leftType}' and '{rightType}' does not match.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (leftType == TypeEnum.boolean)
            {
                var error = $"Error: Division of type 'boolean' is not allowed.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (isList(leftType))
            {
                var error = $"Error: Division of type 'list' is not allowed.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }

            return leftType;
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
                var error = $"Error: Type '{leftType}' does not match type 'boolean' on the left hand side of the logic 'and' expression";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            if(rightType != TypeEnum.boolean) 
            {
                var error = $"Error: Type '{rightType}' does not match type 'boolean' on the right hand side of the logic 'and' expression";
                typeErrorhandler.TypeErrorMessages.Add(error);
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
                var error = $"Error: Type '{leftType}' does not match type 'boolean' on the left hand side of the logic 'or' expression";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            if(rightType != TypeEnum.boolean) 
            {
                var error = $"Error: Type '{rightType}' does not match type 'boolean' on the right hand side of the logic 'or' expression";
                typeErrorhandler.TypeErrorMessages.Add(error);
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

            if (leftType != rightType)
            {
                var error = $"Error: The type of '{leftType}' does not match type '{rightType}' in the logic equal expression.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (isList(leftType)){
                var error = $"Error: Type '{leftType}' is not allowed in boolean expressions.";
                typeErrorhandler.TypeErrorMessages.Add(error);
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

            if (leftType != rightType)
            {
                var error = $"Error: The type of '{leftType}' does not match type '{rightType}' in the logic not equal expression.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }
            else if (isList(leftType)){
                var error = $"Error: Type '{leftType}' is not allowed in boolean expressions.";
                typeErrorhandler.TypeErrorMessages.Add(error);
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

            if (leftType != rightType)
            {
                if (leftType != TypeEnum.number)
                {
                    var error = $"Error: The left hand side with type '{leftType}' does not match type 'number'.";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
                if (rightType != TypeEnum.number)
                {
                    var error = $"Error: The right hand side with type '{rightType}' does not match type 'number'.";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
            }
            else if (leftType != TypeEnum.number || rightType != TypeEnum.number )
            {
                var error = $"Error: The '>' symbol is only allowed in number expressions.";
                typeErrorhandler.TypeErrorMessages.Add(error);
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

            if (leftType != rightType)
            {
                if (leftType != TypeEnum.number)
                {
                    var error = $"Error: The left hand side with type '{leftType}' does not match type 'number'.";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
                if (rightType != TypeEnum.number)
                {
                    var error = $"Error: The right hand side with type '{rightType}' does not match type 'number'.";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
            }
            else if (leftType != TypeEnum.number || rightType != TypeEnum.number )
            {
                var error = $"Error: The '<' symbol is only allowed in number expressions.";
                typeErrorhandler.TypeErrorMessages.Add(error);
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

            if (leftType != rightType)
            {
                if (leftType != TypeEnum.number)
                {
                    var error = $"Error: The left hand side with type '{leftType}' does not match type 'number'";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
                if (rightType != TypeEnum.number)
                {
                    var error = $"Error: The right hand side with type '{rightType}' does not match type 'number'";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
            }
            else if (leftType != TypeEnum.number || rightType != TypeEnum.number )
            {
                var error = $"Error: The '>=' symbol is only allowed in number expressions";
                typeErrorhandler.TypeErrorMessages.Add(error);
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

            if (leftType != rightType)
            {
                if (leftType != TypeEnum.number)
                {
                    var error = $"Error: The left hand side with type '{leftType}' does not match type 'number'";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
                if (rightType != TypeEnum.number)
                {
                    var error = $"Error: The right hand side with type '{rightType}' does not match type 'number'";
                    typeErrorhandler.TypeErrorMessages.Add(error);
                }
            }
            else if (leftType != TypeEnum.number || rightType != TypeEnum.number )
            {
                var error = $"Error: The '<=' symbol is only allowed in number expressions";
                typeErrorhandler.TypeErrorMessages.Add(error);
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
                var error = $"Error: Only boolean expression is allowed in the 'if' condition.";
                typeErrorhandler.TypeErrorMessages.Add(error);
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
                throw new Exception();

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
                throw new Exception();

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
                throw new Exception();

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
            TypeEnum? localVarType = Visit(node.LocalVariable);

            Visit(node.Block);

            if (!isList(list.Type))
            {
                var error = $"Error: '{list.Name}' is not a list.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            } else if (getListType(list.Type) != localVarType)
            {
                var error = $"Error: For each local variable type error. Expects type '{getListType(list.Type)}'.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }

            return null;
        }
        
        public override TypeEnum? Visit(ListOperationNode node)
        {
            //Visits based on type of ListOperationNode

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

        //ListAddNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListAddNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if list inner type matches Expression

            Symbol? list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
            {
                var error = $"Error: '{list.Name}' is not a list.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            } else if (getListType(list.Type) != type)
            {
                var error = $"Error: '{list.Name}:Add()' expects type '{getListType(list.Type)}'.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }

            return null;
        }

        //ListDeleteNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListDeleteNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if expression is a number

            Symbol? list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
            {
                var error = $"Error: '{list.Name}' is not a list.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            } else if (type != TypeEnum.number)
            {
                var error = $"Error: '{list.Name}:DeleteOf()' expects a number.";
                typeErrorhandler.TypeErrorMessages.Add(error);
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

            Symbol? list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
            {
                var error = $"Error: '{list.Name}' is not a list.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            } else if (type != TypeEnum.number)
            {
                var error = $"Error: '{list.Name}:ValueOf()' expects a number.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }

            return null;
        }

        //ListIndexOfNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListIndexOfNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if list inner type matches Expression

            Symbol? list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
            {
                var error = $"Error: '{list.Name}' is not a list.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            } else if (getListType(list.Type) != type)
            {
                var error = $"Error: '{list.Name}:IndexOf()' expects type '{getListType(list.Type)}'.";
                typeErrorhandler.TypeErrorMessages.Add(error);
            }

            return null;
        }

        #region List helper functions

        //Takes TypeEnum (list_[TYPE]) and returns the Type
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
                default:
                    throw new Exception();
            }
        }

        //Checks if TypeEnum is a list
        //e.g. list_number -> return true,
        //  number -> return false;
        private bool isList(TypeEnum? listType)
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
