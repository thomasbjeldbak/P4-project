using Antlr4.Runtime;
using NUnit.Framework.Internal;
using static NUnit.Framework.Assert;
namespace PEAKCompilerTesting;
[TestFixture]
public class TypeCheckerIntegrationTest
{

    [Test]
    public void TestVisitAdditionNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var additionNode1 = new ASTNodes.AdditionNode();
        var additionNode2 = new ASTNodes.AdditionNode();
        var additionNode3 = new ASTNodes.AdditionNode();
        
        additionNode1.Left = new ASTNodes.NumberNode();
        additionNode1.Right = new ASTNodes.NumberNode();
        additionNode2.Left = new ASTNodes.TextNode();
        additionNode2.Right = new ASTNodes.TextNode();
        additionNode3.Left = new ASTNodes.DecimalNode();
        additionNode3.Right = new ASTNodes.DecimalNode();

        // Act
        var result1 = typeChecker.Visit(additionNode1);
        var result2 = typeChecker.Visit(additionNode2);
        var result3 = typeChecker.Visit(additionNode3);

        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.number));
        That(result2, Is.EqualTo(ASTNodes.TypeEnum.text));
        That(result3, Is.EqualTo(ASTNodes.TypeEnum._decimal));
    }

    [Test]
    public void TestVisitSubtractionNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var subtractionNode1 = new ASTNodes.SubtractionNode();
        var subtractionNode2 = new ASTNodes.SubtractionNode();
        
        subtractionNode1.Left = new ASTNodes.NumberNode();
        subtractionNode1.Right = new ASTNodes.NumberNode();
        subtractionNode2.Left = new ASTNodes.DecimalNode();
        subtractionNode2.Right = new ASTNodes.DecimalNode();
        
        // Act
        var result1 = typeChecker.Visit(subtractionNode1);
        var result2 = typeChecker.Visit(subtractionNode2);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.number));
        That(result2, Is.EqualTo(ASTNodes.TypeEnum._decimal));
    }

    [Test]
    public void TestVisitMultiplicationNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var multiplicationNode1 = new ASTNodes.MultiplicationNode();
        var multiplicationNode2 = new ASTNodes.MultiplicationNode();
        
        multiplicationNode1.Left = new ASTNodes.NumberNode();
        multiplicationNode1.Right = new ASTNodes.NumberNode();
        multiplicationNode2.Left = new ASTNodes.DecimalNode();
        multiplicationNode2.Right = new ASTNodes.DecimalNode();
        
        // Act
        var result1 = typeChecker.Visit(multiplicationNode1);
        var result2 = typeChecker.Visit(multiplicationNode2);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.number));
        That(result2, Is.EqualTo(ASTNodes.TypeEnum._decimal));
    }
    
    [Test]
    public void TestVisitDivisionNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var divisionNode1 = new ASTNodes.DivisionNode();
        
        divisionNode1.Left = new ASTNodes.DecimalNode();
        divisionNode1.Right = new ASTNodes.DecimalNode();
        
        // Act
        var result1 = typeChecker.Visit(divisionNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum._decimal));
    }

    [Test]
    public void TestVisitAndNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var andNode1 = new ASTNodes.AndNode();
        
        andNode1.Left = new ASTNodes.BooleanNode();
        andNode1.Right = new ASTNodes.BooleanNode();

        // Act
        var result1 = typeChecker.Visit(andNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.boolean));
    }
    
    [Test]
    public void TestVisitOrNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var orNode1 = new ASTNodes.OrNode();
        
        orNode1.Left = new ASTNodes.BooleanNode();
        orNode1.Right = new ASTNodes.BooleanNode();

        // Act
        var result1 = typeChecker.Visit(orNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.boolean));
    }

    [Test]
    public void TestVisitEqualNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var equalNode1 = new ASTNodes.EqualNode();
        
        equalNode1.Left = new ASTNodes.BooleanNode();
        equalNode1.Right = new ASTNodes.BooleanNode();
        
        // Act
        var result1 = typeChecker.Visit(equalNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.boolean));
    }
    
    [Test]
    public void TestVisitNotEqualNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var notEqualNode1 = new ASTNodes.NotEqualNode();
        
        notEqualNode1.Left = new ASTNodes.BooleanNode();
        notEqualNode1.Right = new ASTNodes.BooleanNode();
        
        // Act
        var result1 = typeChecker.Visit(notEqualNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.boolean));
    }

    [Test]
    public void TestVisitGreaterNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var greaterNode1 = new ASTNodes.GreaterNode();
        
        greaterNode1.Left = new ASTNodes.NumberNode();
        greaterNode1.Right = new ASTNodes.NumberNode();
        
        // Act
        var result1 = typeChecker.Visit(greaterNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.boolean));
    }

    [Test]
    public void TestVisitLessNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());
        
        var lessNode1 = new ASTNodes.LessNode();
        
        lessNode1.Left = new ASTNodes.NumberNode();
        lessNode1.Right = new ASTNodes.NumberNode();
        
        // Act
        var result1 = typeChecker.Visit(lessNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.boolean));
    }
    
    [Test]
    public void TestVisitGreaterEqualNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());

        var greaterEqualNode1 = new ASTNodes.GreaterEqualNode();
        
        greaterEqualNode1.Left = new ASTNodes.NumberNode();
        greaterEqualNode1.Right = new ASTNodes.NumberNode();
        
        // Act
        var result1 = typeChecker.Visit(greaterEqualNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.boolean));
    }

    [Test]
    public void TestVisitLessEqualNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var typeChecker = new TypeChecker(symbolTable, new ErrorHandler());

        var lessEqualNode1 = new ASTNodes.LessEqualNode();
        
        lessEqualNode1.Left = new ASTNodes.NumberNode();
        lessEqualNode1.Right = new ASTNodes.NumberNode();
        
        // Act
        var result1 = typeChecker.Visit(lessEqualNode1);
        
        // Assert
        That(result1, Is.EqualTo(ASTNodes.TypeEnum.boolean));
    }

}