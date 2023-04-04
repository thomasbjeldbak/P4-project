using Antlr4.Runtime;
using System.Reflection;
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
                // Create an assembly and module to hold the generated code
                var method = new DynamicMethod("MyMethod", typeof(int), Type.EmptyTypes);
                var ilGenerator = method.GetILGenerator();
                #region newTestCode
                //var assemblyName = new AssemblyName("MyAssembly");
                //var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
                //var moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);

                ////Create a type and method to hold the generated code
                //var typeBuilder = moduleBuilder.DefineDynamicModule("MyModule");
                //typeBuilder.DefineType("MyType", TypeAttributes.Public);
                //var methodBuilder = typeBuilder.DefineGlobalMethod("MyMethod", MethodAttributes.Public | MethodAttributes.Static, typeof(int), Type.EmptyTypes);
                //var ilGenerator = methodBuilder.GetILGenerator();

                ////Create an instance of the Emitter class and generate code
                //var em = new Emitter(ilGenerator, st).Visit((ProgramNode)ast);

                //// Emit a return opcode
                //ilGenerator.Emit(OpCodes.Ret);

                ////Create the type
                //var type = typeBuilder;

                ////Get a reference to the generated method
                //var method = type.GetMethod("MyMethod");

                ////Create a delegate for the generated method
                //var myMethod = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), method);

                ////Invoke the delegate to execute the generated code
                //int result = myMethod();
                #endregion


                #region OldCode
                var em = new Emitter(ilGenerator, st).Visit((ProgramNode)ast);

                // Emit a return opcode
                ilGenerator.Emit(OpCodes.Ret);

                // Create a delegate for the dynamic method
                var myMethod = (Func<int>)method.CreateDelegate(typeof(Func<int>));

                // Invoke the delegate to execute the generated code
                int result = myMethod();
                #endregion

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