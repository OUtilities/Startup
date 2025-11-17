namespace O;

using BlackScreen;
using O.Extensions;
using O.Profiles;

class Program
{
    private static readonly IProfile profile = new OloProfile();

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            // Add other command handling or default behavior here
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
            utility.Run(args.Skip(1).ToArray(), profile.GetVsSolutionMappings());
            return;
        }
        if (args[0].Equals("vsc", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new VsCodeUtility();
            utility.Run(args.Skip(1).ToArray(), profile.GetVsCodeFoldersMappings());
            return;
        }
        if (args[0].Equals("g", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new GitExtentionUtility();
            // Pass all arguments after "g" to the utility
            utility.Run(args.Skip(1).ToArray());
            return;
        }
        if (args[0].Equals("gitb", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new GitBashUtility();
            utility.Run(args.Skip(1).ToArray(), profile.GetRepoMappings());
            return;
        }
        if (args[0].Equals("start", StringComparison.OrdinalIgnoreCase))
        {
            var utility = new ApplicationStarterUtility();
            utility.Run(args.Skip(1).ToArray());
            return;
        }
    }
}