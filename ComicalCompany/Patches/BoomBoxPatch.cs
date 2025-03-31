using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class BoomBoxPatch
    {
        public static GameObject? boomBoxItem;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BoomboxItem), "StartMusic")]
        public static void startMusicPatch(BoomboxItem __instance)
        {
            ComicalCompany.Logger.LogInfo("[Harmony Patch] ItemActivate called");

            // Check each field and log if it's null
            if (__instance.boomboxAudio == null)
                ComicalCompany.Logger.LogInfo("[Harmony Patch] boomboxAudio is null!");

            if (__instance.musicAudios == null)
                ComicalCompany.Logger.LogInfo("[Harmony Patch] musicAudios is null!");
            else if (__instance.musicAudios.Length == 0)
                ComicalCompany.Logger.LogInfo("[Harmony Patch] musicAudios array is empty!");

            if (__instance.stopAudios == null)
                ComicalCompany.Logger.LogInfo("[Harmony Patch] stopAudios is null!");
            else if (__instance.stopAudios.Length == 0)
                ComicalCompany.Logger.LogInfo("[Harmony Patch] stopAudios array is empty!");

            if (__instance.musicRandomizer == null)
                ComicalCompany.Logger.LogInfo("[Harmony Patch] musicRandomizer is null!");

            if (__instance.musicPitchDown == null)
                ComicalCompany.Logger.LogInfo("[Harmony Patch] musicPitchDown coroutine is null!");

            ComicalCompany.Logger.LogInfo("[Harmony Patch] ItemActivate check completed.");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BoomboxItem), "ItemActivate")]
        public static void ItemActivate(BoomboxItem __instance)
        {
            // Explode the boombox if its name is "Boom Box"
            if (__instance.isHeld)
            {
                // check to see if boombox has an AudioSource

                ComicalCompany.Logger.LogInfo("Boom Box exploded!");
                Utils.ComicalNetworking.Instance?.SpawnLandmineServerRpc(__instance.gameObject.transform.position, true, 3f, 7f, 80, 20f);
                __instance.playerHeldBy?.DestroyItemInSlot(__instance.playerHeldBy.currentItemSlot);
            }
        }

        [HarmonyPriority(Priority.Low)]
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BoomboxItem), "Start")]
        public static void ItemStart(BoomboxItem __instance)
        {
            if (__instance.GetType() == typeof(BoomboxItem))
            {
                ((BoomboxItem) __instance).ItemActivate(true);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoundManager), "SpawnScrapInLevel")]
        public static void SpawnBoomBox(RoundManager __instance)
        {
            if (__instance.NetworkManager.IsServer || __instance.NetworkManager.IsHost)
            {
                if (UnityEngine.Random.Range(0f, 1f) < 1)
                {
                    if (boomBoxItem == null)
                    {
                        Item boomboxEntry = StartOfRound.Instance.allItemsList.itemsList.Find(x => x.itemName.Contains("box"));

                        if (boomboxEntry == null)
                        {
                            ComicalCompany.Logger.LogError("Boombox item not found in allItemsList!");
                            return;
                        }
                        boomBoxItem = boomboxEntry.spawnPrefab;
                    }
                    // Log stuff to check null
                    RandomScrapSpawn[] source = UnityEngine.Object.FindObjectsOfType<RandomScrapSpawn>();
                    Vector3 position = source[UnityEngine.Random.RandomRangeInt(0, source.Length)].gameObject.transform.position;

                    GameObject obj = UnityEngine.Object.Instantiate<GameObject>(boomBoxItem, position, Quaternion.identity, __instance.playersManager.propsContainer);
                    obj.GetComponent<GrabbableObject>().fallTime = 0f;
                    obj.GetComponent<NetworkObject>().Spawn(false);   
                }
            } 
        }
    }
}
