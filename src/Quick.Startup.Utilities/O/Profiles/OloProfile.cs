using Common.Utils;
using O.Extensions;
using System.Diagnostics;

namespace O.Profiles;
#region CheatSheet

// olo --help
// olo build Admin
// olo build Dashboard
// olo build MenuAdmin
// olo build MobileWeb
// olo db sync
// olo ff - Fast-forward cloned repositories registered in builder.json
// olo aws localdev
// olo consul update Local
// dotnet tool update -g olo-builder

// cd C:\code\platform\Admin\src
// npm i
// olo build Admin
// cd C:\code\platform\Dashboard\src
// npm i
// olo build Dashboard
// olo build MenuAdmin
// olo build MobileWeb
// cd C:\code\platform\CallCenter\src
// npm i
// olo build CallCenter
#endregion

public class OloProfile : IProfile
{
    private BatFileExecutor batFileExecutor = new BatFileExecutor();
    private PowerShellExecutor powerShellExecutor = new PowerShellExecutor();
    public Dictionary<string, string> GetRepoMappings()
    {
        return new()
            {
                { "utils", @"C:\_PetProjects\OUtilities\Startup" },

                { "platform", @"C:\code\platform" },
                { "mapi", @"C:\code\menu-api" },
                { "imageapi", @"C:\code\image-api" },
            };
    }

    public Dictionary<string, string> GetVsSolutionMappings()
    {
        return new()
            {
            
                { "utils", @"C:\_PetProjects\OUtilities\Startup\src\Quick.Startup.Utilities\Quick.Startup.Utilities.sln" },

                { "serve", @"C:\code\platform\MobileWeb\MobileWeb.sln" },
                { "menu", @"C:\code\platform\Olo.Menus\Olo.Menus.sln" },
                { "menulogic", @"C:\code\platform\MenuLogic\Mobo.MenuLogic.sln" },
                { "menuadmin", @"C:\code\platform\MenuAdmin\MenuAdmin.sln" },
                { "dashboard", @"C:\code\platform\Dashboard\Dashboard.sln" },
                { "mobologic", @"C:\code\platform\MoboLogic\MoboLogic.sln" },
                { "mes", @"C:\code\platform\Olo.Menus.Export\Olo.Menus.Export.sln" },
                { "elastic", @"C:\code\platform\MenuSearchIndexingService\MenuSearchIndexingService.sln" },

                { "imageapi", @"C:\code\image-api\ImageApi.sln" },
                { "mapi", @"C:\code\menu-api\Olo.Menus.Api.sln" },
            };
    }

    public Dictionary<string, string> GetVsCodeFoldersMappings()
    {
        return new()
            {
                { "oct", @"C:\code\octopus-configurations" },
                { "tss", @"C:\code\terraform-state-staging-environment" },
                { "tsl", @"C:\code\terraform-state-live-environment" },
            };
    }

    public async Task Startup()
    {
        RunBatFiles(); // Start common applications that I use for work
        await RunPowershelCommands(); // Start development environment related commands

        var starter = new ApplicationStarterUtility();
        //starter.StartImageApi();
        //starter.StartMES(startDockerPostgresDatabase: true);
    }

    private void RunBatFiles()
    {
        batFileExecutor.Run("Chrome.bat");
        batFileExecutor.Run("Telegram.bat");
        batFileExecutor.Run("Zoom.bat");
        batFileExecutor.Run("NotepadPlusPlus.bat");
        //batFileExecutor.Run("Postman.bat");
        batFileExecutor.Run("PowerShell.bat", runAsAdmin: true);
        batFileExecutor.Run("Gemini.bat");
    }

    private async Task RunPowershelCommands()
    {
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb platform", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs serve", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs elastic", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs olomenus", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs mes", PowerShellMode.CloseInTheEnd);

        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb mapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs mapi", PowerShellMode.CloseInTheEnd);

        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb imageapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs imageapi", PowerShellMode.CloseInTheEnd);

        powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc oct", PowerShellMode.CloseInTheEnd);
        await Task.Delay(5 * 1000); // Wait some time to let commands finish
        powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc tss", PowerShellMode.CloseInTheEnd);
        await Task.Delay(5 * 1000); // Wait some time to let commands finish
        powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc tsl", PowerShellMode.CloseInTheEnd);
        await Task.Delay(5 * 1000); // Wait some time to let commands finish

        powerShellExecutor.RunPowerShellCommandAsAdmin("start chrome https://olo.login.duosecurity.com/central/", PowerShellMode.CloseInTheEnd);

        await Task.Delay(15 * 1000); // Wait some time to let commands finish
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo stop; Start-Sleep -Seconds 30", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo start; Start-Sleep -Seconds 30", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
    }
}
