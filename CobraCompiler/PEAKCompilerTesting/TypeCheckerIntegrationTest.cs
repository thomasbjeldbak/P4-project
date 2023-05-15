using Antlr4.Runtime;
using static NUnit.Framework.Assert;
namespace PEAKCompilerTesting;
[TestFixture]
public class TypeCheckerIntegrationTest
{

    [Test]
    public void TestVisitAdditionNode()
    {
        // Arrange
        var additionNode = new ASTNodes.AdditionNode();
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        additionNode.Left = new ASTNodes.NumberNode();
        additionNode.Right = new ASTNodes.NumberNode();

        // Act
        var result = typeChecker.Visit(additionNode);

        // Assert
        Assert.That(result, Is.EqualTo(ASTNodes.TypeEnum.number));
    }

}