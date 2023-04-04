using Antlr4.Runtime;
using System.Reflection.Emit;
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

                //try
                //{
                var cst = parser.program(); //CFG -> parse tree 
                var ast = new BuildASTVisitor().VisitProgram(cst); //parse tree -> AST
                var st = new SymbolTable().BuildSymbolTable(ast); //AST -> Symbol Table
                new TypeChecker(st).Visit((ProgramNode)ast); //AST, Symbol Table -> TypeChecker

                #region codeGeneration
                // Create a dynamic method to hold the generated code
                var method = new DynamicMethod("MyMethod", typeof(int), Type.EmptyTypes);
                var ilGenerator = method.GetILGenerator();


                var em = new Emitter(ilGenerator, st).Visit((ProgramNode)ast);

                // Emit a return opcode
                ilGenerator.Emit(OpCodes.Ret);

                // Create a delegate for the dynamic method
                var myMethod = (Func<int>)method.CreateDelegate(typeof(Func<int>));

                // Invoke the delegate to execute the generated code
                int result = myMethod();
                #endregion

                Console.WriteLine("DONE!");
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