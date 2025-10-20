using Common.Utils;

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

    private BatFileExecutor batFileExecutor = new BatFileExecutor();
    private PowerShellExecutor powerShellExecutor = new PowerShellExecutor();
    public async Task Startup()
    {
        RunBatFiles();
        RunPowershelCommands();

        await Task.Delay(10 * 1000); // Wait some time to let commands finish
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo start", PowerShellMode.LeaveOpen);
    }

    private void RunPowershelCommands()
    {
        powerShellExecutor.RunPowerShellCommandAsAdmin("o vs elastic", PowerShellMode.CloseInTheEnd);
        powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb platform", PowerShellMode.CloseInTheEnd);
    }

    private void RunBatFiles()
    {
        batFileExecutor.Run("Chrome.bat");
        batFileExecutor.Run("Telegram.bat");
        batFileExecutor.Run("NotepadPlusPlus.bat");
        batFileExecutor.Run("Postman.bat");
        batFileExecutor.Run("Gemini.bat");
        batFileExecutor.Run("PowerShell.bat", runAsAdmin: true);
    }
}

