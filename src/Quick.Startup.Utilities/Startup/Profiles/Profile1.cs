using Common.Utils;
// OLO
namespace Main.Profiles
{
    internal class Profile1 : IProfile
    {
        private BatFileExecutor batFileExecutor = new BatFileExecutor();
        private PowerShellExecutor powerShellExecutor = new PowerShellExecutor();
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
}
