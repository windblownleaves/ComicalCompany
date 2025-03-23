using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class CoilheadPatch
    {
        public static float randomTimer = 0;
        public static float randomval = 1f;
        public static MethodInfo checkRandomMethod = AccessTools.Method(typeof(CoilheadPatch), "CheckPerlin");

        public static bool CheckRandom()
        {
            randomTimer += Time.deltaTime;
            
            if (randomTimer > 2)
            {
                randomTimer = 0;
                randomval = Random.Range(0f, 1f);
            }
            if (randomval < 0.1f)
            {
                return false;
            }
            return true;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(SpringManAI), "Update")]

        public static IEnumerable<CodeInstruction> Patch_Update(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {

            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
            int stloc3flag = 0;
            Label skipFlagLabel = il.DefineLabel();
            for (int i = 0; i < codes.Count; i++)
            {                
                if (codes[i].opcode == OpCodes.Stloc_3)
                {
                    stloc3flag++;
                    if (stloc3flag == 1)
                    {

                        codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, checkRandomMethod));
                        codes.Insert(i + 2, new CodeInstruction(OpCodes.Brfalse_S, skipFlagLabel));

                    }
                    else if (stloc3flag == 3)
                    {
                        codes[i-5].labels.Add(skipFlagLabel);
                        break;
                    }

                }
            }
            return codes;
        }
        
    }
}
