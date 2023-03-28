using Antlr4.Runtime;
using static ASTNodes;

namespace CobraCompiler
{
    internal class Program
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

                //try
                //{
                var cst = parser.program();
                var ast = new BuildASTVisitor().VisitProgram(cst);
                var st = new SymbolTable().BuildSymbolTable(ast);
                new TypeChecker(st).VisitProgramNode((ProgramNode)ast);

                //var value = new EvaluateExpressionVisitor().Visit(ast);

                //Console.WriteLine("= {0}", value);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                //Console.WriteLine();
            }
        }
    }
}