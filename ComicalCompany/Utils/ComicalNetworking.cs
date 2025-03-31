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
            ComicalCompany.Logger.LogInfo("Charging item server");
            ChargeItemClientRPC();
        }

        [ClientRpc]
        public void ChargeItemClientRPC()
        {
            ComicalCompany.Logger.LogInfo("Charging item");
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
            ComicalCompany.Logger.LogInfo("Inside SpawnEasterEggExplosionClientRpc");
            GameObject egg = StartOfRound.Instance.allItemsList.itemsList.Find(item => item.itemName.ToLower().Contains("easter")).spawnPrefab;
            StunGrenadeItem eggInstance = Instantiate(egg, position, Quaternion.identity).GetComponent<StunGrenadeItem>();
            eggInstance.explodeOnThrow = true;
            eggInstance.chanceToExplode = 100f;
            eggInstance.DestroyGrenade = false;
            eggInstance.ExplodeStunGrenade();
            Utils.DestroyGameObject(eggInstance.gameObject);
        }
    }
}
