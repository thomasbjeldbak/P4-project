using CobraCompiler;
using System;

public class ASTNodes
{
    internal class ASTNode
    {
    }

    internal class ProgramNode : BlockNode
    {
    }

    //abstract "Command" is either a delcaration, assignment or statement
    internal abstract class CommandNode : ASTNode { };

    //Declaration declares a variable using an expression
    internal class DeclarationNode : CommandNode
    {
        public IdentifierNode Identifier { get; set; }
        public ExpressionNode Expression { get; set; }
    }

    //Spørg hjælpelærer :)
    //Assignement contains reference to declaration and an expression
    internal class AssignNode : CommandNode
    {
        public IdentifierNode Identifier { get; set; }
        public ExpressionNode Expression { get; set; }
    }

    //SKAL MÅSKE SLETTES?
    //Statement can be many things: functions, control structures etc,
    internal abstract class StatementNode : CommandNode
    {

    }

    //Identifier has a type (which has a value) and a name
    internal class IdentifierNode : ASTNode
    {
        public TypeNode Type { get; set; }
        public string Name { get; set; }
    }

    //Any expression
    internal class ExpressionNode : ASTNode
    {
        public dynamic Value { get; set; }
    }

    //InfixExpression has a left and right side to an expression
    internal abstract class InfixExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
    }

    //Types with values
    internal abstract class TypeNode : ASTNode
    {
    }

    #region Types

    internal class NumberNode : TypeNode
    {
        public int Value { get; set; }
    }

    internal class TextNode : TypeNode
    {
        public string Value { get; set; }
    }
     
    internal class BooleanNode : TypeNode
    {
        public bool Value { get; set; }
    }

    internal class ListNode : TypeNode
    {
        public TypeNode[] Values { get; set; }
    }

    #endregion

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

    //Abstract class for all constrol structure (they contain blocks)
    internal abstract class ControlStructureNode : StatementNode
    {
        public BlockNode Block { get; set; }
    }

    #region Control Structures

    //"If" containing a predicate and a block
    internal class IfNode : ControlStructureNode
    {
        public ExpressionNode Condition { get; set; }
        public List<ElseNode> ElseIfs { get; set; }
    }

    internal class ElseNode : ControlStructureNode 
    {
    }

    internal class ElseIfNode : ElseNode
    {
        public ExpressionNode Condition { get; set; }
    }
    
    //"For loop" containing the number to count up to and a block
    internal class RepeatNode : ControlStructureNode
    {
        public ExpressionNode Expression { get; set; }
    }
    
    //"While loop" containing a predicate and a block
    internal class WhileNode : ControlStructureNode
    {
        public ExpressionNode Condition { get; set; }
    }

    //"Foreach loop" containing
    internal class ForeachNode : ControlStructureNode
    {
        public IdentifierNode List { get; set; }
        public IdentifierNode LocalVariable { get; set; }
    }

    #endregion

    //"Function call" has arguments and a reference to funciton declaration
    internal class FunctionCallNode : StatementNode
    {
        public FunctionDeclarationNode Function { get; set; }
        public IdentifierNode[] Arguments { get; set; }
    }

    //"Function delcaration" has a type to return, parameters and a block
    internal class FunctionDeclarationNode : StatementNode
    {
        public string Name { get; set; }
        public TypeNode ReturnType { get; set; }
        public IdentifierNode[] Parameters { get; set; }
        public BlockNode Block { get; set; }
    }

    //"block" contains commands
    internal class BlockNode : ASTNode
    {
        public List<CommandNode> Commands { get; set; }
    }

    internal abstract class ListOperationNode : StatementNode
    {
        public IdentifierNode Identifier { get; set; }
        public ExpressionNode Expression { get; set; }
    }
    internal class ListAddNode : ListOperationNode
    {

    }

    internal class ListDeleteNode : ListOperationNode
    {

    }

    internal class ListValueOfNode : ListOperationNode
    {

    }

    internal class ListIndexOfNode : ListOperationNode
    {

    }
}
