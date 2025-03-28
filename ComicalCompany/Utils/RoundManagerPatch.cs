using HarmonyLib;

namespace ComicalCompany.Utils
{
    [HarmonyPatch]
    public class RoundManagerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoundManager), "Start")]
        public static void Postfix(RoundManager __instance)
        {
            ComicalCompany.Logger.LogInfo("Hijacked RoundManager with malicious intent.");

            if (__instance.GetComponent<ComicalNetworking>() == null)
            {
                __instance.gameObject.AddComponent<ComicalNetworking>();
            }
        }
    }
}
