using Antlr4.Runtime;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Antlr4.Runtime.Tree.Xpath;
using static ASTNodes;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace CobraCompiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var exprText = File.ReadAllText("../../../Code.txt");

            if (string.IsNullOrWhiteSpace(exprText))
            {
                Console.Write("> ");
                exprText = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(exprText))
                    return;

            var inputStream = new AntlrInputStream(new StringReader(exprText));
            var lexer = new ExprLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ExprParser(tokenStream);

            var errorHandler = new ErrorHandler();
            parser.RemoveErrorListeners(); // remove the default ConsoleErrorListener
            parser.AddErrorListener(errorHandler); // set your ErrorHandler as the error listener'

            //Get root of CST (which is program)
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
            new TypeChecker(st, errorHandler).Visit((ProgramNode)ast);
            if (errorHandler.TypeErrorMessages.Count > 0)
            {
                Console.WriteLine("Type errors:");
                foreach (var errorMessage in errorHandler.TypeErrorMessages)
                {
                    Console.WriteLine(errorMessage);
                }
                Environment.Exit(1);
            }

            //#region CodeGeneration
            //var sb = new StringBuilder();
            //sb = new Emitter(sb, st).Visit((ProgramNode)ast);

            //string path = Directory.GetCurrentDirectory();
            //path += "\\GeneratedProgram.cs";

            //File.WriteAllText("GeneratedProgram.cs", sb.ToString());
            //CompileMethods.CompileExecutable(path);

            //#endregion

            Console.WriteLine("DONE!");
        
        }
    }
}
