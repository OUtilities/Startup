using System.Diagnostics;

namespace O.Extensions;

public class SsmsUtility
{
    public void Run()
    {
        OpenSsms();
    }

    private static void OpenSsms()
    {
        // Common SSMS installation locations (try newer versions first)
        var candidates = new[]
        {
            @"%ProgramFiles%\Microsoft SQL Server Management Studio 19\Common7\IDE\Ssms.exe",
            @"%ProgramFiles(x86)%\Microsoft SQL Server Management Studio 19\Common7\IDE\Ssms.exe",
            @"%ProgramFiles%\Microsoft SQL Server Management Studio 18\Common7\IDE\Ssms.exe",
            @"%ProgramFiles(x86)%\Microsoft SQL Server Management Studio 18\Common7\IDE\Ssms.exe",
            @"%ProgramFiles%\Microsoft SQL Server\140\Tools\Binn\ManagementStudio\Ssms.exe",
            @"%ProgramFiles(x86)%\Microsoft SQL Server\140\Tools\Binn\ManagementStudio\Ssms.exe",
            @"%ProgramFiles%\Microsoft SQL Server Management Studio 22\Release\Common7\IDE\SSMS.exe"
        };

        string exePath = null;
        foreach (var raw in candidates)
        {
            try
            {
                var expanded = Environment.ExpandEnvironmentVariables(raw);
                if (File.Exists(expanded))
                {
                    exePath = expanded;
                    break;
                }
            }
            catch { }
        }

        if (string.IsNullOrEmpty(exePath))
        {
            Console.WriteLine("SQL Server Management Studio (SSMS) not found. Please ensure it is installed.");
            return;
        }

        var psi = new ProcessStartInfo
        {
            FileName = exePath,
            UseShellExecute = false
        };

        try
        {
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to launch SSMS. Ensure it is installed and accessible.");
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
