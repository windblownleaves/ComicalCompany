using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class StartOfRoundPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StartOfRound), "Awake")]
        public static void Awake()
        {
            Utils.Utils.allEnemyTypes = Resources.FindObjectsOfTypeAll<EnemyType>().ToList();
        }
    }
}
