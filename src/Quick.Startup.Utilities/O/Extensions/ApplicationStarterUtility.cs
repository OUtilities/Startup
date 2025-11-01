using Common.Utils;
using System.Diagnostics;
namespace O.Extensions;

internal class ApplicationStarterUtility
{
    private PowerShellExecutor powerShellExecutor = new PowerShellExecutor();

    public void Run(string[] args)
    {
        if (args.Length == 0)
        {
            StartMES();
            StartImageApi();
            return;
        }
        string application = args[0].ToLower();
        switch (application)
        {
            case "mes":
                StartMES();
                break;
            case "imageapi":
                StartImageApi();
                break;
            default:
                Console.WriteLine($"Unknown application: {application}");
                Console.WriteLine("Available applications: mes, imageapi");
                break;
        }
    }

    public void StartMES()
    {
        powerShellExecutor.RunPowerShellCommandAsAdmin(@"cd C:\code\platform\Olo.Menus.Export; .\Docker.ps1 LocalDev start", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);

        powerShellExecutor.RunPowerShellCommandAsAdmin(@"dotnet run --project C:\code\platform\Olo.Menus.Export\src\Olo.Menus.Export.SupportService\Olo.Menus.Export.SupportService.csproj --launch-profile Olo.Menus.Export.SupportService", PowerShellMode.LeaveOpen);
        powerShellExecutor.RunPowerShellCommandAsAdmin(@"dotnet run --project C:\code\platform\Olo.Menus.Export\src\Olo.Menus.Export.Worker\Olo.Menus.Export.Worker.csproj --launch-profile Olo.Menus.Export.Worker", PowerShellMode.LeaveOpen);
    }

    public void StartImageApi()
    {
        powerShellExecutor.RunPowerShellCommandAsAdmin(@"dotnet run --project C:\code\image-api\ImageApi.MockImgix\ImageApi.MockImgix.csproj --launch-profile ImageApi.MockImgix", PowerShellMode.LeaveOpen);
        powerShellExecutor.RunPowerShellCommandAsAdmin(@"dotnet run --project C:\code\image-api\src\ImageApi.fsproj --launch-profile ImageApi", PowerShellMode.LeaveOpen);
    }
}
