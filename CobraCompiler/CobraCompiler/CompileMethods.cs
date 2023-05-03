using System.Diagnostics;

namespace CobraCompiler;
using System;
using System.IO;
using System.Globalization;
using System.CodeDom.Compiler;
using System.Text;
using Microsoft.CSharp;
using System.Runtime.InteropServices;

public static class CompileMethods
{

    public static void CompileExecutable(string filePath)
    {
        string path;
        string command;
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;

        //Check operating system
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            //Set the command to run
            command = $"xcrun --sdk macosx --find mcs";

            startInfo.FileName = "bash";
            startInfo.Arguments = $"-c \"{command}\"";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            //Set path
            path = "\"C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\csc.exe\"";
            // Set the command to run
            command = $"{path} {filePath}";

            // Create a ProcessStartInfo instance
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C {command}";

        }
        else
        {
            throw new NotSupportedException("Operating system not supported");
        }

        // Create and start the process
        
        using (Process process = new Process())
        { 
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
                Console.WriteLine("Compilation succeeded.");
            }
        }
    }
}
