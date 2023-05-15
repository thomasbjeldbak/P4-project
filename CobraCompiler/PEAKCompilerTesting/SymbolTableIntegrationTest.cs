using static NUnit.Framework.Assert;

namespace PEAKCompilerTesting;

public class SymbolTableIntegrationTest
{
    [Test]
    public void ExitScope_UpdatesCurrentBlockToProgramNodeIfTopScopeIsProgramNode()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode = new ASTNodes.BlockNode();
        var programNode = new ASTNodes.ProgramNode();
        symbolTable.NewScope(blockNode);
        symbolTable.NewScope(programNode);

        // Act
        symbolTable.ExitScope();
        
        // Assert
        That(symbolTable._currentBlock, Is.EqualTo(programNode));
    }
    
    [Test]
    public void ExitScope_PopsScopeAndUpdatesCurrentBlock()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode1 = new ASTNodes.BlockNode();
        var blockNode2 = new ASTNodes.BlockNode();
        var blockNode3 = new ASTNodes.BlockNode();
        symbolTable.NewScope(blockNode1);
        symbolTable.NewScope(blockNode2);
        symbolTable.NewScope(blockNode3);

        // Act
        symbolTable.ExitScope();
        
        // Assert
        That(symbolTable._currentBlock, Is.EqualTo(blockNode2));
    }
    
    [Test]
    public void ExitScope_UpdatesCurrentBlockToParentOfTopScope()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode1 = new ASTNodes.BlockNode();
        var blockNode2 = new ASTNodes.BlockNode();
        symbolTable.NewScope(blockNode1);
        symbolTable.NewScope(blockNode2);

        // Act
        symbolTable.ExitScope();
        
        // Assert
        That(symbolTable._currentBlock, Is.EqualTo(blockNode1));
    }
    
    [Test]
    public void Lookup_ReturnsCorrectSymbol()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode1 = new ASTNodes.BlockNode();
        symbolTable.NewScope(blockNode1);
        symbolTable.Insert("x", ASTNodes.TypeEnum.text, blockNode1);
        symbolTable.Insert("y", ASTNodes.TypeEnum.text, blockNode1);
        
        // Act
        var result1 = symbolTable.Lookup("x", blockNode1);
        var result2 = symbolTable.Lookup("y", blockNode1);
        var result3 = symbolTable.Lookup("z", blockNode1);

        // Assert
        That(result1.Name, Is.EqualTo("x"));
        That(result2.Name, Is.EqualTo("y"));
        That(result3, Is.Null);
    }
}