using BepInEx;
using BepInEx.Logging;
using ComicalCompany.Configuration;
using ComicalCompany.Patches;
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

            //Harmony.PatchAll();

            if (BoundConfig.enableApparatusPatch.Value)
                Harmony.CreateClassProcessor(typeof(ApparatusPatch)).Patch();

            if (BoundConfig.enableBoomBoxPatch.Value)
                Harmony.CreateClassProcessor(typeof(BoomBoxPatch)).Patch();

            if (BoundConfig.enableCoilheadPatch.Value)
                Harmony.CreateClassProcessor(typeof(CoilheadPatch)).Patch();

            if (BoundConfig.enableEnemySizePatch.Value)
                Harmony.CreateClassProcessor(typeof(EnemySizePatch)).Patch();

            if (BoundConfig.enableFallDamagePatch.Value)
                Harmony.CreateClassProcessor(typeof(FallDamagePatch)).Patch();

            if (BoundConfig.enableGovernmentPropertyPatch.Value)
                Harmony.CreateClassProcessor(typeof(GovernmentPropertyPatch)).Patch();

            if (BoundConfig.enableGreenModePatch.Value)
                Harmony.CreateClassProcessor(typeof(GreenModePatch)).Patch();

            if (BoundConfig.enableItemChargerPatch.Value)
                Harmony.CreateClassProcessor(typeof(ItemChargerPatch)).Patch();

            if (BoundConfig.enableItemNamePatch.Value)
                Harmony.CreateClassProcessor(typeof(ItemNamePatch)).Patch();

            if (BoundConfig.enableJesterInfestation.Value)
                Harmony.CreateClassProcessor(typeof(JesterInfestation)).Patch();

            if (BoundConfig.enableJumpPatch.Value)
                Harmony.CreateClassProcessor(typeof(JumpPatch)).Patch();

            if (BoundConfig.enableLadderPatch.Value)
                Harmony.CreateClassProcessor(typeof(LadderPatch)).Patch();

            if (BoundConfig.enableLandminePatch.Value)
                Harmony.CreateClassProcessor(typeof(LandminePatch)).Patch();

            if (BoundConfig.enableOrbitDoorPatch.Value)
                Harmony.CreateClassProcessor(typeof(OrbitDoorPatch)).Patch();

            if (BoundConfig.enableQuicksandPatch.Value)
                Harmony.CreateClassProcessor(typeof(QuicksandPatch)).Patch();

            if (BoundConfig.enableTZPPatch.Value)
                Harmony.CreateClassProcessor(typeof(TZPPatch)).Patch();

            if (BoundConfig.enableVentPatch.Value)
                Harmony.CreateClassProcessor(typeof(VentPatch)).Patch();

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