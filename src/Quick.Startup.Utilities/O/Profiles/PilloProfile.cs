using Common.Utils;

namespace O.Profiles;

public class PilloProfile : IProfile
{

    private BatFileExecutor batFileExecutor = new BatFileExecutor();
    private PowerShellExecutor powerShellExecutor = new PowerShellExecutor();

    public Dictionary<string, string> GetRepoMappings()
    {
        return new()
            {
                { "utils", @"C:\_PetProjects\OUtilities\Startup" },
                { "papi", @"C:\_PetProjects\PilloOrganization\MonolithApi" },
                { "ui", @"C:\_PetProjects\PilloOrganization\MobileApp-ReactNative" }
            };
    }

    public Dictionary<string, string> GetVsSolutionMappings()
    {
        return new()
            {
                { "utils", @"C:\_PetProjects\OUtilities\Startup\src\Quick.Startup.Utilities\Quick.Startup.Utilities.sln" },
                { "papi", @"C:\_PetProjects\PilloOrganization\MonolithApi\Api\src\Api.sln" }
            };
    }
    public async Task Startup()
    {
        //RunBatFiles();
        RunPowershelCommands();
    }

    public Dictionary<string, string> GetVsCodeFoldersMappings()
    {
        return new()
            {
                { "ui", @"C:\_PetProjects\PilloOrganization\MobileApp-ReactNative\src\app" }
            };
    }

    private void RunPowershelCommands()
    {
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs papi", PowerShellMode.CloseInTheEnd);
        powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc ui", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb papi", PowerShellMode.CloseInTheEnd);

    }

    private void RunBatFiles()
    {
        batFileExecutor.Run("Chrome.bat");
        batFileExecutor.Run("Telegram.bat");
        //batFileExecutor.Run("NotepadPlusPlus.bat");
        //batFileExecutor.Run("Postman.bat");
        //batFileExecutor.Run("Gemini.bat");
        //batFileExecutor.Run("PowerShell.bat", runAsAdmin: true);
    }
}

