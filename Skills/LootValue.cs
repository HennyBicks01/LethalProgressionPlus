using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using LethalProgression.Config;
using Unity.Netcode;

namespace LethalProgression.Skills
{
    [HarmonyPatch]
    internal class LootValue
    {
        private static readonly float BaseScrapValueMultiplier = .35f; // Define the base multiplier

        [HarmonyPrefix]
        [HarmonyPatch(typeof(RoundManager), "SpawnScrapInLevel")]
        private static void AddLootValue()
        {
            if (!LP_NetworkManager.xpInstance.skillList.IsSkillListValid())
                return;

            if (!LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.Value))
                return;

            // Reset the scrap value multiplier to its base value
            RoundManager.Instance.scrapValueMultiplier = BaseScrapValueMultiplier;

            // Apply the loot value bonus
            float mult = LP_NetworkManager.xpInstance.skillList.skills[UpgradeType.Value].GetMultiplier();
            RoundManager.Instance.scrapValueMultiplier += (mult*7*LP_NetworkManager.xpInstance.teamLootValue.Value / 2000.0f);
        }

        public static void LootValueUpdate(int change, int newLevel)
        {
            if (!LP_NetworkManager.xpInstance.skillList.IsSkillListValid())
                return;

            if (!LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.Value))
                return;

            LP_NetworkManager.xpInstance.TeamLootValueUpdate(change, newLevel);
        }
    }
}
