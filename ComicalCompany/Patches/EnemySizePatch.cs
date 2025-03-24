using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class EnemySizePatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(EnemyAI), "Start")]
        public static void Awake(EnemyAI __instance)
        {
            if (__instance.GetType() == typeof(ForestGiantAI))
            {
                __instance.transform.localScale = __instance.transform.localScale / 4;
                __instance.gameObject.name = "Forest";
                __instance.gameObject.GetComponent<ScanNodeProperties>().headerText = "Forest";
            }
            else if (__instance.GetType() == typeof(SandSpiderAI))
            {
                __instance.transform.localScale = __instance.transform.localScale / 3;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SandSpiderWebTrap), "Awake")]
        public static void WebAwake(SandSpiderWebTrap __instance)
        {
            __instance.transform.localScale = __instance.transform.localScale * 2;
        }
    }
}
