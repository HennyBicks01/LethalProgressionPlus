using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using BepInEx.Configuration;

namespace LethalProgression.Config
{
    internal class SkillConfig
    {
        public static IDictionary<string, string> hostConfig = new Dictionary<string, string>();
        public static void InitConfig()
        {
            LethalPlugin.Instance.BindConfig<int>(
                "General",
                "Person Multiplier",
                35,
                "How much does XP cost to level up go up per person?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "General",
                "Quota Multiplier",
                30,
                "How much more XP does it cost to level up go up per quota? (Percent)"
                );
            LethalPlugin.Instance.BindConfig<int>(
                "General",
                "XP Minimum",
                40,
                "Minimum XP to level up."
                );
            LethalPlugin.Instance.BindConfig<int>(
                "General",
                "XP Maximum",
                750,
                "Maximum XP to level up."
                );
            LethalPlugin.Instance.BindConfig<bool>(
                "General",
                "Unspec in Ship Only",
                true,
                "Disallows unspecing stats if you're not currently on the ship."
                );
            LethalPlugin.Instance.BindConfig<bool>(
                "General",
                "Disable Unspec",
                true,
                "Disallows unspecing altogether."
                );

            // Health Regen
            LethalPlugin.Instance.BindConfig<bool>(
                "Health Regen",
                "Health Regen Enabled",
                true,
                "Enable the Health Regen skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Health Regen",
                "Health Regen Max Level",
                20,
                "Maximum level for the health regen."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Health Regen",
                "Health Regen Multiplier",
                0.05f,
                "How much does the health regen skill increase per level?"
                );

            // Stamina
            LethalPlugin.Instance.BindConfig<bool>(
                "Stamina",
                "Stamina Enabled",
                true,
                "Enable the Stamina skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Stamina",
                "Stamina Max Level",
                99999,
                "Maximum level for the stamina."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Stamina",
                "Stamina Multiplier",
                2,
                "How much does the stamina skill increase per level?"
                );

            // Battery
            LethalPlugin.Instance.BindConfig<bool>(
                "Battery",
                "Battery Life Enabled",
                true,
                "Enable the Battery Life skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Battery",
                "Battery Life Max Level",
                99999,
                "Maximum level for the battery life."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Battery",
                "Battery Life Multiplier",
                5,
                "How much does the battery life skill increase per level?"
                );

            // Hand Slots
            LethalPlugin.Instance.BindConfig<bool>(
                "Hand Slots",
                "Hand Slots Enabled",
                true,
                "Enable the Hand Slots skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Hand Slots",
                "Hand Slots Max Level",
                99999,
                "Maximum level for the hand slots."
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Hand Slots",
                "Hand Slots Initial Cost",
                10,
                "Initial cost for a handslot"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Hand Slots",
                "Hand Slots Increment",
                3,
                "Cost increase per handslot"
                );

            // Loot Value
            LethalPlugin.Instance.BindConfig<bool>(
                "Loot Value",
                "Loot Value Enabled",
                true,
                "Enable the Loot Value skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Loot Value",
                "Loot Value Max Level",
                99999,
                "Maximum level for the loot value."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Loot Value",
                "Loot Value Multiplier",
                1f,
                "How much does the loot value skill increase per level?"
                );

            // Oxygen
            LethalPlugin.Instance.BindConfig<bool>(
                "Oxygen",
                "Oxygen Enabled",
                true,
                "Enable the Oxygen skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Oxygen",
                "Oxygen Max Level",
                99999,
                "Maximum level for Oxygen."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Oxygen",
                "Oxygen Multiplier",
                1f,
                "How much does the Oxygen skill increase per level?"
                );

            // Strength
            LethalPlugin.Instance.BindConfig<bool>(
                "Strength",
                "Strength Enabled",
                true,
                "Enable the Strength skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Strength",
                "Strength Max Level",
                99999,
                "Maximum level for the Strength."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Strength",
                "Strength Multiplier",
                1,
                "How much does the Strength skill increase per level?"
                );

            // Power
            LethalPlugin.Instance.BindConfig<bool>(
                "Power",
                "Power Enabled",
                true,
                "Enable the Power skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Power",
                "Power Max Level",
                99999,
                "Maximum level for the Power."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Power",
                "Power Multiplier",
                1,
                "How much does the Power skill increase per level?"
                );

            // Jump
            LethalPlugin.Instance.BindConfig<bool>(
                "Jump",
                "Jump Enabled",
                true,
                "Enable the Jump skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Jump",
                "Jump Max Level",
                99999,
                "Maximum level for the Jump."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Jump",
                "Jump Multiplier",
                1,
                "How much does the Jump skill increase per level?"
                );

            // Speed
            LethalPlugin.Instance.BindConfig<bool>(
                "Speed",
                "Speed Enabled",
                true,
                "Enable the Speed skill?"
                );

            LethalPlugin.Instance.BindConfig<int>(
                "Speed",
                "Speed Max Level",
                99999,
                "Maximum level for the Speed."
                );

            LethalPlugin.Instance.BindConfig<float>(
                "Speed",
                "Speed Multiplier",
                1,
                "How much does the Speed skill increase per level?"
                );
        }
    }
}