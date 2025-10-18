using System.Diagnostics;

namespace Git.Extentions.Utility;

public class GitExtentionUtility
{
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

        switch (command)
        {
            case "p":
                string branchP = args[1];
                if (branchP == null)
                {
                    Console.WriteLine("Could not determine current branch.");
                    return;
                }
                string[] allowedBranches = { "master", "main", "develop" };
                if (!allowedBranches.Any(b => branchP.Equals(b, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Current branch is '{branchP}'. 'o g p' can only be run on the following branches: {string.Join(", ", allowedBranches)}.");
                    return;
                }
                RunGitCommands(new[] { $"git pull origin {branchP}" });
                break;
            case "r":
                if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
                {
                    string rebaseBranchName = args[1];
                    RunGitCommands(new[] { $"git rebase {rebaseBranchName}" });
                }
                else
                {
                    Console.WriteLine("Usage: o g r <branch_name>");
                }
                break;
            case "ri":
                if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
                {
                    Console.WriteLine("Usage: o g ri <branch_name>");
                    return;
                }
                string interactiveBranchName = args[1];
                RunGitCommands(new[] { $"git rebase -i {interactiveBranchName}" });
                break;
            case "s":
                RunGitCommands(new[] { "git stash" });
                break;
            case "sp":
                RunGitCommands(new[] { "git stash pop" });
                break;
            case "c":
                if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
                {
                    Console.WriteLine("Usage: o g c <branch_name>");
                    return;
                }
                string branchName = args[1];
                RunGitCommands(new[] { $"git checkout {branchName}" });
                break;
            case "cb":
                if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
                {
                    Console.WriteLine("Usage: o g cb <branch_name>");
                    return;
                }
                string newBranchName = args[1];
                RunGitCommands(new[] { $"git checkout -b {newBranchName}" });
                break;
            case "pf":
                if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
                {
                    Console.WriteLine("Usage: o g pf <branch_name>");
                    return;
                }
                string forceBranchName = args[1];
                RunGitCommands(new[] { $"git push --force origin {forceBranchName}" });
                break;
            default:
                Console.WriteLine("Unknown command. Use 'o g p', 'o g r [branch_name]', 'o g ri <branch_name>', 'o g s', 'o g sp', 'o g c <branch_name>', 'o g cb <branch_name>', or 'o g pf <branch_name>'.");
                break;
        }
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