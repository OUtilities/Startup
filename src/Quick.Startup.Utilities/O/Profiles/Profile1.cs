namespace O.Profiles;

// OLO
internal class Profile1 : IProfile
{
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
}
