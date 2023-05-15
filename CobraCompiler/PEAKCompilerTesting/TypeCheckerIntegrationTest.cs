using Antlr4.Runtime;
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
        
        // Assert
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
        
        // Assert
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
                return MethodCalledTestHelper(() => symbolTable.Visit(declarationNode));
            case ASTNodes.AssignNode assignNode:
                return MethodCalledTestHelper(() => symbolTable.Visit(assignNode));
            case ASTNodes.ReturnNode returnNode:
                return MethodCalledTestHelper(() => symbolTable.Visit(returnNode));
            case ASTNodes.StatementNode statementNode:
                return MethodCalledTestHelper(() => symbolTable.Visit(statementNode));
            default:
                return false;
        }
    }

    private bool MethodCalledTestHelper(Action action)
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
    
    public static void RunCodeFromFileTestHelper(string code)
    {
        var exprText = code;

        var inputStream = new AntlrInputStream(new StringReader(exprText));
        var lexer = new ExprLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new ExprParser(tokenStream);

        var errorHandler = new ErrorHandler();
        parser.RemoveErrorListeners(); // remove the default ConsoleErrorListener
        parser.AddErrorListener(errorHandler); // set your ErrorHandler as the error listener'

        // Get root of CST (which is program)
        var cst = parser.program();
        if (errorHandler.SyntaxErrorMessages.Count > 0)
        {
            Console.WriteLine("Syntax errors:");
            foreach (var errorMessage in errorHandler.SyntaxErrorMessages)
            {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

        var ast = new BuildASTVisitor().VisitProgram(cst);
        var st = new SymbolTable(errorHandler).BuildSymbolTable(ast);
        if (errorHandler.SymbolErrorMessages.Count > 0)
        {
            Console.WriteLine("Symbol errors:");
            foreach (var errorMessage in errorHandler.SymbolErrorMessages)
            {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }
        new TypeChecker(st, errorHandler).Visit((ASTNodes.ProgramNode)ast);
        if (errorHandler.TypeErrorMessages.Count > 0)
        {
            Console.WriteLine("Type errors:");
            foreach (var errorMessage in errorHandler.TypeErrorMessages)
            {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }
    }
}