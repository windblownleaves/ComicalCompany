using BepInEx;
using BepInEx.Logging;
using ComicalCompany.Configuration;
using ComicalCompany.Patches;
using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComicalCompany
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class ComicalCompany : BaseUnityPlugin
    {
        public static ComicalCompany Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        internal static ComicalCompanyConfig BoundConfig { get; private set; } = null!;

        public static AssetBundle? assetBundle;

        public static GameObject ventPrefab;
        private bool prefabRegistered = false;

        public static GameObject lollipopPrefab;
        public static GameObject hatPrefab;

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
            lollipopPrefab = assetBundle.LoadAsset<GameObject>("assets/LethalCompany/Custom/lollipop.prefab");
            hatPrefab = assetBundle.LoadAsset<GameObject>("assets/LethalCompany/Custom/hat.prefab");
            ventPrefab = assetBundle.LoadAsset<GameObject>("assets/LethalCompany/Custom/vent.prefab");

            SceneManager.sceneLoaded += OnSceneLoaded;

            NetcodePatcher(); // ONLY RUN ONCE //
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (prefabRegistered) return;

            if (NetworkManager.Singleton != null && ventPrefab != null)
            {
                NetworkManager.Singleton.AddNetworkPrefab(ventPrefab);
                prefabRegistered = true;
            }
        }

        private static void NetcodePatcher()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }
            }
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            if ((DateTime.UtcNow.Month == 4 && DateTime.UtcNow.Day == 1)
                || (DateTime.UtcNow.Month == 3 && DateTime.UtcNow.Day == 31)
                || (DateTime.UtcNow.Month == 4 && DateTime.UtcNow.Day == 2)
                || BoundConfig.alwaysEnableMod.Value)
            {
                // Non-Negotiable Patches
                Harmony.CreateClassProcessor(typeof(NetworkingPatch)).Patch();
                Harmony.CreateClassProcessor(typeof(StartOfRoundPatch)).Patch();
                Harmony.CreateClassProcessor(typeof(RoundManagerPatch)).Patch();

                if (BoundConfig.enableSwitchPatch.Value)
                    Harmony.CreateClassProcessor(typeof(SwitchPatch)).Patch();

                if (BoundConfig.enableTeleporterPatch.Value)
                    Harmony.CreateClassProcessor(typeof(TeleporterPatch)).Patch();

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

                if (BoundConfig.enablePropsPatch.Value)
                    Harmony.CreateClassProcessor(typeof(PropsPatch)).Patch();

                if (BoundConfig.enableInspirationPatch.Value)
                    Harmony.CreateClassProcessor(typeof(InspirationPatch)).Patch();
            }

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