using System.Diagnostics;

namespace O.Extensions;

public class VsCodeUtility
{
    public void Run(string[] args, Dictionary<string, string> solutionMappings)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: o vsc <command>");
            Console.WriteLine("Available commands: " + string.Join(", ", solutionMappings.Keys));
            return;
        }

        string command = args[0];

        if (solutionMappings.TryGetValue(command, out var solutionPath))
        {
            if (!File.Exists(solutionPath) && !Directory.Exists(solutionPath))
            {
                Console.WriteLine($"The path '{solutionPath}' does not exist.");
                return;
            }

            OpenSolutionInVsCode(solutionPath);
        }
        else
        {
            Console.WriteLine($"Unknown command: {command}");
            Console.WriteLine("Available commands: " + string.Join(", ", solutionMappings.Keys));
        }
    }

    private static void OpenSolutionInVsCode(string solutionPath)
    {
        // Try to find VS Code executable in PATH
        string codeExe = "code";

        var psi = new ProcessStartInfo
        {
            FileName = codeExe,
            Arguments = $"\"{solutionPath}\"",
            UseShellExecute = false
        };

        try
        {
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to launch Visual Studio Code. Ensure it is installed and 'code' is available in your PATH.");
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}