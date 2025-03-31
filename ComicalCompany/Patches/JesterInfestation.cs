using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class JesterInfestation
    {
        // Untested, doesn't break but may not work
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(RoundManager), "RefreshEnemiesList")]
        public static IEnumerable<CodeInstruction> PatchRefreshEnemiesList(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldstr && instruction.operand is string str && str == "Nutcracker")
                {
                    // log the instruction
                    ComicalCompany.Logger.LogInfo($"Found instruction: {instruction}");

                    yield return new CodeInstruction(OpCodes.Ldstr, "Jester");
                }
                else
                {
                    yield return instruction;
                }
            }
        }
        


        
    }
}
