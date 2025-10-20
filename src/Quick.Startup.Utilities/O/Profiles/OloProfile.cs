using Common.Utils;

namespace O.Profiles;

public class OloProfile : IProfile
{
    private BatFileExecutor batFileExecutor = new BatFileExecutor();
    private PowerShellExecutor powerShellExecutor = new PowerShellExecutor();
    public Dictionary<string, string> GetRepoMappings()
    {
        return new()
            {
                { "platform", @"C:\code\platform" },
                { "mapi", @"C:\code\menu-api" },
                { "imageapi", @"C:\code\image-api" },
            };
    }

    public Dictionary<string, string> GetVsSolutionMappings()
    {
        return new()
            {
                { "startup", @"C:\_Olo\Quick.Startup.Utilities\Quick.Startup.Utilities.sln" },

                { "serve", @"C:\code\platform\MobileWeb\MobileWeb.sln" },
                { "menu", @"C:\code\platform\Olo.Menus\Olo.Menus.sln" },
                { "menulogic", @"C:\code\platform\MenuLogic\Mobo.MenuLogic.sln" },
                { "dashboard", @"C:\code\platform\Dashboard\Dashboard.sln" },
                { "mobologic", @"C:\code\platform\MoboLogic\MoboLogic.sln" },
                { "elastic", @"C:\code\platform\MenuSearchIndexingService\MenuSearchIndexingService.sln" },

                { "imageapi", @"C:\code\image-api\ImageApi.sln" },
                { "mapi", @"C:\code\menu-api\Olo.Menus.Api.sln" },
            };
    }

    public async Task Startup()
    {
        RunBatFiles();
        RunPowershelCommands();

        await Task.Delay(10 * 1000); // Wait some time to let commands finish
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo start", PowerShellMode.LeaveOpen);
        // olo --help
        // olo build Admin
        // olo build Dashboard
        // olo build MenuAdmin
        // olo build MobileWeb
        // olo db sync
        // olo ff - Fast-forward cloned repositories registered in builder.json
        // olo aws localdev
    }

    private void RunPowershelCommands()
    {
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs serve", PowerShellMode.CloseInTheEnd);
        powerShellExecutor.RunPowerShellCommandAsAdmin("o vs elastic", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs olomenus", PowerShellMode.CloseInTheEnd);
        powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb platform", PowerShellMode.CloseInTheEnd);

        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs mapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb mapi", PowerShellMode.CloseInTheEnd);

        powerShellExecutor.RunPowerShellCommandAsAdmin("o vs imageapi", PowerShellMode.CloseInTheEnd);
        powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb imageapi", PowerShellMode.CloseInTheEnd);
    }

    private void RunBatFiles()
    {
        batFileExecutor.Run("Chrome.bat");
        batFileExecutor.Run("Telegram.bat");
        batFileExecutor.Run("Zoom.bat");
        batFileExecutor.Run("NotepadPlusPlus.bat");
        batFileExecutor.Run("Postman.bat");
        //batFileExecutor.Run("PowerShell.bat", runAsAdmin: true);
        batFileExecutor.Run("Gemini.bat");
    }
}
