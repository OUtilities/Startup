namespace O.Profiles;

// VelvetScreen
internal class VelvetScreenProfile : IProfile
{
    public Dictionary<string, string> GetRepoMappings()
    {
        return new()
            {
                { "utils", @"C:\_PetProjects\OUtilities\Startup" },
                { "vui", @"C:\_PetProjects\VelvetScreen\angular-monolith-ui" },
                { "vapi", @"C:\_PetProjects\VelvetScreen\angular-monolith-ui" },
            };
    }

    public Dictionary<string, string> GetVsSolutionMappings()
    {
        return new()
            {
                { "startup", @"C:\_Olo\Quick.Startup.Utilities\Quick.Startup.Utilities.sln" },

                { "vui", @"C:\_PetProjects\VelvetScreen\angular-monolith-ui" },
                { "vapi", @"C:\_PetProjects\VelvetScreen\angular-monolith-ui" },
            };
    }
}

