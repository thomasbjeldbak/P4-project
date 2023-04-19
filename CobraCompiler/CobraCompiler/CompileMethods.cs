namespace CobraCompiler;
using System;
using System.IO;
using System.Globalization;
using System.CodeDom.Compiler;
using System.Text;
using Microsoft.CSharp;
public static class CompileMethods
{

    public static bool CompileExecutable(string sourceName)
    {
        FileInfo sourceFile = new FileInfo(sourceName);
        CodeDomProvider provider = null;
        bool compileOk = false;

        //Create provider with C# as language
        provider = CodeDomProvider.CreateProvider("CSharp");


        // Format the executable file name.
        // Build the output assembly path using the current directory
        // and <source>_cs.exe or <source>_vb.exe.

        String exeName = String.Format(@"{0}\{1}.exe",
            System.Environment.CurrentDirectory,
            sourceFile.Name.Replace(".", "_"));

        CompilerParameters cp = new CompilerParameters();

        // Generate an executable instead of
        // a class library.
        cp.GenerateExecutable = true;

        // Specify the assembly file name to generate.
        cp.OutputAssembly = exeName;

        // Save the assembly as a physical file.
        cp.GenerateInMemory = false;

        // Set whether to treat all warnings as errors.
        cp.TreatWarningsAsErrors = false;

        // Invoke compilation of the source file.
        CompilerResults cr = provider.CompileAssemblyFromFile(cp,
            sourceName);

        if (cr.Errors.Count > 0)
        {
            // Display compilation errors.
            Console.WriteLine("Errors building {0} into {1}",
                sourceName, cr.PathToAssembly);
            foreach (CompilerError ce in cr.Errors)
            {
                Console.WriteLine("  {0}", ce.ToString());
                Console.WriteLine();
            }
        }
        else
        {
            // Display a successful compilation message.
            Console.WriteLine("Source {0} built into {1} successfully.",
                sourceName, cr.PathToAssembly);
        }

        // Return the results of the compilation.
        if (cr.Errors.Count > 0)
        {
            compileOk = false;
        }
        else
        {
            compileOk = true;
        }



        return compileOk;
    }
}