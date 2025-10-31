using System.Diagnostics;

namespace O.Extensions;

public class GitExtentionUtility
{
    private string[] allowedBranches = { "master", "main", "develop" };
    public void Run(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine(
                @"Usage: o g <command> [branch_name]

                Commands:
                  p                git pull origin <current_branch> (only on master, main, develop)
                  r <branch_name>  git rebase <branch_name>
                  ri <branch_name> git rebase -i <branch_name>
                  s                git stash
                  sp               git stash pop
                  c <branch_name>  git checkout <branch_name>
                  cb <branch_name> git checkout -b <branch_name>
                  pf <branch_name> git push --force origin <branch_name>
                "
            );
            return;
        }

        string command = args[0].ToLower();
        string branchName = GetBranchName(args);
        switch (command)
        {
            case "p":               
                if (!this.allowedBranches.Any(b => branchName.Equals(b, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Current branch is '{branchName}'. 'o g p' can only be run on the following branches: {string.Join(", ", this.allowedBranches)}.");
                    return;
                }
                RunGitCommands(new[] { $"git pull origin {branchName}" });
                break;
            case "r":
                if (!this.allowedBranches.Any(b => branchName.Equals(b, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Current branch is '{branchName}'. 'o g p' can only be run on the following branches: {string.Join(", ", this.allowedBranches)}.");
                    return;
                }
                RunGitCommands(new[] { $"git rebase {branchName}" });
                break;
            case "ri":
                if (!this.allowedBranches.Any(b => branchName.Equals(b, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Current branch is '{branchName}'. 'o g p' can only be run on the following branches: {string.Join(", ", this.allowedBranches)}.");
                    return;
                }
                RunGitCommands(new[] { $"git rebase {branchName}" });
                RunGitCommands(new[] { $"git rebase -i {branchName}" });
                break;
            case "pf":
                RunGitCommands(new[] { $"git push --force origin {branchName}" });
                break;
            case "c":
                RunGitCommands(new[] { $"git checkout {branchName}" });
                break;
            case "cb":
                RunGitCommands(new[] { $"git checkout -b {branchName}" });
                break;
            case "cm":
                if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
                {
                    Console.WriteLine("Usage: o g cm <message>");
                    return;
                }
                string message = args[1];
                RunGitCommands(new[] { $"git commit -m \"{message}\"" });
                break;
            case "s":
                RunGitCommands(new[] { "git stash" });
                break;
            case "sp":
                RunGitCommands(new[] { "git stash pop" });
                break;
            default:
                Console.WriteLine("Unknown command. Use 'o g p', 'o g r [branch_name]', 'o g ri <branch_name>', 'o g s', 'o g sp', 'o g c <branch_name>', 'o g cb <branch_name>', or 'o g pf <branch_name>'.");
                break;
        }
    }

    private static string GetBranchName(string[] args)
    {
        if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            return null;
        }
        string branchP = args[1].ToLower();
        if (branchP == "d")
        {
            branchP = "develop";
        }
        if (branchP == "ms")
        {
            branchP = "master";
        }
        if (branchP == "mn")
        {
            branchP = "main";
        }

        return branchP;
    }

    private static void RunGitCommands(string[] commands)
    {
        foreach (string cmd in commands)
        {
            if (!RunGitCommand(cmd))
            {
                Console.WriteLine($"Command failed: {cmd}");
                break;
            }
        }
    }

    private static bool RunGitCommand(string command)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c {command}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi);
        process.WaitForExit();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        if (!string.IsNullOrWhiteSpace(output))
            Console.WriteLine(output);
        if (!string.IsNullOrWhiteSpace(error))
            Console.WriteLine(error);

        return process.ExitCode == 0;
    }
}