using Common.Utils;
using O.Extensions;
using System.Diagnostics;

namespace O.Profiles;
#region CheatSheet

// olo --help
// olo config
// olo ff - Fast-forward cloned repositories registered in builder.json
// olo db sync
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

// git rebase develop
// git rebase -i HEAD~2

// .\Docker.ps1 LocalDev start
// docker system prune -a - clean up space on disk
// docker volume prune -a
// docker container ls -a --filter "name=mes"
// docker container rm <container_id>
// docker images --filter "reference=*mes*"
// docker rmi <image_id>
// docker build -t mes-supportservice:localdev .
// docker build -t mes-supportservice:localdev -f ./Olo.Menus.Export/src/Olo.Menus.Export.Worker/Dockerfile .
// docker run --name mes-worker -e AWS_REGION=us-east-1 -e CONSUL_HTTP_ADDR=consul.ololocal.net:8500 -e OLO_LOCAL_DEV -e LOCALSTACK_HOSTNAME mes-workerservice:localdev
// docker run -e AWS_REGION=us-east-1 -e CONSUL_HTTP_ADDR=consul.ololocal.net:8500 mes-workerservice:localdev
// cd C:\code\platform
// docker compose -f Olo.Menus.Export/docker-compose.yml up -d
// cd C:\code\configuration\LocalDev\modules\LocalDev-Services\optional
// docker compose -f menu-export-service/docker-compose.yml up -d
// docker compose -f menu-export-service/docker-compose.yml down --rmi all --volumes --remove-orphans
// docker stop localstack-kafkaui-1
// docker build -t imagexxx:localdev -f ./docker/e2e-tests/Imgix.Dockerfile .
// docker run --name imagexxx -e ASPNETCORE_URLS=http://+:4222 -e AWS_REGION=us-east-1 -e CONSUL_HTTP_ADDR=consul.ololocal.net:8500 -e OLO_LOCAL_DEV -e LOCALSTACK_HOSTNAME -p 4222:4222 imagexxx:localdev
// docker compose -f image-api/docker-compose.yml up -d
// docker compose -f image-api/docker-compose.yml down

// docker system prune -af --volumes
// docker builder prune -af
// CLEANUP DOCKER: WizTree to show what is taking space on disk,
// PRESS on Docker Desktop Question Mark(?) then "Clean / Purge data" then choose WSL 2
// npm cache clean --force
// yarn cache clean --all --mirror
// nuget locals all -clear
// dotnet nuget locals --clear all

// SystemPropertiesAdvanced - windows swap files settings
#endregion

public class OloProfile : IProfile
{
    private BatFileExecutor batFileExecutor = new BatFileExecutor();
    private PowerShellExecutor powerShellExecutor = new PowerShellExecutor();
    private ApplicationStarterUtility starter = new ApplicationStarterUtility();

    public Dictionary<string, string> GetRepoMappings()
    {
        return new()
            {
                { "utils", @"C:\_PetProjects\OUtilities\Startup" },
                { "platform", @"C:\code\platform" },
                { "mapi", @"C:\code\menu-api" },
                { "imageapi", @"C:\code\image-api" },
                { "oct", @"C:\code\octopus-configurations" },
                { "tss", @"C:\code\terraform-state-staging-environment" },
                { "tsl", @"C:\code\terraform-state-live-environment" },
                { "tecr", @"C:\code\terraform-state-ecr" },
                { "null", @"C:\code\terraform-null-service-info" },
                { "config", @"C:\code\configuration" },
            };
    }

