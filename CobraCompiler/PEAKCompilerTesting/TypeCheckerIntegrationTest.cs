using Antlr4.Runtime;
using static NUnit.Framework.Assert;

namespace PEAKCompilerTesting;

[TestFixture]
public class TypeCheckerIntegrationTest
{
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
}