﻿using Antlr4.Runtime;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Antlr4.Runtime.Tree.Xpath;
using static ASTNodes;

namespace CobraCompiler
{
    internal abstract class Program
    {
        static void Main(string[] args)
        {
            var exprText = File.ReadAllText("../../../code.txt");

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
                }
                new TypeChecker(st, errorHandler).Visit((ProgramNode)ast);
                if (errorHandler.TypeErrorMessages.Count > 0)
                {
                    Console.WriteLine("Type errors:");
                    foreach (var errorMessage in errorHandler.TypeErrorMessages)
                    {
                        Console.WriteLine(errorMessage);
                    }
                }

                #region CodeGeneration
                var sb = new StringBuilder();
                sb = new Emitter(sb, st).Visit((ProgramNode)ast);

                File.WriteAllText("../GeneratedProgram.txt", sb.ToString());

                #endregion

            Console.WriteLine("DONE!");
        
        }
    }
}
