using System.Diagnostics;

namespace O.Extensions;

public class Vs2022Utility
{
    public void Run(string[] args, Dictionary<string, string> solutionMappings)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: o vs <command>");
            Console.WriteLine("Available commands: " + string.Join(", ", solutionMappings.Keys));
            return;
        }

        string command = args[0];

        if (solutionMappings.TryGetValue(command, out var solutionPath))
        {
            if (!File.Exists(solutionPath))
            {
                Console.WriteLine($"The solution file '{solutionPath}' does not exist.");
                return;
            }

            OpenSolutionInVisualStudio(solutionPath);
        }
        else
        {
            Console.WriteLine($"Unknown command: {command}");
            Console.WriteLine("Available commands: " + string.Join(", ", solutionMappings.Keys));
        }
    }

    private static void OpenSolutionInVisualStudio(string solutionPath)
    {
        // Try to find the default VS2022 path
        string vsPath = Environment.ExpandEnvironmentVariables(
            @"%ProgramFiles%\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe");

        if (!File.Exists(vsPath))
        {
            // Try Professional
            vsPath = Environment.ExpandEnvironmentVariables(
                @"%ProgramFiles%\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe");
        }
        if (!File.Exists(vsPath))
        {
            // Try Enterprise
            vsPath = Environment.ExpandEnvironmentVariables(
                @"%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\devenv.exe");
        }

        if (!File.Exists(vsPath))
        {
            Console.WriteLine("Visual Studio 2022 not found. Please ensure it is installed.");
            return;
        }

        var psi = new ProcessStartInfo
        {
            FileName = vsPath,
            Arguments = $"\"{solutionPath}\"",
            UseShellExecute = false
        };

        Process.Start(psi);
    }
}