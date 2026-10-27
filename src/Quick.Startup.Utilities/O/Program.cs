namespace O;

using BlackScreen;
using O.Extensions;
using O.Profiles;
using O.Profiles.EpicFlooring;

public class Program
{
    // [Environment]::SetEnvironmentVariable("O_PROFILE", "pillo", "User")
    // [Environment]::SetEnvironmentVariable("O_PROFILE", "fdef", "User")
    public static IProfile Profile { get; private set; } = CalculateProfile();

    static void Main(string[] args)
    {
        Profile = CalculateProfile();

        if (args.Length == 0)
        {
            Console.WriteLine("Usage: o bs | o vs <param> | o g <p|r> | o gitb <args>");
            return;
        }

        if (args[0].Equals("bs", StringComparison.OrdinalIgnoreCase))
        {
            var blackScreen = new BlackScreen();
            blackScreen.Run();
            return;
        }

        if (args[0].Equals("vs", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new Vs2022Utility();
            utility.Run(args.Skip(1).ToArray(), Profile.GetVsSolutionMappings());
            return;
        }

        if (args[0].Equals("vsc", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new VsCodeUtility();
            utility.Run(args.Skip(1).ToArray(), Profile.GetVsCodeFoldersMappings());
            return;
        }

        if (args[0].Equals("ssms", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new SsmsUtility();
            utility.Run();
            return;
        }

        if (args[0].Equals("g", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new GitExtentionUtility();
            utility.Run(args.Skip(1).ToArray());
            return;
        }

        if (args[0].Equals("gitb", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new GitBashUtility();
            utility.Run(args.Skip(1).ToArray(), Profile.GetRepoMappings());
            return;
        }

        if (args[0].Equals("start", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new ApplicationStarterUtility();
            utility.Run(args.Skip(1).ToArray());
            return;
        }
    }

    private static IProfile CalculateProfile()
    {
        string? profileValue = Environment.GetEnvironmentVariable("O_PROFILE");

        if (IsProfileEnabled(profileValue, "pillo"))
        {
            return new PilloProfile();
        }

        if (IsProfileEnabled(profileValue, "fdef"))
        {
            return new EpicFlooringProfile();
        }

        throw new InvalidOperationException($"Unknown profile: {profileValue}");
    }

    private static bool IsProfileEnabled(string? value, string profileName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return value.Trim().Equals(profileName, StringComparison.OrdinalIgnoreCase);
    }
}
