using Common.Utils;

namespace BlackScreen;

public class BlackScreen
{
    public void Run()
    {
        var batFileExecutor = new BatFileExecutor();
        batFileExecutor.Run("BlackScreen.bat", "BlackScreen");
    }
}