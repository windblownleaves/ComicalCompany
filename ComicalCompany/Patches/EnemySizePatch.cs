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
        [HarmonyPostfix]
        [HarmonyPatch(typeof(EnemyAI), "Start")]
        public static void Awake(EnemyAI __instance)
        {
            if (__instance.GetType() == typeof(ForestGiantAI))
            {
                __instance.transform.localScale = __instance.transform.localScale / 4;
                __instance.enemyType.enemyName = "Forest";
                __instance.agent.height = 1;
                __instance.agent.radius = 0.5f;

                if (ComicalCompany.lollipopPrefab != null && ComicalCompany.hatPrefab != null)
                {
                    // WIP not working
                    ComicalCompany.Logger.LogInfo("transform: " + __instance.gameObject.transform);
                    ComicalCompany.Logger.LogInfo("transform: " + __instance.gameObject.transform.childCount);

                    Transform AnimContainer = __instance.gameObject.transform.Find("FGiantModelContainer").transform.Find("AnimContainer");
                    ComicalCompany.Logger.LogInfo("AnimContainer: " + AnimContainer);
                    Transform HeadTransform = AnimContainer.transform.Find("metarig").transform.Find("spine").transform.Find("spine.003").transform.Find("shoulder.L");
                    ComicalCompany.Logger.LogInfo("HeadTransform: " + HeadTransform);
                    GameObject funnyHat = GameObject.Instantiate(ComicalCompany.hatPrefab, HeadTransform);
                    funnyHat.transform.localPosition = new Vector3(0.006f, -0.055f, 0.145f);
                    funnyHat.transform.localRotation = Quaternion.Euler(34, -112.5f, -89f);

                    ComicalCompany.Logger.LogInfo("FunnyHat: " + funnyHat);
                    Transform HandTransform = HeadTransform.transform.Find("upper_arm.L").transform.Find("forearm.L").transform.Find("hand.L");
                    ComicalCompany.Logger.LogInfo("HandTransform: " + HandTransform.position);
                    GameObject lollypop = GameObject.Instantiate(ComicalCompany.lollipopPrefab, HandTransform);
                    lollypop.transform.localPosition = new Vector3(-0.076f, 0.239f, -0.013f);
                    lollypop.transform.localRotation = Quaternion.Euler(90, 83.5f, 86);
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
                __instance.agent.height = 1;
                __instance.agent.radius = 0.5f;
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
