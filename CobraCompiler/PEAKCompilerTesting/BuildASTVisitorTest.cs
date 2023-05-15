using Antlr4.Runtime;
using static NUnit.Framework.Assert;
using System;

namespace PEAKCompilerTesting;

[TestFixture]
public class BuildAstVisitorTest
{
    [Test]
    public void VisitProgram_nullTest()
    {
        // Arrange
        var exprText = "function hej(number x) return number { number x = 1; return x; } call hej(10);";

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
        
        // Act
        var ast = new BuildASTVisitor().VisitProgram(cst);

        var expected = new ASTNodes.ProgramNode()
        {
            Commands = new List<ASTNodes.CommandNode>()
            {
                new ASTNodes.DeclarationNode()
                {
                    Identifier = new ASTNodes.IdentifierNode()
                    {
                        Name = "x",
                        TypeNode = new ASTNodes.NumberNode()
                        {
                            
                        }
                    },
                    Expression = new ASTNodes.NumberNode()
                    {
                        Value = 5
                    }
                }
            }
        };
        
        Console.WriteLine(ast.GetType());

        // Assert
        Assert.That(expected, Is.TypeOf(ast.GetType())); 
    }
}