    public Dictionary<string, string> GetVsSolutionMappings()
    {
        return new()
            {
                { "utils", @"C:\_PetProjects\OUtilities\Startup\src\Quick.Startup.Utilities\Quick.Startup.Utilities.sln" },
                { "serve", @"C:\code\platform\MobileWeb\MobileWeb.sln" },
                { "admin", @"C:\code\platform\Admin\Admin.sln" },
                { "menu", @"C:\code\platform\Olo.Menus\Olo.Menus.sln" },
                { "menulogic", @"C:\code\platform\MenuLogic\Mobo.MenuLogic.sln" },
                { "menuadmin", @"C:\code\platform\MenuAdmin\MenuAdmin.sln" },
                { "dashboard", @"C:\code\platform\Dashboard\Dashboard.sln" },
                { "orderingapi", @"C:\code\platform\PlatformAPI\1.1\PlatformApiV1.1.sln" },
                { "reportService", @"C:\code\platform\ReportService\ReportService.sln" },
                { "mobologic", @"C:\code\platform\MoboLogic\MoboLogic.sln" },
                { "cloud86", @"C:\code\platform\CloudEightySixingService\CloudEightySixingService.sln" },
                { "mi86", @"C:\code\platform\MenuIntegration\MenuIntegration.sln" },
                { "mes", @"C:\code\platform\Olo.Menus.Export\Olo.Menus.Export.sln" },
                { "mis", @"C:\code\platform\Olo.Menus.Import\Olo.Menus.Import.sln" },
                { "elastic", @"C:\code\platform\MenuSearchIndexingService\MenuSearchIndexingService.sln" },
                { "omnivoreApi", @"C:\code\omnivore-api-clients\Olo.Omnivore.ApiClients.sln" },
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
                { "tecr", @"C:\code\terraform-state-ecr" },
                { "null", @"C:\code\terraform-null-service-info" },
                { "mes", @"C:\code\platform\Olo.Menus.Export" },
                { "config", @"C:\code\configuration" },
                { "imageapi", @"C:\code\image-api" },
                { "platform", @"C:\code\platform" },
                { "reportService", @"C:\code\platform\ReportService" },
            };
    }

    public async Task Startup()
    {
        await OpenPrograms();
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo ff; Start-Sleep -Seconds 5", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo db sync; Start-Sleep -Seconds 5", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo db sync --databases ImageApi; Start-Sleep -Seconds 5", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo build Admin; Start-Sleep -Seconds 5", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo build Dashboard; Start-Sleep -Seconds 5", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo build MenuAdmin; Start-Sleep -Seconds 5", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        await OpenIDEs();
        powerShellExecutor.RunPowerShellCommandAsAdmin("start chrome https://olo.login.duosecurity.com/central/", PowerShellMode.CloseInTheEnd);
        //await OloStart();
        //starter.StartImageApi();
        //starter.StartMES(startDockerPostgresDatabase: true);
    }

    private async Task OpenPrograms()
    {
        batFileExecutor.Run("Chrome.bat");
        batFileExecutor.Run("Telegram.bat");
        batFileExecutor.Run("Zoom.bat");
        batFileExecutor.Run("NotepadPlusPlus.bat");
        batFileExecutor.Run("Postman.bat");
        batFileExecutor.Run("PowerShell.bat", runAsAdmin: true);
        batFileExecutor.Run("Gemini.bat");
        await Task.Delay(10 * 1000); // Wait some time to let commands finish
    }

    private async Task OpenIDEs()
    {
        powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb platform", PowerShellMode.CloseInTheEnd);
        powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc platform", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs serve", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs elastic", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs olomenus", PowerShellMode.CloseInTheEnd);
        powerShellExecutor.RunPowerShellCommandAsAdmin("o vs dashboard", PowerShellMode.CloseInTheEnd);
        powerShellExecutor.RunPowerShellCommandAsAdmin("o vs orderingapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs reportService", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc reportService", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs mes", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc mes", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs cloud86", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs mi86", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs omnivoreApi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb mapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs mapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb imageapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vs imageapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc imageapi", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb oct", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc oct", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb tss", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc tss", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb tsl", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc tsl", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o gitb config", PowerShellMode.CloseInTheEnd);
        //powerShellExecutor.RunPowerShellCommandAsAdmin("o vsc config", PowerShellMode.CloseInTheEnd);

        await Task.Delay(30 * 1000); // Wait some time to let commands finish
    }

    private async Task OloStart()
    {
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo stop; Start-Sleep -Seconds 10", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("docker system prune -f; Start-Sleep -Seconds 10", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("olo start; Start-Sleep -Seconds 10", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("docker stop localstack-kafkaui-1; Start-Sleep -Seconds 10", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
        powerShellExecutor.RunPowerShellCommandAsAdmin("docker volume prune -f; Start-Sleep -Seconds 10", PowerShellMode.CloseInTheEnd, ProcessWindowStyle.Maximized);
    }
}
