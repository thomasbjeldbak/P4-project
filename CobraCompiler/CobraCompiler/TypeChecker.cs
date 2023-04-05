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

        //Each time we enter a scope, we update the current block
        private BlockNode _currentBlock;

        public TypeChecker(SymbolTable symbolTable) 
        {
            _symbolTable = symbolTable;
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
                    throw new Exception($"Initialization of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
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
                    throw new Exception($"Assignment of {symbol.Type} '{symbol.Name}' does not match expression of type {exprNode}");
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

        //SubtractionNode -> Left, Right
        public override TypeEnum? Visit(SubtractionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Subtraction

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

        //MultiplicationNode -> Left, Right
        public override TypeEnum? Visit(MultiplicationNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Multiplication

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

        //DivisionNode -> Left, Right
        public override TypeEnum? Visit(DivisionNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for Division

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

        //AndNode -> Left, Right
        public override TypeEnum? Visit(AndNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for AndNode

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

        //OrNode -> Left, Right
        public override TypeEnum? Visit(OrNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for OrNode

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

        //EqualNode -> Left, Right
        public override TypeEnum? Visit(EqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for EqualNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (!isList(type))
                throw new Exception();

            return type;
        }

        //NotEquaNode -> Left, Right
        public override TypeEnum? Visit(NotEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for NotEqualNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (isList(type))
                throw new Exception();

            return type;
        }

        //GreaterNode -> Left, Right
        public override TypeEnum? Visit(GreaterNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for GreaterNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return type;
        }

        //LessNode -> Left, Right
        public override TypeEnum? Visit(LessNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for LessNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return type;
        }

        //GreaterEqualNode -> Left, Right
        public override TypeEnum? Visit(GreaterEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for EqualNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return type;
        }

        //LessEqualNode -> Left, Right
        public override TypeEnum? Visit(LessEqualNode node)
        {
            //Visits left and right side and gets their type
            //Check if the types match, and that they are of valid typing for LessEqualNode

            TypeEnum? leftType = Visit(node.Left);
            TypeEnum? rightType = Visit(node.Right);
            TypeEnum type;

            if (leftType != rightType)
                throw new Exception();

            type = (TypeEnum)leftType;

            if (type != TypeEnum.number)
                throw new Exception();

            return type;
        }

        #endregion

        //IfNode -> Condition, Block, ElseIfs
        public override TypeEnum? Visit(IfNode node)
        {
            //Visit Condition & Block
            //If any - visit ElseIfs and/Or Else
            //Check if Condition type is boolean

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

        //ElseIfNode -> Condition, Block
        public override TypeEnum? Visit(ElseIfNode node)
        {
            //Visit Condition & Block
            //Check if Condition type is boolean

            TypeEnum? type = Visit(node.Condition);
            
            if (node.Block != null)
                Visit(node.Block);

            if (type != TypeEnum.boolean)
                throw new Exception();

            return null;
        }

        //ElseNode -> Block
        public override TypeEnum? Visit(ElseNode node)
        {
            //Visit Block

            if (node.Block != null)
                Visit(node.Block);

            return null;
        }

        //RepeatNode -> Expression, Block
        public override TypeEnum? Visit(RepeatNode node)
        {
            //Visit Expression & Block
            //Check if Expression type is Number
            TypeEnum? type = Visit(node.Expression);
            
            if (node.Block != null)
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

            if (node.Block != null)
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

            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (getListType(list.Type) != type)
                throw new Exception();

            return null;
        }

        //ListDeleteNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListDeleteNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if expression is a number

            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (type != TypeEnum.number)
                throw new Exception();

            return null;
        }

        //ListValueOfNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListValueOfNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if Expression is type number

            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (type != TypeEnum.number)
                throw new Exception();

            return null;
        }

        //ListIndexOfNode -> IdentifierNode, Expression
        public override TypeEnum? Visit(ListIndexOfNode node)
        {
            //Visits Expression
            //Get symbol for Identifier in Symboltable
            //Check if the identifier is a list
            //Check if list inner type matches Expression

            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            TypeEnum? type = Visit(node.Expression);

            if (!isList(list.Type))
                throw new Exception();

            if (getListType(list.Type) != type)
                throw new Exception();

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
