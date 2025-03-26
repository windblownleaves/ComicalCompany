using HarmonyLib;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class TZPPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TetraChemicalItem), "Update")]
        public static void Update(TetraChemicalItem __instance)
        {
            if (__instance.emittingGas)
            {
                __instance.previousPlayerHeldBy.drunknessInertia = 3f;
                __instance.previousPlayerHeldBy.drunkness = 1f;
            }
        }
    }
}
