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

                try
                {
                    var cst = parser.program();
                    var ast = new BuildASTVisitor().VisitProgram(cst);
                    var st = new SymbolTable().BuildSymbolTable(ast);
                    new TypeChecker(st).visitBlockNode((ProgramNode)ast);
                    Console.WriteLine("DONE!");
                    //var value = new EvaluateExpressionVisitor().Visit(ast);

                    //Console.WriteLine("= {0}", value);
                }
                catch (SyntaxException ex)
                {
                    Console.WriteLine($"SyntaxError: Line{ex.Line} column{ex.Column}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}