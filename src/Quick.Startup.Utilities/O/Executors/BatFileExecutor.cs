using System.Diagnostics;

namespace Common.Utils;

public class BatFileExecutor
{
    public void Run(string batFilePath, string subFolder = "StartupPrograms", bool runAsAdmin = false)
    {
        string fullBatFilePath = Path.Combine(AppContext.BaseDirectory, subFolder, batFilePath);

        var process = new ProcessStartInfo
        {
            FileName = fullBatFilePath,
            WorkingDirectory = Path.GetDirectoryName(fullBatFilePath),
            UseShellExecute = true // Required to run .bat files
        };

        if (runAsAdmin)
        {
            process.Verb = "runas"; // This will prompt for elevation
        }

        Process.Start(process);
    }
}