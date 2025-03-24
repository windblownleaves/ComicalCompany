using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class BoomBoxPatch
    {
        // WIP
        public static GameObject? boomBoxItem;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BoomboxItem), "ItemActivate")]
        public static void ItemActivate(BoomboxItem __instance)
        {
            // Explode the boombox if its name is "Boom Box"
            if (__instance.gameObject.GetComponent<GrabbableObject>().itemProperties.automaticallySetUsingPower)
            {
                ComicalCompany.Logger.LogInfo("Boom Box exploded!");
                Landmine.SpawnExplosion(__instance.gameObject.transform.position, true, 3f, 7f, 80, 20f);
                __instance.playerHeldBy?.DestroyItemInSlot(__instance.playerHeldBy.currentItemSlot);
            }

        }

        [HarmonyPriority(Priority.Low)]
        [HarmonyPostfix]
        [HarmonyPatch(typeof(GrabbableObject), "Start")]
        public static void ItemStart(GrabbableObject __instance)
        {
            if (__instance.GetType() == typeof(BoomboxItem) && __instance.itemProperties.automaticallySetUsingPower)
            {
                __instance.itemProperties.itemName = "Boom Box";
                __instance.GetComponentInChildren<ScanNodeProperties>().headerText = "Boom Box";
                ComicalCompany.Logger.LogInfo("Boom Box renamed!");
                ComicalCompany.Logger.LogInfo(((BoomboxItem)__instance).musicAudios.Length);
                ((BoomboxItem)__instance).StartMusic(true, false);
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

                    ComicalCompany.Logger.LogInfo("Spawning Boom Box");
                    ComicalCompany.Logger.LogInfo(boomBoxItem);
                    ComicalCompany.Logger.LogInfo(position);

                    GameObject obj = UnityEngine.Object.Instantiate<GameObject>(boomBoxItem, position, Quaternion.identity, __instance.playersManager.propsContainer);
                    ComicalCompany.Logger.LogInfo(obj);
                    obj.GetComponent<GrabbableObject>().fallTime = 0f;
                    obj.GetComponent<GrabbableObject>().itemProperties.automaticallySetUsingPower = true;
                    obj.GetComponent<NetworkObject>().Spawn(false);   
                }
            }
            
        }

    }
}
