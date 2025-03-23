using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class ItemChargerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ItemCharger), "Update")]
        public static void Update(ItemCharger __instance, ref bool __runOriginal)
        {
            __runOriginal = false;
            if (NetworkManager.Singleton == null)
            {
                return;
            }
            if (__instance.updateInterval > 1f)
            {
                __instance.updateInterval = 0f;
                if (GameNetworkManager.Instance != null && GameNetworkManager.Instance.localPlayerController != null)
                {
                    __instance.triggerScript.interactable = (GameNetworkManager.Instance.localPlayerController.currentlyHeldObjectServer == null || GameNetworkManager.Instance.localPlayerController.currentlyHeldObjectServer.itemProperties.isConductiveMetal || GameNetworkManager.Instance.localPlayerController.currentlyHeldObjectServer.itemProperties.requiresBattery);
                }
            }
            else
            {
                __instance.updateInterval += Time.deltaTime;
            }
        }   

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ItemCharger), "ChargeItem")]
        public static void ChargeItem(ref ItemCharger __instance, ref bool __runOriginal)
        {
            GrabbableObject currentlyHeldObjectServer = GameNetworkManager.Instance.localPlayerController.currentlyHeldObjectServer;
            if (currentlyHeldObjectServer == null || (currentlyHeldObjectServer.itemProperties.isConductiveMetal && !currentlyHeldObjectServer.itemProperties.requiresBattery))
            {
                __runOriginal = false;
                __instance.StartCoroutine(explosionRoutine(__instance));

            }
            else
            {
                __runOriginal = true;
                return;
            }
        }
        public static IEnumerator explosionRoutine(ItemCharger __instance)
        {
            // run the charge item coroutine to get the zap effect
            __instance.zapAudio.Play();
            yield return new WaitForSeconds(0.75f);
            __instance.chargeStationAnimator.SetTrigger("zap");
            Landmine.SpawnExplosion(__instance.transform.position, spawnExplosionEffect: true, 1f, 6f, physicsForce:10f);
        }
    }

    
}
