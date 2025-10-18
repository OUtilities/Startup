using System.Diagnostics;

namespace Git.Utility;

public class GitBashUtility
{
    public void Run(string[] args, Dictionary<string, string> commandPaths)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: o gitb <command>");
            Console.WriteLine("Available commands: " + string.Join(", ", commandPaths.Keys));
            return;
        }

        string command = args[0];

        HandleCommand(command, commandPaths);
    }

    private static void HandleCommand(string command, Dictionary<string, string> commandPaths)
    {
        if (commandPaths.TryGetValue(command, out var path))
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"The path '{path}' does not exist.");
                return;
            }

            LaunchGitBash(path);
        }
        else
        {
            Console.WriteLine($"Unknown command: {command}");
            Console.WriteLine("Available commands: " + string.Join(", ", commandPaths.Keys));
        }
    }

    private static void LaunchGitBash(string path)
    {
        string gitBashPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Git\git-bash.exe");
        if (!File.Exists(gitBashPath))
        {
            gitBashPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles(x86)%\Git\git-bash.exe");
        }

        if (!File.Exists(gitBashPath))
        {
            Console.WriteLine("git-bash.exe not found. Please ensure Git Bash is installed and in your PATH.");
            return;
        }

        var psi = new ProcessStartInfo
        {
            FileName = gitBashPath,
            WorkingDirectory = path,
            UseShellExecute = false
        };

        Process.Start(psi);
    }
}