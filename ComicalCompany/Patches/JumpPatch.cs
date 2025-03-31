using GameNetcodeStuff;
using HarmonyLib;
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
            // 5% chance to not jump
            if (UnityEngine.Random.Range(0, 100) < 5)
            {
                __runOriginal = false;
            }
        }
    }
}
