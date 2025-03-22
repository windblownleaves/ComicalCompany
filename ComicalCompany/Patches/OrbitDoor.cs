using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ComicalCompany.Patches
{
    public class OrbitDoor
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StartOfRound), "ShipHasLeft")]
        public static void ShipHasLeft()
        {
            UnityEngine.Object.FindObjectOfType<HangarShipDoor>().SetDoorButtonsEnabled(true);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(HangarShipDoor), "SetDoorButtonsEnabled")]
        public static void SetDoorButtonsEnabled(ref bool doorButtonsEnabled)
        {
            doorButtonsEnabled = true;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(HangarShipDoor), "SetDoorOpen")]
        public static void SetDoorOpen()
        {
            if (StartOfRound.Instance.inShipPhase)
            {
                StartOfRound.Instance.FirePlayersAfterDeadlineClientRpc(new int[] {
                    StartOfRound.Instance.gameStats.daysSpent,
                    StartOfRound.Instance.gameStats.scrapValueCollected,
                    StartOfRound.Instance.gameStats.deaths,
                    StartOfRound.Instance.gameStats.allStepsTaken
                }, true);
            }
        }
        
    }
}
