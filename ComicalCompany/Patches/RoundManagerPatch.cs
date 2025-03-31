using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class RoundManagerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoundManager), "FinishGeneratingLevel")]
        public static void FinishGeneratingLevel(RoundManager __instance)
        {
            CoilheadPatch.random = new Random(StartOfRound.Instance.randomMapSeed + 1001);
            TeleporterPatch.random = new Random(StartOfRound.Instance.randomMapSeed + 3002);
        }
    }
}
