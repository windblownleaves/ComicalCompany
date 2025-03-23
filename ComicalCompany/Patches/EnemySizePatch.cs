using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class EnemySizePatch
    {
        [HarmonyPatch(typeof(EnemyAI), "Start")]
        [HarmonyPostfix]
        public static void Awake(EnemyAI __instance)
        {
            if (__instance.GetType() == typeof(ForestGiantAI))
            {
                __instance.transform.localScale = __instance.transform.localScale / 4;
            }
            else if (__instance.GetType() == typeof(SandSpiderAI))
            {
                __instance.transform.localScale = __instance.transform.localScale / 3;
            }
        }
    }
}
