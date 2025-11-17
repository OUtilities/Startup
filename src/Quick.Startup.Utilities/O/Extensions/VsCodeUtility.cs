using System.Diagnostics;

namespace O.Extensions;

public class VsCodeUtility
{
    public void Run(string[] args, Dictionary<string, string> folderMappings)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: o vsc <command>");
            Console.WriteLine("Available commands: " + string.Join(", ", folderMappings.Keys));
            return;
        }

        string command = args[0];

        if (folderMappings.TryGetValue(command, out var folderPath))
        {
            if (!File.Exists(folderPath) && !Directory.Exists(folderPath))
            {
                Console.WriteLine($"The path '{folderPath}' does not exist.");
                return;
            }

            OpenFolderInVsCode(folderPath);
        }
        else
        {
            Console.WriteLine($"Unknown command: {command}");
            Console.WriteLine("Available commands: " + string.Join(", ", folderMappings.Keys));
        }
    }

    private static void OpenFolderInVsCode(string solutionPath)
    {
        // Try to find VS Code executable in PATH
        string codeExe = @"""C:\Users\oleg.lazarovych\AppData\Local\Programs\Microsoft VS Code\Code.exe""";

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