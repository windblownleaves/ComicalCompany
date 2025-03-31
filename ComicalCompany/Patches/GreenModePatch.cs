using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class GreenModePatch
    {
        private static float greenModePercentChance = 2f;

        [HarmonyPatch(typeof(StartOfRound), "StartGame")]
        [HarmonyPostfix]
        private static void StartGamePostfix()
        {
            if (Random.Range(0, 100) >= greenModePercentChance)
            {
                return;
            }

            GameObject mainVolumeObject = GameObject.Find("VolumeMain");
            Volume mainVolume = mainVolumeObject.GetComponent<Volume>();

            if (mainVolume == null)
            {
                ComicalCompany.Logger.LogError("Main volume not found. Skipping green mode...");
                return;
            }

            if (mainVolume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
            {
                HUDManager.Instance.DisplayTip("Green mode enabled", "Green mode enabled", false, false, "LC_Tip1");
                colorAdjustments.colorFilter.overrideState = true;
                colorAdjustments.colorFilter.value = Color.green;
            }
            else
            {
                ComicalCompany.Logger.LogWarning("ColorAdjustments not found. Skipping green mode...");
            }
        }

        [HarmonyPatch(typeof(RoundManager), "DespawnPropsAtEndOfRound")]
        [HarmonyPrefix]
        private static void DespawnPropsPrefix()
        {
            GameObject mainVolumeObject = GameObject.Find("VolumeMain");
            Volume mainVolume = mainVolumeObject.GetComponent<Volume>();

            if (mainVolume == null)
            {
                ComicalCompany.Logger.LogWarning("Main volume not found. You must remain green :(");
                return;
            }

            if (mainVolume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
            {
                colorAdjustments.colorFilter.overrideState = false;
            }
            else
            {
                ComicalCompany.Logger.LogWarning("ColorAdjustments not found. You must remain green :(");
            }
        }
    }
}
