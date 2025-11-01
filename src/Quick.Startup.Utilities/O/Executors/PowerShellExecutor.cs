using System.Diagnostics;

namespace Common.Utils;

public enum PowerShellMode
{
    CloseInTheEnd,
    LeaveOpen
}

public class PowerShellExecutor
{
    public void RunPowerShellCommandAsAdmin(string command, PowerShellMode mode, ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal)
    {
        string noExitArg = mode == PowerShellMode.CloseInTheEnd ? "" : "-NoExit ";
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"{noExitArg}-Command \"{command}\"",
            UseShellExecute = true,
            CreateNoWindow = false,
            Verb = "runas",
            WindowStyle = windowStyle
        };

        try
        {
            using (Process process = Process.Start(startInfo))
            {
                if (process != null)
                {
                    if (mode == PowerShellMode.CloseInTheEnd)
                    {
                        process.WaitForExit();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting PowerShell as admin: {ex.Message}");
        }
    }
}