using HarmonyLib;
using UnityEngine;

namespace ComicalCompany.Patches
{
    internal class VentPatch
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

            ventInstance.transform.position = vanillaVent.transform.position;
            ventInstance.transform.rotation = vanillaVent.transform.rotation;
            ventInstance.name = vanillaVent.name;
            ventInstance.transform.parent = vanillaVent.transform.parent;
            GameObject.Destroy(vanillaVent);
        }
    }
}
