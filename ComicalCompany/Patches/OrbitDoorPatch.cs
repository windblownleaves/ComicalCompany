using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class OrbitDoorPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StartOfRound), "ShipHasLeft")]
        public static void ShipHasLeft()
        {
            ComicalCompany.Logger.LogInfo("Doors unlocked");
            UnityEngine.Object.FindObjectOfType<HangarShipDoor>().SetDoorButtonsEnabled(true);
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(HangarShipDoor), "SetDoorOpen")]
        public static void SetDoorOpen(ref bool __runOriginal)
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
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StartOfRound), "playersFiredGameOver")]
        public static void playersFiredGameOver(ref IEnumerator __result, bool abridgedVersion)
        {
            if (StartOfRound.Instance.inShipPhase && abridgedVersion)
            {
                __result = abridgedPlayersFiredGameOver(__result);


            }
        }

        public static IEnumerator abridgedPlayersFiredGameOver(IEnumerator originalEnumerator)
        {
            for (int i = 0; i < 3; i++)
            {
                originalEnumerator.MoveNext();
            }
            while (originalEnumerator.MoveNext())
            {
                yield return originalEnumerator.Current;
            }

            HangarShipDoor hangarDoor = UnityEngine.Object.FindObjectOfType<HangarShipDoor>();
            if (hangarDoor != null)
            {
                hangarDoor.SetDoorClosed();
                hangarDoor.PlayDoorAnimation(true);
                ComicalCompany.Logger.LogInfo("Doors closed after reset.");
            }
        }



    }
}
