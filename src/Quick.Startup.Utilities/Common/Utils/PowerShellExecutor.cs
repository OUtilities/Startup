using System.Diagnostics;

namespace Common.Utils;

public enum PowerShellMode
{
    CloseInTheEnd,
    LeaveOpen
}

public class PowerShellExecutor
{
    public void RunPowerShellCommandAsAdmin(string command, PowerShellMode mode)
    {
        string noExitArg = mode == PowerShellMode.LeaveOpen ? "-NoExit " : "";
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"{noExitArg}-Command \"{command}\"",
            UseShellExecute = true,
            CreateNoWindow = false,
            Verb = "runas",
            WindowStyle = ProcessWindowStyle.Maximized
        };

        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting PowerShell as admin: {ex.Message}");
        }
    }
}