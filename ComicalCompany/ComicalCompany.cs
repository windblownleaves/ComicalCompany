using BepInEx;
using BepInEx.Logging;
using ComicalCompany.Configuration;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ComicalCompany
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class ComicalCompany : BaseUnityPlugin
    {
        public static ComicalCompany Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        internal static ComicalCompanyConfig BoundConfig { get; private set; } = null!;

        public static AssetBundle assetBundle;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            BoundConfig = new ComicalCompanyConfig(Config);

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");

            string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "physicsapi"));

            if (assetBundle == null)
            {
                Logger.LogError("Failed to load custom assets."); // ManualLogSource for your plugin
                return;
            }
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();
            // Log harmony patched methods
            Logger.LogInfo("Patched methods:");
            foreach (var patch in Harmony.GetPatchedMethods())
            {
                Logger.LogInfo(patch.FullDescription());
            }

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }
}