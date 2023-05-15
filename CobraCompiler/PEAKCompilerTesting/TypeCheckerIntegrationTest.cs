using static NUnit.Framework.Assert;

namespace PEAKCompilerTesting;

[TestFixture]
public class TypeCheckerIntegrationTest
{
    [Test]
    public void Visit_ProgramNode_CallsVisitMethodsForCommands()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var programNode = new ASTNodes.ProgramNode
        {
            Commands = new List<ASTNodes.CommandNode>
            {
                new ASTNodes.DeclarationNode(),
                new ASTNodes.AssignNode(),
                new ASTNodes.StatementNode()
            }
        };

        // Act
        symbolTable.Visit(programNode);
        That(VisitMethodCalled(programNode.Commands[0], symbolTable), Is.True);
        That(VisitMethodCalled(programNode.Commands[1], symbolTable), Is.True);
        That(VisitMethodCalled(programNode.Commands[2], symbolTable), Is.True);
    }
    
    [Test]
    public void Visit_BlockNode_CallsVisitMethodsForCommands()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode = new ASTNodes.BlockNode
        {
            Commands = new List<ASTNodes.CommandNode>
            {
                new ASTNodes.DeclarationNode(),
                new ASTNodes.AssignNode(),
                new ASTNodes.ReturnNode(),
                new ASTNodes.StatementNode()
            }
        };

        // Act
        symbolTable.Visit(blockNode);
        That(VisitMethodCalled(blockNode.Commands[0], symbolTable), Is.True);
        That(VisitMethodCalled(blockNode.Commands[1], symbolTable), Is.True);
        That(VisitMethodCalled(blockNode.Commands[2], symbolTable), Is.True);
        That(VisitMethodCalled(blockNode.Commands[3], symbolTable), Is.True);
    }
    
    [Test]
    public void Visit_DeclarationNode_UpdatesSymbolAndChecksExpression()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var declarationNode = new ASTNodes.DeclarationNode
        {
            Identifier = new ASTNodes.IdentifierNode { Name = "variable", TypeNode = new ASTNodes.NumberNode()},
            Expression = new ASTNodes.NumberNode() { Value = 10 }
        };
        var programNode = new ASTNodes.ProgramNode()
        {
            Commands = new List<ASTNodes.CommandNode>
            {
                declarationNode
            }
        };
        var scope = new Scope() { Block = programNode };
        symbolTable._stackScopes.Push(scope);

        // Act
        symbolTable.Visit(declarationNode);

        // Assert
        // Assert that the symbol's expression is updated correctly
        Assert.AreEqual(10, ((ASTNodes.DeclarationNode)symbolTable.Lookup("variable", programNode)?.Reference!)?.Expression.Value);
    }

    private bool VisitMethodCalled(ASTNodes.CommandNode command, SymbolTable symbolTable)
    {
        switch (command)
        {
            case ASTNodes.DeclarationNode declarationNode:
                return MethodCalled(() => symbolTable.Visit(declarationNode));
            case ASTNodes.AssignNode assignNode:
                return MethodCalled(() => symbolTable.Visit(assignNode));
            case ASTNodes.ReturnNode returnNode:
                return MethodCalled(() => symbolTable.Visit(returnNode));
            case ASTNodes.StatementNode statementNode:
                return MethodCalled(() => symbolTable.Visit(statementNode));
            default:
                return false;
        }
    }

    private bool MethodCalled(Action action)
    {
        var methodCalled = false;

        void MethodWrapper()
        {
            methodCalled = true;
            action();
        }

        DoesNotThrow(MethodWrapper);
        return methodCalled;
    }
}