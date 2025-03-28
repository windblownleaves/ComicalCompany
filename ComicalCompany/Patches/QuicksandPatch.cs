using HarmonyLib;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    internal class QuicksandPatch
    {
        static bool patched = false;

        [HarmonyPatch(typeof(QuicksandTrigger), "OnTriggerStay")]
        [HarmonyPostfix]
        public static void OnTriggerStayPostfix(QuicksandTrigger __instance)
        {
            if (!patched)
            {
                __instance.sinkingSpeedMultiplier = 1.75f;
                patched = true;
            }
        }
    }
}
