using Antlr4.Runtime;
using Antlr4.Runtime.Tree.Xpath;
using static ASTNodes;

namespace CobraCompiler
{
    internal abstract class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                var exprText = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(exprText))
                    break;

                var inputStream = new AntlrInputStream(new StringReader(exprText));
                var lexer = new ExprLexer(inputStream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new ExprParser(tokenStream);

                var errorHandler = new ErrorHandler();
                parser.RemoveErrorListeners(); // remove the default ConsoleErrorListener
                parser.AddErrorListener(errorHandler); // set your ErrorHandler as the error listener'
                
                var cst = parser.program();
                    if (errorHandler.ErrorMessages.Count > 0)
                    {
                        Console.WriteLine("Syntax errors:");
                        foreach (var errorMessage in errorHandler.ErrorMessages)
                        {
                            Console.WriteLine(errorMessage);
                        }
                        Environment.Exit(1);
                    }

                    try
                    {
                    var ast = new BuildASTVisitor().VisitProgram(cst);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    try
                    {
                    var st = new SymbolTable().BuildSymbolTable(ast);
                    }
                    catch (Exception ex.Message)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    try
                    {
                    new TypeChecker(st).visitBlockNode((ProgramNode)ast);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    Console.WriteLine("DONE!");
                }
            }
        }
    }
}