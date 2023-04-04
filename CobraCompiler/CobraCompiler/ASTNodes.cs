using CobraCompiler;
using System;
using System.Linq.Expressions;
using static CobraCompiler.Symbol;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ASTNodes
{
    public enum TypeEnum //All possible types
    {
        number,
        text,
        boolean,
        list_object,
        list_number,
        list_text,
        list_boolean,
    }

    //All abstract nodes cannot directly be a node in the generated AST tree
    public abstract class ASTNode //A general class for all AST Nodes
    {
        //A function for retrieving the properties (children) of the node
        //Is used for the symbol table
        public abstract List<ASTNode> GetChildren();
    }

    //ProgramNode (the start node)
    internal class ProgramNode : BlockNode
    {
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.AddRange(Commands);
            return children;
        }
    }

    //abstract "Command" is either a declaration, assignment or statement
    internal abstract class CommandNode : ASTNode 
    {
    };

    //Declaration declares a variable using an expression
    //Type Identifier = Expression;
    internal class DeclarationNode : CommandNode
    {
        public IdentifierNode Identifier { get; set; }
        public ExpressionNode Expression { get; set; }

        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Identifier);
            children.Add(Expression);
            return children;
        }
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
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Identifier);
            children.Add(Expression);
            return children;
        }
    }

    //Identifier has a type and a name
    //Type Name;
    //An Identifer can be used as an expression e.g. 'if ( IdentifierNode ) { }'
    internal class IdentifierNode : ExpressionNode
    {
        public TypeNode TypeNode { get; set; }
        public string Name { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            return children;
        }
    }

    //Any expression can have a value (except infixExpressions shouldn't have)
    //e.g. if (true) {}
    internal abstract class ExpressionNode : ASTNode
    {
        public dynamic Value { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            return children;
        }
    }

    //InfixExpression has a left and right side
    //e.g. Left + Right
    internal abstract class InfixExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Left);
            children.Add(Right);
            return children;
        }
    }

    //Types (with values)
    internal abstract class TypeNode : ExpressionNode
    {
        public TypeEnum Type { get; set; }
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
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            return children;
        }
    }

    internal class TextNode : TypeNode
    {
        public TextNode()
        {
            Type = TypeEnum.text;
        }
        public new string Value { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            return children;
        }
    }
     
    internal class BooleanNode : TypeNode
    {
        public BooleanNode()
        {
            Type = TypeEnum.boolean;
        }
        public new bool Value { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            return children;
        }
    }

    internal class ListNode : TypeNode
    {
        public ListNode(TypeEnum listType)
        {
            Type = listType;
        }
        public new TypeNode[] Value { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.AddRange(Value);
            return children;
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
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Condition);
            children.Add(Block);
            children.AddRange(ElseIfs);
            return children;
        }
    }

    //Else doesn't need a Condition
    //Else {Block}
    internal class ElseNode : ControlStructureNode 
    {
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Block);
            return children;
        }
    }

    //ElseIf has a condition and is an ElseNode
    //Else If (Condition) {Block}
    internal class ElseIfNode : ElseNode
    {
        public ExpressionNode Condition { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Condition);
            children.Add(Block);
            return children;
        }
    }
    
    //"For loop" containing the number to count up to and a block
    //Repeat (Expression) {Block}
    internal class RepeatNode : ControlStructureNode
    {
        public ExpressionNode Expression { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Expression);
            children.Add(Block);
            return children;
        }
    }
    
    //"While loop" containing a Condition and a block
    //Repeat While (Condtion) {Block}
    internal class WhileNode : ControlStructureNode
    {
        public ExpressionNode Condition { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Condition);
            children.Add(Block);
            return children;
        }
    }

    //"Foreach loop" containing
    //Repeat For each (DeclarationNode in List) {Block}
    internal class ForeachNode : ControlStructureNode
    {
        public IdentifierNode List { get; set; }
        public DeclarationNode LocalVariable { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(List);
            children.Add(LocalVariable);
            children.Add(Block);
            return children;
        }
    }

    #endregion

    //"Function call" has arguments and a reference to function declaration
    //Call Name
    internal class FunctionCallNode : StatementNode
    {
        public FunctionDeclarationNode Function { get; set; }
        public IdentifierNode[] Arguments { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Function);
            children.AddRange(Arguments);
            return children;
        }
    }

    //"Function declaration" has a type to return, parameters and a block
    //Function Name (Parameters) ReturnType {Block}
    internal class FunctionDeclarationNode : StatementNode
    {
        public string Name { get; set; }
        public TypeNode ReturnType { get; set; }
        public IdentifierNode[] Parameters { get; set; }
        public BlockNode Block { get; set; }

        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(ReturnType);
            children.AddRange(Parameters);
            children.Add(Block);
            return children;
        }
    }

    //"block" contains commands
    //{Block}
    internal class BlockNode : ASTNode
    {
        public List<CommandNode> Commands { get; set; }
        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            
            if (Commands != null)
                children.AddRange(Commands);
            
            return children;
        }
    }

    //Abstract class for all listOperations
    //e.g. Identifier:[ADD](Expression);
    internal abstract class ListOperationNode : StatementNode
    {
        public IdentifierNode Identifier { get; set; }
        public ExpressionNode Expression { get; set; }

        public override List<ASTNode> GetChildren()
        {
            var children = new List<ASTNode>();
            children.Add(Identifier);
            children.Add(Expression);
            return children;
        }
    }

    #region List Operations
    internal class ListAddNode : ListOperationNode { }

    internal class ListDeleteNode : ListOperationNode { }

    internal class ListValueOfNode : ListOperationNode { }

    internal class ListIndexOfNode : ListOperationNode { }

    #endregion
}
