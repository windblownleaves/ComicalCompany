using HarmonyLib;

namespace ComicalCompany.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    public class InspirationPatch
    {
        [HarmonyPatch(nameof(RoundManager.GenerateNewLevelClientRpc))]
        [HarmonyPostfix]
        private static void GenerateNewLevelClientRpcPostfix()
        {
            int seed = StartOfRound.Instance.randomMapSeed;

            switch (seed % 100)
            {
                case 01:
                    HUDManager.Instance.DisplayTip("sdfjkds", "adkjsd HJASBA Jhsajd  shjkad", false, false, "LC_Tip1");
                    break;
                case 02:
                    HUDManager.Instance.DisplayTip("hello", "hi", false, false, "LC_Tip1");
                    break;
                case 03:
                    HUDManager.Instance.DisplayTip("whatg's up..", "not a lot and u", false, false, "LC_Tip1");
                    break;
                case 04:
                    HUDManager.Instance.DisplayTip("why leg do that", "bro ur sideways rn", false, false, "LC_Tip1");
                    break;
                case 06:
                    HUDManager.Instance.DisplayTip("haahhahahaha", "wait this isn’t funny", false, false, "LC_Tip1");
                    break;
                case 07:
                    HUDManager.Instance.DisplayTip("???", "ok but like how did we get HERE", false, false, "LC_Tip1");
                    break;
                case 08:
                    HUDManager.Instance.DisplayTip("me when the", "yeah", false, false, "LC_Tip1");
                    break;
                case 11:
                    HUDManager.Instance.DisplayTip("whomst laddered", "he do be climbing tho", false, false, "LC_Tip1");
                    break;
                case 13:
                    HUDManager.Instance.DisplayTip("69% loaded", "nice", false, false, "LC_Tip1");
                    break;
                case 14:
                    HUDManager.Instance.DisplayTip("do NOT open that", "he opened it", false, false, "LC_Tip1");
                    break;
                case 15:
                    HUDManager.Instance.DisplayTip("u ok bro?", "no i heard the music", true, false, "LC_Tip1");
                    break;
                case 17:
                    HUDManager.Instance.DisplayTip("HUNGY", "floor snack?", false, false, "LC_Tip1");
                    break;
                case 18:
                    HUDManager.Instance.DisplayTip("keybind lost", "walk forward to apologize", false, false, "LC_Tip1");
                    break;
                case 19:
                    HUDManager.Instance.DisplayTip("help", "vent jsut looked at me", false, false, "LC_Tip1");
                    break;
                case 21:
                    HUDManager.Instance.DisplayTip("oopsie woopsie", "we did a killie willie", false, false, "LC_Tip1");
                    break;
                case 22:
                    HUDManager.Instance.DisplayTip("who coded this", "seriously who made thsi", false, false, "LC_Tip1");
                    break;
                case 23:
                    HUDManager.Instance.DisplayTip("vent doing vent things", "classic vent moment", false, false, "LC_Tip1");
                    break;
                case 24:
                    HUDManager.Instance.DisplayTip("where am i", "no seriously where’s the exit", false, false, "LC_Tip1");
                    break;
                case 25:
                    HUDManager.Instance.DisplayTip("bro what", "did the door just sigh", false, false, "LC_Tip1");
                    break;
                case 26:
                    HUDManager.Instance.DisplayTip("tuesday", "it’s always tuesday here", false, false, "LC_Tip1");
                    break;
                case 28:
                    HUDManager.Instance.DisplayTip("get in loser", "we’re dying today", true, false, "LC_Tip1");
                    break;
            }
        }
    }
}
