using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ComicalCompany.Utils
{
    public class Utils
    {
        public static List<EnemyType> allEnemyTypes;
        // Destroy the game object
        public static void DestroyGameObject(GameObject gameObject)
        {
            UnityEngine.Object.Destroy(gameObject);
            MeshRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                UnityEngine.Object.Destroy(componentsInChildren[i]);
            }
            Collider[] componentsInChildren2 = gameObject.GetComponentsInChildren<Collider>();
            for (int j = 0; j < componentsInChildren2.Length; j++)
            {
                UnityEngine.Object.Destroy(componentsInChildren2[j]);
            }
        }
    }
}
