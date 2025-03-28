using ComicalCompany.Patches;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Utils
{
    public class Networking : NetworkBehaviour
    {
        [ServerRpc(RequireOwnership = false)]
        public static void SpawnLandmineServerRpc(Vector3 position, bool spawnExplosionEffect, float killRange, float damageRange, int nonLethalDamage, float physicsForce, GameObject? overridePrefab = null, bool goThroughCar = false)
        {
            SpawnLandmineClientRpc(position, spawnExplosionEffect, killRange, damageRange, nonLethalDamage, physicsForce, overridePrefab, goThroughCar);
        }

        [ClientRpc]
        public static void SpawnLandmineClientRpc(Vector3 position, bool spawnExplosionEffect, float killRange, float damageRange, int nonLethalDamage, float physicsForce, GameObject? overridePrefab, bool goThroughCar)
        {
            Landmine.SpawnExplosion(position, spawnExplosionEffect, killRange, damageRange, nonLethalDamage, physicsForce, overridePrefab, goThroughCar);
        }

        [ServerRpc(RequireOwnership = false)]
        public static void ChargeItemServerRPC(ItemCharger itemCharger)
        {
            ChargeItemClientRPC(itemCharger);
        }

        [ClientRpc]
        public static void ChargeItemClientRPC(ItemCharger itemCharger)
        {
            itemCharger.StartCoroutine(ItemChargerPatch.explosionRoutine(itemCharger));
        }

        [ServerRpc(RequireOwnership = false)]
        public static void SpawnEasterEggExplosionServerRpc(GameObject egg, Vector3 position)
        {
            SpawnEasterEggExplosionClientRpc(egg, position);
        }

        [ClientRpc]
        public static void SpawnEasterEggExplosionClientRpc(GameObject egg, Vector3 position)
        {
            StunGrenadeItem eggInstance = (StunGrenadeItem)Instantiate(egg, position, Quaternion.identity).GetComponent<GrabbableObject>();
            eggInstance.ExplodeStunGrenade();
        }
    }
}
