using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class VentPatch
    {
        static Vector3 newPosition;
        static bool hasRun = false;

        [HarmonyPatch(typeof(StartOfRound), "StartGame")]
        [HarmonyPostfix]
        private static void GameLoadPostfix()
        {
            if (hasRun)
            {
                return;
            }
            hasRun = true;

            GameObject vanillaVent = GameObject.Find("VentEntrance");

            if (vanillaVent == null)
            {
                ComicalCompany.Logger.LogError("Can't find ship vent");
                return;
            }

            GameObject? ventInstance = Object.Instantiate(ComicalCompany.ventPrefab);

            NetworkObject netObj = ventInstance.GetComponent<NetworkObject>();
            if (netObj != null && !netObj.IsSpawned)
            {
                netObj.Spawn();
            }

            ventInstance.transform.position = vanillaVent.transform.position + new Vector3(0, 0, 0.05f);
            ventInstance.transform.rotation = vanillaVent.transform.rotation;
            ventInstance.name = vanillaVent.name;
            ventInstance.transform.parent = vanillaVent.transform.parent;
            GameObject.Destroy(vanillaVent);
        }

        [HarmonyPatch(typeof(RoundManager), "DespawnPropsAtEndOfRound")]
        [HarmonyPrefix]
        private static void DespawnPropsPrefix()
        {
            GameObject vent = GameObject.Find("VentEntrance");

            vent.GetComponent<AudioSource>()?.Stop();
            vent.GetComponent<Animator>().Play("New State");
            vent.GetComponent<EnemyVent>().ventIsOpen = false;
        }
    }
}
