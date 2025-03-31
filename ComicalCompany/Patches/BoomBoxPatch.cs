using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class BoomBoxPatch
    {
        public static GameObject? boomBoxItem;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BoomboxItem), "ItemActivate")]
        public static void ItemActivate(BoomboxItem __instance)
        {
            // Explode the boombox if its name is "Boom Box"
            if (__instance.isHeld)
            {
                // check to see if boombox has an AudioSource

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
