namespace O;

using BlackScreen;
using O.Extensions;
using O.Profiles;

class Program
{
    static void Main(string[] args)
    {
        IProfile profile = new Profile1();
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
            var vsUtility = new Vs2022Utility();
            vsUtility.Run(args.Skip(1).ToArray(), profile.GetVsSolutionMappings());
            return;
        }
        if (args[0].Equals("g", StringComparison.OrdinalIgnoreCase))
        {
            var gitUtility = new GitExtentionUtility();
            // Pass all arguments after "g" to the utility
            gitUtility.Run(args.Skip(1).ToArray());
            return;
        }
        if (args[0].Equals("gitb", StringComparison.OrdinalIgnoreCase))
        {
            var gitBashUtility = new GitBashUtility();
            gitBashUtility.Run(args.Skip(1).ToArray(), profile.GetRepoMappings());
            return;
        }
    }
}