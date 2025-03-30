using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace ComicalCompany.Patches
{
    [HarmonyPatch(typeof(HangarShipDoor))]
    public class LadderPatch
    {
        private static bool hasPatchedLadders = false;

        [HarmonyPatch(nameof(HangarShipDoor.SetDoorButtonsEnabled))]
        [HarmonyPostfix]
        private static void HangarDoorsOpenPostfix()
        {
            if (hasPatchedLadders)
            {
                return;
            }
            hasPatchedLadders = true;

            GameObject ladder0 = GameObject.Find("LadderShort");
            GameObject ladder1 = GameObject.Find("LadderShort (1)");
            GameObject[] ladders = { ladder0, ladder1 };

            GameObject ladderMesh = GameObject.Find("OutsideShipRoom/Ladder");
            ComicalCompany.Logger.LogError(ladderMesh);

            foreach (GameObject ladder in ladders)
            {
                BoxCollider collider = ladder.GetComponentInChildren<BoxCollider>();
                collider.size = new Vector3(1, 2.6f, 0.594941f);
                collider.center = new Vector3(0, -1.34f, -0.2025296f);

                for (int i = 0; i < 3; i++)
                { 
                    GameObject instance = Object.Instantiate(ladderMesh, Vector3.zero, Quaternion.identity, ladder.transform);
                    instance.transform.GetChild(0).gameObject.SetActive(false);
                    instance.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    instance.transform.localScale = Vector3.one;
                    instance.transform.localPosition = new Vector3(0, -7.321347f, 0) + i * new Vector3(0, -9.044013f, 0);

                    ComicalCompany.Logger.LogError("Processed instance.");
                }
            }
        }
    }
}
