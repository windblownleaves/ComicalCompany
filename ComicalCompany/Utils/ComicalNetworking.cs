using ComicalCompany.Patches;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Utils
{
    public class ComicalNetworking : NetworkBehaviour
    {
        public static ComicalNetworking? Instance;

        private void Awake()
        {
            Instance = this;
            ComicalCompany.Logger.LogInfo("ComicalNetworking awakened!");
        }

        [ServerRpc(RequireOwnership = false)]
        public void SpawnLandmineServerRpc(Vector3 position, bool spawnExplosionEffect, float killRange, float damageRange, int nonLethalDamage, float physicsForce, GameObject? overridePrefab = null, bool goThroughCar = false)
        {
            SpawnLandmineClientRpc(position, spawnExplosionEffect, killRange, damageRange, nonLethalDamage, physicsForce, overridePrefab, goThroughCar);
        }

        [ClientRpc]
        public void SpawnLandmineClientRpc(Vector3 position, bool spawnExplosionEffect, float killRange, float damageRange, int nonLethalDamage, float physicsForce, GameObject? overridePrefab, bool goThroughCar)
        {
            Landmine.SpawnExplosion(position, spawnExplosionEffect, killRange, damageRange, nonLethalDamage, physicsForce, overridePrefab, goThroughCar);
        }

        [ServerRpc(RequireOwnership = false)]
        public void ChargeItemServerRPC(ItemCharger itemCharger)
        {
            ChargeItemClientRPC(itemCharger);
        }

        [ClientRpc]
        public void ChargeItemClientRPC(ItemCharger itemCharger)
        {
            itemCharger.StartCoroutine(ItemChargerPatch.explosionRoutine(itemCharger));
        }

        [ServerRpc(RequireOwnership = false)]
        public void SpawnEasterEggExplosionServerRpc(Vector3 position)
        {
            SpawnEasterEggExplosionClientRpc(position);
        }

        [ClientRpc]
        public void SpawnEasterEggExplosionClientRpc(Vector3 position)
        {
            ComicalCompany.Logger.LogInfo("Inside SpawnEasterEggExplosionClientRpc");
            GameObject egg = StartOfRound.Instance.allItemsList.itemsList.Find(item => item.itemName.ToLower().Contains("egg")).spawnPrefab;
            StunGrenadeItem eggInstance = (StunGrenadeItem)Instantiate(egg, position, Quaternion.identity).GetComponent<GrabbableObject>();
            eggInstance.ExplodeStunGrenade();
        }
    }
}
