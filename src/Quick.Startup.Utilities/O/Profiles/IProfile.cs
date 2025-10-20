namespace O.Profiles
{
    public interface IProfile
    {
        Task Startup();
        Dictionary<string, string> GetRepoMappings();
        Dictionary<string, string> GetVsSolutionMappings();
    }
}
