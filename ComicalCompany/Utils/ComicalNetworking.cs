using ComicalCompany.Patches;
using Unity.Netcode;
using UnityEngine;

namespace ComicalCompany.Utils
{
    public class ComicalNetworking : NetworkBehaviour
    {
        public static ComicalNetworking? Instance;


        public override void OnNetworkSpawn()
        {
            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
                Instance?.gameObject.GetComponent<NetworkObject>().Despawn();
            Instance = this;
            ComicalCompany.Logger.LogInfo("ComicalNetworking awakened!");


            base.OnNetworkSpawn();
        }

        [ServerRpc(RequireOwnership = false)]
        public void SpawnLandmineServerRpc(Vector3 position, bool spawnExplosionEffect, float killRange, float damageRange, int nonLethalDamage, float physicsForce)
        {
            SpawnLandmineClientRpc(position, spawnExplosionEffect, killRange, damageRange, nonLethalDamage, physicsForce);
        }

        [ClientRpc]
        public void SpawnLandmineClientRpc(Vector3 position, bool spawnExplosionEffect, float killRange, float damageRange, int nonLethalDamage, float physicsForce)
        {
            Landmine.SpawnExplosion(position, spawnExplosionEffect, killRange, damageRange, nonLethalDamage, physicsForce);
        }

        [ServerRpc(RequireOwnership = false)]
        public void ChargeItemServerRPC()
        {
            ChargeItemClientRPC();
        }

        [ClientRpc]
        public void ChargeItemClientRPC()
        {
            ItemCharger itemCharger = GameObject.FindObjectOfType<ItemCharger>();
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
            GameObject? egg = StartOfRound.Instance.allItemsList.itemsList.Find(item => item.itemName.ToLower().Contains("easter") || item.itemName.ToLower().Contains("spheroid"))?.spawnPrefab;
            if (egg == null)
            {
                ComicalCompany.Logger.LogWarning("Egg not found in allItemsList!");
                return;
            }
            StunGrenadeItem eggInstance = Instantiate(egg, position, Quaternion.identity).GetComponent<StunGrenadeItem>();
            eggInstance.explodeOnThrow = true;
            eggInstance.explodeOnCollision = true;
            eggInstance.chanceToExplode = 100f;
            eggInstance.DestroyGrenade = true;
            eggInstance.ExplodeStunGrenade();
        }
    }
}
