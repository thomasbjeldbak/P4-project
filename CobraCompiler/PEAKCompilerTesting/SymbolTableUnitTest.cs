using static NUnit.Framework.Assert;

namespace PEAKCompilerTesting;

public class SymbolTableUnitTest
{
    [Test]
    public void NewScope_AddsScopeToScopesDictionary()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode = new ASTNodes.BlockNode();

        // Act
        symbolTable.NewScope(blockNode);
        
        // Assert
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
    
    [Test]
    public void AddIDToFunctionBlock_AddsVariableToUsedVariables()
    {
        // Arrange
        var symbolTable = new SymbolTable(new ErrorHandler());
        var blockNode = new ASTNodes.BlockNode();
        var functionBlockNode = new ASTNodes.FunctionBlockNode()
        {
            UsedVariables = new Dictionary<string, ASTNodes.TypeEnum>()
        };
        var scope1 = new Scope { Block = blockNode };
        var scope2 = new Scope { Block = functionBlockNode };
        scope1.Parent = scope2;
        symbolTable._scopes[blockNode] = scope1;
        symbolTable._scopes[functionBlockNode] = scope2;

        Symbol testSymbol = new Symbol()
        {
            Name = "variable",
            Type = ASTNodes.TypeEnum.text
        };

        // Act
        symbolTable.AddIDToFunctionBlock(testSymbol, blockNode);
        
        // Assert
        That(functionBlockNode.UsedVariables.Keys, Does.Contain(testSymbol.Name));
    }
}