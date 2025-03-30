using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class NetworkingPatch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(GameNetworkManager), nameof(GameNetworkManager.Start))]
        public static void Init()
        {
            if (networkPrefab != null)
                return;

            networkPrefab = (GameObject)ComicalCompany.assetBundle.LoadAsset("assets/LethalCompany/Custom/ComicalNetworkHandler.prefab");
            networkPrefab.AddComponent<Utils.ComicalNetworking>();

            NetworkManager.Singleton.AddNetworkPrefab(networkPrefab);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Awake))]
        static void SpawnNetworkHandler()
        {
            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            {
                var networkHandlerHost = UnityEngine.Object.Instantiate(networkPrefab, Vector3.zero, Quaternion.identity);
                networkHandlerHost.GetComponent<NetworkObject>().Spawn();
                //log 
                ComicalCompany.Logger.LogInfo("Network handler spawned!");
            }
        }

        static GameObject? networkPrefab;
    }
}
