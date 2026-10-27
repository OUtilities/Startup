using Common.Utils;

namespace O.Profiles.EpicFlooring
{
    public class EpicFlooringProfile : IProfile
    {
        private BatFileExecutor batFileExecutor = new BatFileExecutor();
        private PowerShellExecutor powerShellExecutor = new PowerShellExecutor();

        public Dictionary<string, string> GetRepoMappings()
        {
            return new()
            {
                { "utils", @"C:\_PetProjects\OUtilities\Startup" },
                { "ef", @"C:\_Projects\EpicFlooring\EpicFlooring" }
            };
        }

        public Dictionary<string, string> GetVsSolutionMappings()
        {
            return new()
            {
                { "utils", @"C:\_PetProjects\OUtilities\Startup\src\Quick.Startup.Utilities\Quick.Startup.Utilities.sln" },
                { "ef", @"C:\_Projects\EpicFlooring\EpicFlooring\EpicFlooring.sln" }
            };
        }
        public async Task Startup()
        {
            RunBatFiles();
            RunPowershelCommands();
        }

        public Dictionary<string, string> GetVsCodeFoldersMappings()
        {
            return new()
            {
                { "ef", @"C:\_Projects\EpicFlooring\EpicFlooring\src\EpicFlooring.Angular" }
            };
        }

        private void RunPowershelCommands()
        {
            powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb ef", PowerShellMode.CloseInTheEnd);
            powerShellExecutor.RunPowerShellCommandAsAdmin("o vs ef", PowerShellMode.CloseInTheEnd);
            powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc ef", PowerShellMode.CloseInTheEnd);
            powerShellExecutor.RunPowerShellCommandAsAdmin("o ssms", PowerShellMode.CloseInTheEnd);
        }

        private void RunBatFiles()
        {
            batFileExecutor.Run("Chrome.bat");
            batFileExecutor.Run("Telegram.bat");
            batFileExecutor.Run("NotepadPlusPlus.bat");
            batFileExecutor.Run("SnippingTool.bat");
            //batFileExecutor.Run("Postman.bat");
            batFileExecutor.Run("PowerShell.bat", runAsAdmin: true);
        }
    }
}
