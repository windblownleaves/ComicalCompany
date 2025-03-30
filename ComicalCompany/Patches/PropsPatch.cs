using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using UnityEngine;
using Unity.AI.Navigation;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace ComicalCompany.Patches
{
    [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.SpawnOutsideHazards))]
    public static class PropsPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(RoundManager __instance)
        {
            CustomSpawnMapObjects(__instance);

            return false;
        }

        private static void CustomSpawnMapObjects(RoundManager __instance)
        {
            System.Random random = new System.Random(StartOfRound.Instance.randomMapSeed + 2);
            __instance.outsideAINodes = (from x in GameObject.FindGameObjectsWithTag("OutsideAINode")
                              orderby Vector3.Distance(x.transform.position, Vector3.zero)
                              select x).ToArray();
            NavMeshHit navMeshHit = default(NavMeshHit);
            int num = 0;
            if (TimeOfDay.Instance.currentLevelWeather == LevelWeatherType.Rainy)
            {
                num = random.Next(5, 15);
                num *= StartOfRound.Instance.randomMapSeed % 10;
                if (random.Next(0, 100) < 7)
                {
                    num = random.Next(5, 30);
                }
                for (int i = 0; i < num; i++)
                {
                    Vector3 position = __instance.outsideAINodes[random.Next(0, __instance.outsideAINodes.Length)].transform.position;
                    Vector3 position2 = __instance.GetRandomNavMeshPositionInBoxPredictable(position, 30f, navMeshHit, random) + Vector3.up;
                    GameObject gameObject = UnityEngine.Object.Instantiate(__instance.quicksandPrefab, position2, Quaternion.identity, __instance.mapPropsContainer.transform);
                }
            }
            int num2 = 0;
            List<Vector3> list = new List<Vector3>();
            __instance.spawnDenialPoints = GameObject.FindGameObjectsWithTag("SpawnDenialPoint");
            if (__instance.currentLevel.spawnableOutsideObjects != null)
            {
                for (int j = 0; j < __instance.currentLevel.spawnableOutsideObjects.Length; j++)
                {
                    double num3 = random.NextDouble();
                    num = (int)__instance.currentLevel.spawnableOutsideObjects[j].randomAmount.Evaluate((float)num3);
                    num *= StartOfRound.Instance.randomMapSeed % 10;
                    if (__instance.increasedMapPropSpawnRateIndex == j)
                    {
                        num += 12;
                    }
                    if ((float)random.Next(0, 100) < 20f)
                    {
                        num *= 2;
                    }
                    for (int k = 0; k < num; k++)
                    {
                        int num4 = random.Next(0, __instance.outsideAINodes.Length);
                        Vector3 position2 = __instance.GetRandomNavMeshPositionInBoxPredictable(__instance.outsideAINodes[num4].transform.position, 30f, navMeshHit, random);
                        if (__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.spawnableFloorTags != null)
                        {
                            bool flag = false;
                            if (Physics.Raycast(position2 + Vector3.up, Vector3.down, out var hitInfo, 5f, StartOfRound.Instance.collidersAndRoomMaskAndDefault))
                            {
                                for (int l = 0; l < __instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.spawnableFloorTags.Length; l++)
                                {
                                    if (hitInfo.collider.transform.CompareTag(__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.spawnableFloorTags[l]))
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (!flag)
                            {
                                continue;
                            }
                        }
                        position2 = __instance.PositionEdgeCheck(position2, __instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.objectWidth);
                        if (position2 == Vector3.zero)
                        {
                            continue;
                        }
                        bool flag2 = false;
                        for (int m = 0; m < __instance.shipSpawnPathPoints.Length; m++)
                        {
                            if (Vector3.Distance(__instance.shipSpawnPathPoints[m].transform.position, position2) < (float)__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.objectWidth + 6f)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                        if (flag2)
                        {
                            continue;
                        }
                        for (int n = 0; n < __instance.spawnDenialPoints.Length; n++)
                        {
                            if (Vector3.Distance(__instance.spawnDenialPoints[n].transform.position, position2) < (float)__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.objectWidth + 6f)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                        if (flag2)
                        {
                            continue;
                        }
                        if (Vector3.Distance(GameObject.FindGameObjectWithTag("ItemShipLandingNode").transform.position, position2) < (float)__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.objectWidth + 4f)
                        {
                            flag2 = true;
                            break;
                        }
                        if (flag2)
                        {
                            continue;
                        }
                        if (__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.objectWidth > 4)
                        {
                            flag2 = false;
                            for (int num5 = 0; num5 < list.Count; num5++)
                            {
                                if (Vector3.Distance(position2, list[num5]) < (float)__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.objectWidth)
                                {
                                    flag2 = true;
                                    break;
                                }
                            }
                            if (flag2)
                            {
                                continue;
                            }
                        }
                        list.Add(position2);
                        GameObject gameObject = UnityEngine.Object.Instantiate(__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.prefabToSpawn, position2 - Vector3.up * 0.7f, Quaternion.identity, __instance.mapPropsContainer.transform);
                        num2++;
                        if (__instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.spawnFacingAwayFromWall)
                        {
                            gameObject.transform.eulerAngles = new Vector3(0f, __instance.YRotationThatFacesTheFarthestFromPosition(position2 + Vector3.up * 0.2f), 0f);
                        }
                        else
                        {
                            int num6 = random.Next(0, 360);
                            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, num6, gameObject.transform.eulerAngles.z);
                        }
                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x + __instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.rotationOffset.x, gameObject.transform.localEulerAngles.y + __instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.rotationOffset.y, gameObject.transform.localEulerAngles.z + __instance.currentLevel.spawnableOutsideObjects[j].spawnableObject.rotationOffset.z);
                    }
                }
            }
            if (num2 > 0)
            {
                GameObject gameObject2 = GameObject.FindGameObjectWithTag("OutsideLevelNavMesh");
                if (gameObject2 != null)
                {
                    gameObject2.GetComponent<NavMeshSurface>().BuildNavMesh();
                }
            }
            __instance.bakedNavMesh = true;
        }
    }
}
