using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

// mostly adapted from https://lethal.wiki/dev/intermediate/custom-configs

namespace ComicalCompany.Configuration
{
    class ComicalCompanyConfig
    {
        public readonly ConfigEntry<bool> enableApparatusExplosion;
        public readonly ConfigEntry<bool> enableCoilheadCheating;
        public readonly ConfigEntry<bool> enableEnemySizeAdjustments;
        public readonly ConfigEntry<bool> enableFallDamage;
        public readonly ConfigEntry<bool> enableGreenMode;
        public readonly ConfigEntry<bool> enableElectrocution;
        public readonly ConfigEntry<bool> enableItemRenaming;
        public readonly ConfigEntry<bool> enableJumpFailure;
        public readonly ConfigEntry<bool> enableOrbitDoor;
        public readonly ConfigEntry<bool> enableShipVent;
        public readonly ConfigEntry<bool> enableInstantTZP;

        public ComicalCompanyConfig(ConfigFile cfg)
        {
            cfg.SaveOnConfigSet = false;

            enableApparatusExplosion = cfg.Bind(
                "General",
                "EasterApparatus",
                true,
                "It's celebration time"
            );

            enableCoilheadCheating = cfg.Bind(
                "General",
                "Blinking",
                true,
                "To hydrate the eye"
            );

            enableEnemySizeAdjustments = cfg.Bind(
                "General",
                "RealisticEnemies",
                true,
                "Why is everything so big??"
            );

            enableFallDamage = cfg.Bind(
                "General",
                "Gravity",
                true,
                "Ouch this hurts"
            );

            enableGreenMode = cfg.Bind(
                "General",
                "Green",
                true,
                "Green"
            );

            enableElectrocution = cfg.Bind(
                "General",
                "Electricity",
                true,
                "bzzzzzzzzzzzt"
            );

            enableItemRenaming = cfg.Bind(
                "General",
                "Translation",
                true,
                "Fixes the error in the language of the Lethal COmpany"
            );

            enableJumpFailure = cfg.Bind(
                "General",
                "RealisticJumping",
                true,
                "Sometimes, you just can't, you know?"
            );

            enableOrbitDoor = cfg.Bind(
                "General",
                "FixDoorButton",
                true,
                "Occasionally, the door buttons won't work. This fix applies in those situations."
            );

            enableShipVent = cfg.Bind(
                "General",
                "FreshAirInShip",
                true,
                "It can get really stuffy in there"
            );

            enableInstantTZP = cfg.Bind(
                "General",
                "Skooma",
                true,
                "M'aiq wishes you well."
            );

            ClearOrphanedEntries(cfg);
            cfg.Save();
            cfg.SaveOnConfigSet = true;
        }

        static void ClearOrphanedEntries(ConfigFile cfg)
        {
            PropertyInfo orphanedEntriesProp = AccessTools.Property(typeof(ConfigFile), "OrphanedEntries");
            var orphanedEntries = (Dictionary<ConfigDefinition, string>)orphanedEntriesProp.GetValue(cfg);
            orphanedEntries.Clear();
        }
    }
}
