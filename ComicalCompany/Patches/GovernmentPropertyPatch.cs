using HarmonyLib;

namespace ComicalCompany.Patches
{
    [HarmonyPatch]
    public class GovernmentPropertyPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DepositItemsDesk), "SellItemsOnServer")]
        public static void SellItemsOnServer(DepositItemsDesk __instance)
        {
            if (!__instance.IsServer) return;
            for (int i = 0; i < __instance.itemsOnCounter.Count; i++)
            {
                if (__instance.itemsOnCounter[i].itemProperties.itemName.ToLower().Contains("government"))
                {
                    __instance.itemsOnCounter[i].scrapValue = -__instance.itemsOnCounter[i].scrapValue;
                }
            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DepositItemsDesk), "delayedAcceptanceOfItems")]
        public static void delayedAcceptanceOfItems(GrabbableObject[] objectsOnDesk)
        {
            for (int i = 0; i < objectsOnDesk.Length; i++)
            {
                if (objectsOnDesk[i].itemProperties.itemName.ToLower().Contains("government"))
                {
                    HUDManager.Instance.DisplayTip("Government property detected", "Fine deducted from credits", true, false, "LC_Tip1");
                    return;
                }
            }
        }
    }
}
