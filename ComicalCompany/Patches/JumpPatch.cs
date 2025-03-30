using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class JumpPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerControllerB), "Jump_performed")]
        public static void Jump_performed(InputAction.CallbackContext context, ref bool __runOriginal)
        {
            // 2% chance to not jump
            if (UnityEngine.Random.Range(0, 100) < 2)
            {
                ComicalCompany.Logger.LogInfo("Cancelled jumping!");
                __runOriginal = false;

            }
        }
    }
}
