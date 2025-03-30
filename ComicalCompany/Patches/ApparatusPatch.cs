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

        [HarmonyPatch(typeof(GrabbableObject), "OnHitGround")]
        [HarmonyPostfix]
        public static void OnHitGround(GrabbableObject __instance)
        {

            ComicalCompany.Logger.LogInfo("item discarded");
            bool isInShipRoom = __instance.isInShipRoom;
            if (__instance.__getTypeName() == "LungProp")
            {
                if (UnityEngine.Random.value < 0.5f)
                {
                    ComicalCompany.Logger.LogInfo("Triggering explosion");

                    Utils.ComicalNetworking.Instance?.SpawnEasterEggExplosionServerRpc(__instance.transform.position);

                   Utils.Utils.DestroyGameObject(__instance.gameObject);

                }
            }
        }
    }
}
