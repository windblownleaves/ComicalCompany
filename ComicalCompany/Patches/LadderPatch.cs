using HarmonyLib;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    public class LadderPatch
    {
        [HarmonyPatch(nameof(StartOfRound.StartGame))]
        [HarmonyPostfix]
        private static void GameLoadPostfix()
        {
            GameObject longLadder = ComicalCompany.assetBundle.LoadAsset<GameObject>("assets/LethalCompany/Custom/newladder.prefab");

            GameObject ladder0 = GameObject.Find("LadderShort");
            GameObject ladder1 = GameObject.Find("LadderShort (1)");

            if (ladder0 == null || ladder1 == null)
            {
                ComicalCompany.Logger.LogError("Can't find ship ladder(s)");
                return;
            }

            GameObject longLadder0 = GameObject.Instantiate(longLadder);
            GameObject longLadder1 = GameObject.Instantiate(longLadder);

            longLadder0.transform.position = new Vector3(ladder0.transform.position.x, -13.0f, ladder0.transform.position.z);
            longLadder0.transform.rotation = ladder0.transform.rotation;
            longLadder0.name = ladder0.name;
            longLadder0.transform.parent = ladder0.transform.parent;
            GameObject.Destroy(ladder0);

            longLadder1.transform.position = new Vector3(ladder1.transform.position.x, -13.0f, ladder1.transform.position.z);
            longLadder1.transform.rotation = ladder1.transform.rotation;
            longLadder1.name = ladder1.name;
            longLadder1.transform.parent = ladder1.transform.parent;
            GameObject.Destroy(ladder1);
        }
    }
}
