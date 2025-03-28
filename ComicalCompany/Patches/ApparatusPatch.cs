using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class ApparatusPatch
    {
        // load the lethalcompany EasterEggExplosionParticlePrefab
        public static GameObject? explosionPrefab  = null;

        [HarmonyPatch(typeof(GrabbableObject), "OnHitGround")]
        [HarmonyPostfix]
        public static void OnHitGround(GrabbableObject __instance)
        {
            if (explosionPrefab == null) {
                explosionPrefab = StartOfRound.Instance.allItemsList.itemsList.Find(item => item.itemName.ToLower().Contains("egg")).spawnPrefab;
            }

            ComicalCompany.Logger.LogInfo("item discarded");
            bool isInShipRoom = __instance.isInShipRoom;
            if (__instance.__getTypeName() == "LungProp")
            {
                if (UnityEngine.Random.Range(0, 100) < 50)
                {
                    ComicalCompany.Logger.LogInfo("Triggering explosion");

                    Utils.Networking.SpawnEasterEggExplosionServerRpc(explosionPrefab, __instance.transform.position);

                    UnityEngine.Object.Destroy(__instance.gameObject);
                    MeshRenderer[] componentsInChildren = __instance.gameObject.GetComponentsInChildren<MeshRenderer>();
                    for (int i = 0; i < componentsInChildren.Length; i++)
                    {
                        UnityEngine.Object.Destroy(componentsInChildren[i]);
                    }
                    Collider[] componentsInChildren2 = __instance.gameObject.GetComponentsInChildren<Collider>();
                    for (int j = 0; j < componentsInChildren2.Length; j++)
                    {
                        UnityEngine.Object.Destroy(componentsInChildren2[j]);
                    }

            }
            }
        }
    }
}
