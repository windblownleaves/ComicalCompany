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
        public static BoomboxItem boomBoxItem = AllItemsList.FindAnyObjectByType(typeof(BoomboxItem)) as BoomboxItem;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BoomboxItem), "ItemActivate")]
        public static void ItemActivate(BoomboxItem __instance)
        {
            // Explode the boombox if its name is "Boom Box"
            if (__instance.gameObject.name == "Boom Box")
            {
                ComicalCompany.Logger.LogInfo("Boom Box exploded!");
                Landmine.SpawnExplosion(__instance.gameObject.transform.position, true, 1, 4f, 80, 20f);
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
                    RandomScrapSpawn[] source = UnityEngine.Object.FindObjectsOfType<RandomScrapSpawn>();
                    Vector3 position = source[UnityEngine.Random.RandomRangeInt(0, source.Length)].gameObject.transform.position;
                    GameObject obj = UnityEngine.Object.Instantiate(boomBoxItem.gameObject, position, Quaternion.identity, __instance.playersManager.propsContainer);
                    obj.GetComponent<GrabbableObject>().fallTime = 0f;
                    obj.name = "Boom Box";
                    obj.GetComponent<ScanNodeProperties>().name = "Boom Box";
                    // sort this out, probably doesnt work
                    if (obj.GetComponent<GrabbableObject>().GetType() == typeof(BoomboxItem))
                    {
                        ((BoomboxItem)obj.GetComponent<GrabbableObject>()).StartMusic(true, false);
                    }
                    obj.GetComponent<NetworkObject>().Spawn();
                }
            }
            
        }

    }
}
