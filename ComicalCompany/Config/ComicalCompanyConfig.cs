using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

// mostly adapted from https://lethal.wiki/dev/intermediate/custom-configs

namespace ComicalCompany.Configuration
{
    class ComicalCompanyConfig
    {
        public readonly ConfigEntry<bool> enableApparatusPatch;
        public readonly ConfigEntry<bool> enableBoomBoxPatch;
        public readonly ConfigEntry<bool> enableCoilheadPatch;
        public readonly ConfigEntry<bool> enableEnemySizePatch;
        public readonly ConfigEntry<bool> enableFallDamagePatch;
        public readonly ConfigEntry<bool> enableGovernmentPropertyPatch;
        public readonly ConfigEntry<bool> enableGreenModePatch;
        public readonly ConfigEntry<bool> enableItemChargerPatch;
        public readonly ConfigEntry<bool> enableItemNamePatch;
        public readonly ConfigEntry<bool> enableJesterInfestation;
        public readonly ConfigEntry<bool> enableJumpPatch;
        public readonly ConfigEntry<bool> enableLadderPatch;
        public readonly ConfigEntry<bool> enableLandminePatch;
        public readonly ConfigEntry<bool> enableOrbitDoorPatch;
        public readonly ConfigEntry<bool> enableQuicksandPatch;
        public readonly ConfigEntry<bool> enableTZPPatch;
        public readonly ConfigEntry<bool> enableVentPatch;
        public readonly ConfigEntry<bool> enableSwitchPatch;
        public readonly ConfigEntry<bool> enableTeleporterPatch;

        public ComicalCompanyConfig(ConfigFile cfg)
        {
            cfg.SaveOnConfigSet = false;

            enableApparatusPatch = cfg.Bind(
                "General",
                "EasterApparatus",
                true,
                "It's celebration time"
            );

            enableBoomBoxPatch = cfg.Bind(
                "General",
                "BackgroundMusic",
                true,
                "So it doesn't get boring inside"
            );

            enableCoilheadPatch = cfg.Bind(
                "General",
                "Blinking",
                true,
                "To hydrate the eye"
            );

            enableEnemySizePatch = cfg.Bind(
                "General",
                "RealisticEnemies",
                true,
                "Why is everything so big?? now it's fixed"
            );

            enableFallDamagePatch = cfg.Bind(
                "General",
                "Gravity",
                true,
                "Ouch this hurts"
            );

            enableGovernmentPropertyPatch = cfg.Bind(
                "General",
                "GovernmentProperty",
                true,
                "This isn't yours to sell?? wtf"
            );

            enableGreenModePatch = cfg.Bind(
                "General",
                "Green",
                true,
                "Green"
            );

            enableItemChargerPatch = cfg.Bind(
                "General",
                "Electricity",
                true,
                "bzzzzzzzzzzzt"
            );

            enableItemNamePatch = cfg.Bind(
                "General",
                "Translation",
                true,
                "Fixes the error in the language of the Lethal COmpany"
            );

            enableJesterInfestation = cfg.Bind(
                "General",
                "Jest",
                true,
                "It's all a joke..."
            );

            enableJumpPatch = cfg.Bind(
                "General",
                "RealisticJumping",
                true,
                "Sometimes, you just can't, you know?"
            );

            enableLadderPatch = cfg.Bind(
                "General",
                "LongerLadders",
                true,
                "It does what it says on the tin"
            );

            enableLandminePatch = cfg.Bind(
                "General",
                "BetterLandmines",
                true,
                "Goodbye"
            );

            enableOrbitDoorPatch = cfg.Bind(
                "General",
                "FixDoorButton",
                true,
                "Occasionally, the door buttons won't work. This fix applies in those situations."
            );

            enableQuicksandPatch = cfg.Bind(
                "General",
                "BetterQuicksand",
                true,
                "It's not called SLOWSAND is it?????"
            );

            enableTZPPatch = cfg.Bind(
                "General",
                "Skooma",
                true,
                "M'aiq wishes you well."
            );

            enableVentPatch = cfg.Bind(
                "General",
                "FreshAirInShip",
                true,
                "It can get really stuffy in there"
            );

            enableSwitchPatch = cfg.Bind(
                "General",
                "SwitchSwitches",
                true,
                "Wait, that wasn't the lights!"
            );

            enableTeleporterPatch = cfg.Bind(
                "General",
                "FaultyTeleportation",
                true,
                "Transmitting someone as pure energy is tricky. Sometimes the data gets corrupted on arrival."
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
