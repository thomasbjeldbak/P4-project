﻿using CobraCompiler;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ASTNodes
{
    public enum TypeEnum //All possible types
    {
        nothing,
        number,
        text,
        boolean,
        _decimal,
        list_object,
        list_number,
        list_text,
        list_boolean,
        list_decimal,
    }

    //All abstract nodes cannot directly be a node in the generated AST tree
    public abstract class ASTNode //A general class for all AST Nodes
    {
        public int Line { get; set; }
    }

    //ProgramNode (the start node)
    internal class ProgramNode : BlockNode
    {
    }

    //abstract "Command" is either a declaration, assignment or statement
    public abstract class CommandNode : ASTNode 
    {
    }

    //Declaration declares a variable using an expression
    //Type Identifier = Expression;
    internal class DeclarationNode : CommandNode
    {
        public IdentifierNode Identifier { get; set; }
        public ExpressionNode Expression { get; set; }
    }

    //Statement can be many things: functions, control structures etc,
    internal abstract class StatementNode : CommandNode { }

    //Assignment contains reference to declaration and an expression
    //Identifier = Expression;
    //(A clear distinction between this and declarationNode
    //needs to be made for the symbol table)
    internal class AssignNode : StatementNode
    {
        public IdentifierNode Identifier { get; set; }
        public ExpressionNode Expression { get; set; }
    }

    //Identifier has a type and a name
    //Type Name;
    //An Identifer can be used as an expression e.g. 'if ( IdentifierNode ) { }'
    internal class IdentifierNode : ExpressionNode
    {
        public TypeNode TypeNode { get; set; }
        public string Name { get; set; }
    }

    //Any expression can have a value (except infixExpressions shouldn't have)
    //e.g. if (true) {}
    internal abstract class ExpressionNode : ASTNode
    {
        public TypeEnum Type { get; set; }
        public dynamic Value { get; set; }
    }

    //InfixExpression has a left and right side
    //e.g. Left + Right
    internal abstract class InfixExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
    }

    //Types (with values)
    internal abstract class TypeNode : ExpressionNode
    {
    }

    //Each TypeNode overrides the 'Value' from expression
    //They also intialize if a TypeEnum of their correct type
    #region Types
    internal class NumberNode : TypeNode
    {
        public NumberNode() 
        {
            Type = TypeEnum.number;
        }
        public new int Value { get; set; }
    }

    internal class DecimalNode : TypeNode
    {
        public DecimalNode()
        {
            Type = TypeEnum._decimal;
        }
        public new float Value { get; set; }
    }

    internal class TextNode : TypeNode
    {
        public TextNode()
        {
            Type = TypeEnum.text;
        }
        public new string Value { get; set; }
    }
     
    internal class BooleanNode : TypeNode
    {
        public BooleanNode()
        {
            Type = TypeEnum.boolean;
        }
        public new bool Value { get; set; }
    }

    internal class ListNode : TypeNode
    {
        public ListNode(TypeEnum listType)
        {
            if (listType is TypeEnum.number)
                Type = TypeEnum.list_number;
            else if (listType is TypeEnum.text)
                Type = TypeEnum.list_text;
            else if (listType is TypeEnum.boolean)
                Type = TypeEnum.list_boolean;
            else if (listType is TypeEnum._decimal)
                Type = TypeEnum.list_decimal;

            Value = new List<TypeNode>();
        }
        public ushort Size { get; set; }
        public new List<TypeNode> Value { get; set; }
    }

    internal class NothingNode : TypeNode
    {
        public NothingNode()
        {
            Type = TypeEnum.nothing;
        }
    }

    #endregion

    //Each InfixExpression has a left & right side e.g. Left + Right
    //Left and right is also expressions
    #region InfixExpressions

    //RightExpression + LeftExpression
    internal class AdditionNode : InfixExpressionNode { }

    //RightExpression - LeftExpression
    internal class SubtractionNode : InfixExpressionNode { }

    //RightExpression / LeftExpression
    internal class DivisionNode : InfixExpressionNode { }

    //RightExpression * LeftExpression
    internal class MultiplicationNode : InfixExpressionNode { }

    //RightExpression and LeftExpression
    internal class AndNode : InfixExpressionNode { }

    //RightExpression or LeftExpression
    internal class OrNode : InfixExpressionNode { }

    //RightExpression is LeftExpression
    internal class EqualNode : InfixExpressionNode { }

    //RightExpression is not LeftExpression
    internal class NotEqualNode : InfixExpressionNode { }

    //RightExpression > LeftExpression
    internal class GreaterNode : InfixExpressionNode { }

    //RightExpression < LeftExpression
    internal class LessNode : InfixExpressionNode { }

    //RightExpression >= LeftExpression
    internal class GreaterEqualNode : InfixExpressionNode { }

    //RightExpression <= LeftExpression
    internal class LessEqualNode : InfixExpressionNode { }

    #endregion

    internal class CommentNode : StatementNode 
    {
        public string Comment { get; set; }
    }

    //Abstract class for all constrol structure (they all contain blocks)
    internal abstract class ControlStructureNode : StatementNode
    {
        public BlockNode Block { get; set; }
    }

    #region Control Structures

    //"If" containing a condition, block and a list of all elseNodes
    //If (Condition) {Block} Else-if (Condition) {Block}... Else {Block}
    internal class IfNode : ControlStructureNode
    {
        public ExpressionNode Condition { get; set; }
        public List<ElseNode> ElseIfs { get; set; }
    }

    //Else doesn't need a Condition
    //Else {Block}
    internal class ElseNode : ControlStructureNode 
    {
    }

    //ElseIf has a condition and is an ElseNode
    //Else If (Condition) {Block}
    internal class ElseIfNode : ElseNode
    {
        public ExpressionNode Condition { get; set; }
    }
    
    //"For loop" containing the number to count up to and a block
    //Repeat (Expression) {Block}
    internal class RepeatNode : ControlStructureNode
    {
        public ExpressionNode Expression { get; set; }
    }
    
    //"While loop" containing a Condition and a block
    //Repeat While (Condtion) {Block}
    internal class WhileNode : ControlStructureNode
    {
        public ExpressionNode Condition { get; set; }
    }

    //"Foreach loop" containing
    //Repeat For each (DeclarationNode in List) {Block}
    internal class ForeachNode : ControlStructureNode
    {
        public IdentifierNode List { get; set; }
        public new ForeachBlockNode Block { get; set; }
    }

    #endregion

    //Return node is a node representing a return statement
    internal class ReturnNode : StatementNode
    {
        public ExpressionNode Expression { get; set; }
    }

    //"Function call" has arguments and a reference to function declaration
    //Call Name
    internal class FunctionCallExprNode : ExpressionNode
    {
        public string Name { get; set; }
        public ArgumentsNode Arguments { get; set; }
    }

    internal class FunctionCallStmtNode : StatementNode
    {
        public string Name { get; set; }
        public ArgumentsNode Arguments { get; set; }
    }


    internal class InputExprNode : FunctionCallExprNode
    {
        public string VariableName { get; set; }
        public TypeEnum Type { get; set; }
    }

    internal class OutputExprNode : FunctionCallExprNode
    {
    }

    internal class InputStmtNode : FunctionCallStmtNode
    {
        public string VariableName { get; set; }
        public TypeEnum Type { get; set; }
    }

    internal class OutputStmtNode : FunctionCallStmtNode
    {
    }

    //"Function declaration" has a type to return, parameters and a block
    //Function Name (Parameters) ReturnType {Block}
    internal class FunctionDeclarationNode : StatementNode
    {
        public string Name { get; set; }
        public TypeEnum ReturnType { get; set; }
        public FunctionBlockNode Block { get; set; }
    }

    internal class FunctionBlockNode : BlockNode
    {
        public Dictionary<string, TypeEnum> UsedVariables { get; set; }
        public ParametersNode Parameters { get; set; }
        public ExpressionNode ReturnExpression { get; set; }
    }

    internal class ForeachBlockNode : BlockNode
    {
        public DeclarationNode LocalVariable { get; set; }
        public string ListName { get; set; }
    }

    //"block" contains commands
    //{Block}
    public class BlockNode : ASTNode
    {
        public List<CommandNode> Commands { get; set; }
    }

    //Abstract class for all listOperations
    //e.g. Identifier:[ADD](Expression);
    internal abstract class ListOprStatementNode : StatementNode
    {
        public IdentifierNode Identifier { get; set; }
        public ArgumentsNode Arguments { get; set; }
    }

    internal abstract class ListOprExpressionNode : ExpressionNode
    {
        public IdentifierNode Identifier { get; set; }
        public ArgumentsNode Arguments { get; set; }
    }

    internal class ArgumentsNode : ExpressionNode
    {
        public List<ExpressionNode> Expressions { get; set; }
    }

    internal class ParametersNode : ExpressionNode
    {
        public List<DeclarationNode> Declarations { get; set; }
    }

    #region List Operations
    internal class ListAddNode : ListOprStatementNode 
    { 
    }

    internal class ListReplaceNode : ListOprStatementNode 
    {
    }

    internal class ListValueOfNode : ListOprExpressionNode 
    {
    }

    internal class ListIndexOfNode : ListOprExpressionNode
    { 
    }

    #endregion
}
