using HarmonyLib;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class ApparatusPatch
    {

        [HarmonyPatch(typeof(GrabbableObject), "OnHitGround")]
        [HarmonyPostfix]
        public static void OnHitGround(GrabbableObject __instance)
        {
            if (StartOfRound.Instance.inShipPhase)
            {
                return;
            }
            if (StartOfRound.Instance.IsHost || StartOfRound.Instance.IsServer)
            {
                if (__instance.__getTypeName() == "LungProp")
                {
                    if (UnityEngine.Random.value < 0.1f)
                    {
                        Utils.ComicalNetworking.Instance?.SpawnEasterEggExplosionServerRpc(__instance.transform.position);
                        Utils.Utils.DestroyGameObject(__instance.gameObject);
                    }
                }
            }
        }
    }
}
