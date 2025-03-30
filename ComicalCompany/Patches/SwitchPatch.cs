using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class SwitchPatch
    {
        // switches the functionality of the light switch and the magnet lock switch
        static bool isSwappedCall = false;

        // Patch for StartOfRound.SetMagnetOn
        [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.SetMagnetOn))]
        [HarmonyPrefix]
        public static void SetMagnetOn_Prefix(bool on, ref bool __runOriginal)
        {
            if (!isSwappedCall)
            {
                isSwappedCall = true;

                ShipLights shipLights = UnityEngine.Object.FindObjectOfType<ShipLights>();
                shipLights.SetShipLightsBoolean(on);
                isSwappedCall = false;
                __runOriginal = false;
            }
            else
            {
                __runOriginal = true;
            }
        }

        [HarmonyPatch(typeof(ShipLights), nameof(ShipLights.ToggleShipLights))]
        [HarmonyPrefix]
        public static void ToggleShipLights(ref bool __runOriginal)
        {
            if (!isSwappedCall)
            {
                isSwappedCall = true;
                StartOfRound.Instance.SetMagnetOn(!StartOfRound.Instance.magnetOn);
                isSwappedCall = false;
                __runOriginal = false;
            }
            else
            {
                __runOriginal = true;
            }
        }

    }
}
