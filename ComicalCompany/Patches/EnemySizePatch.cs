using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class EnemySizePatch
    {
        public static GameObject? Lollypop;
        public static GameObject? FunnyHat;
        [HarmonyPostfix]
        [HarmonyPatch(typeof(EnemyAI), "Start")]
        public static void Awake(EnemyAI __instance)
        {
            if (__instance.GetType() == typeof(ForestGiantAI))
            {
                __instance.transform.localScale = __instance.transform.localScale / 4;
                __instance.enemyType.enemyName = "Forest";
                if (Lollypop != null && FunnyHat != null)
                {
                    // WIP not working
                    ComicalCompany.Logger.LogInfo("transform: " + __instance.gameObject.transform);
                    ComicalCompany.Logger.LogInfo("transform: " + __instance.gameObject.transform.childCount);

                    Transform AnimContainer = __instance.gameObject.transform.Find("FGiantModelContainer").transform.Find("AnimContainer");
                    ComicalCompany.Logger.LogInfo("AnimContainer: " + AnimContainer);
                    Transform HeadTransform = AnimContainer.transform.Find("metarig").transform.Find("spine").transform.Find("spine.003").transform.Find("shoulder.L");
                    ComicalCompany.Logger.LogInfo("HeadTransform: " + HeadTransform);
                    GameObject funnyHat = GameObject.Instantiate(FunnyHat, HeadTransform);
                    funnyHat.transform.localPosition = new Vector3(0, 1f, 0);
                    funnyHat.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    
                    ComicalCompany.Logger.LogInfo("FunnyHat: " + funnyHat);
                    Transform HandTransform = HeadTransform.transform.Find("upper_arm.L").transform.Find("forearm.L").transform.Find("hand.L");
                    ComicalCompany.Logger.LogInfo("HandTransform: " + HandTransform.position);
                    GameObject lollypop = GameObject.Instantiate(Lollypop, HandTransform);
                    lollypop.transform.localPosition = new Vector3(-0.2f, -0.5f, 0);
                    lollypop.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    ComicalCompany.Logger.LogInfo("Lollypop: " + lollypop);


                }
            }
            else if (__instance.GetType() == typeof(SandSpiderAI))
            {
                __instance.transform.localScale = __instance.transform.localScale / 5;
            }
            else if (__instance.GetType() == typeof(RadMechAI))
            {
                __instance.transform.localScale = __instance.transform.localScale / 5;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SandSpiderWebTrap), "Awake")]
        public static void WebAwake(SandSpiderWebTrap __instance)
        {
            __instance.transform.localScale = __instance.transform.localScale * 2;
        }
    }
}
