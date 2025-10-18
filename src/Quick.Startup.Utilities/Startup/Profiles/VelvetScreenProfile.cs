using Common.Utils;
using Main.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Startup.Profiles
{
    internal class VelvetScreenProfile: IProfile
    {
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
        }
    }
}
