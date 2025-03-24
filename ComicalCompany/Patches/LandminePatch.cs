using HarmonyLib;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class LandminePatch
    {
        // Patch landmines to apply a force to the player when they explode
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Landmine), "Detonate")]
        public static void Detonate(Landmine __instance, ref bool __runOriginal)
        {
            __runOriginal = false;
            __instance.mineAudio.pitch = UnityEngine.Random.Range(0.93f, 1.07f);
            __instance.mineAudio.PlayOneShot(__instance.mineDetonate, 1f);
            Landmine.SpawnExplosion(__instance.transform.position + Vector3.up, false, 0f, 10f, 30, 100f, null, false);
        }
    }
}
