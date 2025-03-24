using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class JesterInfestation
    {
        // WIP
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(RoundManager), "RefreshEnemiesList")]
        public static IEnumerable<CodeInstruction> PatchRefreshEnemiesList(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                    if (instruction.opcode == OpCodes.Ldstr && instruction.operand is string str && str == "Nutcracker")
                {
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
