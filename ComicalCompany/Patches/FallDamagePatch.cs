using GameNetcodeStuff;
using HarmonyLib;

namespace ComicalCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class FallDamagePatch
    {
        [HarmonyPatch("PlayerHitGroundEffects")]
        [HarmonyPrefix]
        public static void BeforeHitGround(ref float ___carryWeight, ref float ___fallValueUncapped, ref float ___fallValue, ref bool ___takingFallDamage)
        {
            if (!ComicalCompany.BoundConfig.enableFallDamage.Value)
            {
                return;
            }

            ___takingFallDamage = true;

            float weightFactor = 1.0f + 0.8f * (___carryWeight - 1);

            if (___fallValueUncapped < -15f)
            {
                ___fallValueUncapped *= weightFactor;
                ___fallValue *= weightFactor;
            }
        }
    }
}
