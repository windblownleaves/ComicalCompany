using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

// mostly adapted from https://lethal.wiki/dev/intermediate/custom-configs

namespace ComicalCompany.Configuration
{
    class ComicalCompanyConfig
    {
        public readonly ConfigEntry<bool> enableGreenMode;
        public readonly ConfigEntry<bool> enableFallDamage;
        public readonly ConfigEntry<bool> enableShipVent;
        public readonly ConfigEntry<bool> enableJumpFailure;
        public readonly ConfigEntry<bool> enableOpenDoorsInSpace;

        public ComicalCompanyConfig(ConfigFile cfg)
        {
            cfg.SaveOnConfigSet = false;

            enableGreenMode = cfg.Bind(
                "General",
                "Green",
                true,
                "Green"
            );

            enableFallDamage = cfg.Bind(
                "General",
                "Gravity",
                true,
                "Ouch this hurts"
            );

            enableShipVent = cfg.Bind(
                "General",
                "FreshAirInShip",
                true,
                "It can get really stuffy in there"
            );

            enableJumpFailure = cfg.Bind(
                "General",
                "RealisticJumping",
                true,
                "Sometimes, you just can't, you know?"
            );

            enableOpenDoorsInSpace = cfg.Bind(
                "General",
                "FixDoorButton",
                true,
                "Occasionally, the door buttons won't work. This fix applies in those situations."
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
