using HarmonyLib;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class BlobPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(EnemyAI), "Update")]
        public static void ChangeSpeed(EnemyAI __instance)
        {
            if (__instance.GetType() == typeof(BlobAI))
            {
                __instance.agent.speed = 3f;
            }
        }
    }
}
