using GameNetcodeStuff;
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
    public class TeleporterPatch
    {
        public static PlayerControllerB? beamingPlayer;

        // Has a chance to teleport a mimic in place of the player
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ShipTeleporter), nameof(ShipTeleporter.beamUpPlayer))]
        public static void beamUpPlayer(ShipTeleporter __instance, ref bool __runOriginal, ref IEnumerator __result)
        {
            __runOriginal = true;

            // some issues with this
            ComicalCompany.Logger.LogInfo("Beaming up player");
            if (StartOfRound.Instance.shipIsLeaving)
            {
                return;
            }
            if (UnityEngine.Random.value < 0.08f)
            {
                ComicalCompany.Logger.LogInfo("Beaming up mimic");
                __result = teleportMasked(__instance);
                __runOriginal = false;
                return;
            }
        }

        public static IEnumerator teleportMasked(ShipTeleporter __instance)
        {
            __instance.shipTeleporterAudio.PlayOneShot(__instance.teleporterSpinSFX);
            Vector3 position = RoundManager.Instance.insideAINodes[UnityEngine.Random.Range(0, RoundManager.Instance.insideAINodes.Length)].transform.position;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Utils.Utils.allEnemyTypes.Find(x => x.enemyName.ToLower().Contains("mask")).enemyPrefab, position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            gameObject.GetComponentInChildren<NetworkObject>().Spawn(true);
            RoundManager.Instance.SpawnedEnemies.Add(gameObject.GetComponent<EnemyAI>());
            gameObject.GetComponent<EnemyAI>().ShipTeleportEnemy();
            yield return new WaitForSeconds(3f);
            __instance.shipTeleporterAudio.PlayOneShot(__instance.teleporterBeamUpSFX);
            if (GameNetworkManager.Instance.localPlayerController.isInHangarShipRoom)
            {
                HUDManager.Instance.ShakeCamera(ScreenShakeType.Big);
            }
            yield break;
        }
    }
}
