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
                ComicalCompany.Logger.LogInfo("Patched quicksand.");

                __instance.sinkingSpeedMultiplier = 2.0f;
            }
        }
    }
}
