using static NUnit.Framework.Assert;

namespace PEAKCompilerTesting;

public class SymbolTableUnitTest
{
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
        AreEqual(blockNode2, symbolTable._currentBlock);
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
        AreEqual(blockNode1, symbolTable._currentBlock);
    }

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
        AreEqual(programNode, symbolTable._currentBlock);
    }
    
    [Test]
    public void NewScope_AddsScopeToScopesDictionary()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode = new ASTNodes.BlockNode();

        // Act
        symbolTable.NewScope(blockNode);
        That(symbolTable._scopes.ContainsKey(blockNode), Is.True);
        That(symbolTable._scopes.Count, Is.EqualTo(1));
        That(symbolTable._scopes[blockNode], Is.Not.Null);
        That(symbolTable._scopes[blockNode].Block, Is.EqualTo(blockNode));
    }

    [Test]
    public void NewScope_SetsParentScopeIfStackScopesNotEmpty()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var parentBlockNode = new ASTNodes.BlockNode();
        symbolTable.NewScope(parentBlockNode);
        var childBlockNode = new ASTNodes.BlockNode();

        // Act
        symbolTable.NewScope(childBlockNode);

        // Assert
        That(symbolTable._scopes[childBlockNode].Parent?.Block, Is.EqualTo(parentBlockNode));
    }

    [Test]
    public void NewScope_SetsParentScopeToNullIfStackScopesIsEmpty()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode = new ASTNodes.BlockNode();

        // Act
        symbolTable.NewScope(blockNode);

        // Assert
        That(symbolTable._scopes[blockNode].Parent, Is.Null);
    }

    [Test]
    public void NewScope_PushesScopeToStackScopes()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode = new ASTNodes.BlockNode();

        // Act
        symbolTable.NewScope(blockNode);

        // Assert
        That(symbolTable._stackScopes, Has.Count.EqualTo(1));
        That(symbolTable._stackScopes.Peek().Block, Is.EqualTo(blockNode));
    }

    [Test]
    public void NewScope_SetsCurrentBlockToBlockOfNewScope()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode = new ASTNodes.BlockNode();

        // Act
        symbolTable.NewScope(blockNode);

        // Assert
        That(symbolTable._currentBlock, Is.EqualTo(blockNode));
    }
}