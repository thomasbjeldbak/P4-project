using System.Diagnostics;

namespace CobraCompiler;
using System;
using System.IO;
using System.Globalization;
using System.CodeDom.Compiler;
using System.Text;
using Microsoft.CSharp;

public static class CompileMethods
{

    public static void CompileExecutable(string filePath)
    {
        // Set the command to run
        string command = $"dotnet build {filePath} -o build";

        // Create a ProcessStartInfo instance
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = $"/C {command}";
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;

        // Start the process
        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        // Read the output
        string output = process.StandardOutput.ReadToEnd();

        // Wait for the process to exit
        process.WaitForExit();

        // Check for errors
        if (process.ExitCode != 0)
        {
            Console.WriteLine("Compilation failed with errors:");
            Console.WriteLine(output);
        }
        else
        {
            Console.WriteLine("Compilation succeeded. Output directory: build");
        }
    }
}
