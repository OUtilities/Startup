namespace O.Profiles
{
    internal interface IProfile
    {
        Dictionary<string, string> GetRepoMappings();
        Dictionary<string, string> GetVsSolutionMappings();
    }
}
