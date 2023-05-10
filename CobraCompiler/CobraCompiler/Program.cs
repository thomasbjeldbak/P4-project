using Antlr4.Runtime;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Antlr4.Runtime.Tree.Xpath;
using static ASTNodes;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

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

            #region CodeGeneration

            StringBuilder sb = new Emitter(st).Visit((ProgramNode)ast);

            string tempPath = Directory.GetCurrentDirectory();
            string path;
            //We check to see what Operating system is used due to pathing.
            //For Mac we need to use "/"
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                path = Path.GetFullPath(Path.Combine(tempPath, @"../../../GeneratedProgram"));
                
            }
            //Check to see if operating system is Windows because pathing needs to be used with "\"
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.GetFullPath(Path.Combine(tempPath, @"..\..\..\GeneratedProgram"));
                
            } 
            //If any other operating system is used an exception is thrown.
            else{
                throw new NotSupportedException("Operating system not supported");
            }
            
            //string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "\\..\\..\\..\\GeneratedProgram.c"));
            //path += "\\GeneratedProgram.c";
            //path += "\\..\\..\\..\\GeneratedProgram.c";

            File.WriteAllText($"{path}.c", sb.ToString());
            CompileMethods.CompileExecutable(path);

            #endregion

            Console.WriteLine("DONE!");
        
        }
    }
}
