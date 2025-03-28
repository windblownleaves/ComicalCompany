using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    public class VentPatch
    {
        [HarmonyPatch(nameof(StartOfRound.StartGame))]
        [HarmonyPostfix]
        private static void GameLoadPostfix()
        {
            GameObject enemyVent = ComicalCompany.assetBundle.LoadAsset<GameObject>("assets/LethalCompany/Custom/vent.prefab");

            GameObject vanillaVent = GameObject.Find("VentEntrance");

            if (vanillaVent == null)
            {
                ComicalCompany.Logger.LogError("Can't find ship vent");
                return;
            }

            GameObject ventInstance = GameObject.Instantiate(enemyVent);

            NetworkObject netObj = ventInstance.GetComponent<NetworkObject>();
            if (netObj != null && !netObj.IsSpawned)
            {
                netObj.Spawn();
            }

            ventInstance.transform.position = vanillaVent.transform.position + new Vector3(0, 0, 1);
            ventInstance.transform.rotation = vanillaVent.transform.rotation;
            ventInstance.name = vanillaVent.name;
            ventInstance.transform.parent = vanillaVent.transform.parent;
            GameObject.Destroy(vanillaVent);

            ComicalCompany.Logger.LogInfo("Destroyed vanilla vent and replaced it with " + enemyVent);
        }
    }
}
