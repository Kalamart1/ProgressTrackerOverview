using MelonLoader;
using RumbleModdingAPI;

namespace ProgressTrackerOverview
{
    public static class BuildInfo
    {
        // The use of this class should be obvious
        public const string ModName = "ProgressTrackerOverview";
        public const string ModVersion = "1.0.0";
        public const string Description = "Adds an overview of your total GC/BP progress on the gear market's progress tracker";
        public const string Author = "Kalamart";
        public const string Company = "";
    }

    public class MyMod : MelonMod
    {
        public static void Log(string msg)
        {
            MelonLogger.Msg(msg);
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
        }

        public override void OnFixedUpdate()
        {
        }
    }
}
