using System;
using System.Collections;
using System.Text;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using System.Timers;

namespace LethalProgression.Skills
{
    internal class Strength
    {
        private static PlayerControllerB localPlayer;
        private static float lastCarryWeight;

        public static void Initialize()
        {
            localPlayer = GameNetworkManager.Instance.localPlayerController;
            lastCarryWeight = localPlayer.carryWeight;
        }

        public static void CheckAndUpdateCarryWeight()
        {
            if (localPlayer == null)
            {
                return;
            }

            if (Mathf.Abs(localPlayer.carryWeight - lastCarryWeight) > Mathf.Epsilon)
            {
                StrengthUpdates();
                lastCarryWeight = localPlayer.carryWeight;
            }
        }

        public static void StrengthUpdates()
        {
            if (!LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.Strength))
                return;

            Skill skill = LP_NetworkManager.xpInstance.skillList.skills[UpgradeType.Strength];

            float weightReductionPercent = skill.GetLevel() * 0.01f; // 1% per level

            float totalWeight = 1f;
            foreach (var obj in localPlayer.ItemSlots)
            {
                if (obj != null)
                {
                    totalWeight += (obj.itemProperties.weight - 1f);
                }
            }

            localPlayer.carryWeight = Mathf.Max(1 + ((totalWeight - 1) * (1 - weightReductionPercent)), 1f);
            LethalPlugin.Log.LogInfo("New carry weight: " + localPlayer.carryWeight);
        }

        public static void StrengthUpdate(int updatedValue, int newStrength)
        {
            if (!LP_NetworkManager.xpInstance.skillList.IsSkillListValid())
                return;

            if (!LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.Strength))
                return;

            Skill skill = LP_NetworkManager.xpInstance.skillList.skills[UpgradeType.Strength];
            Initialize();

            // Calculate the total weight reduction based on Strength level
            float weightReductionPercent = skill.GetLevel() * 0.01f; // 1% per level

            float totalWeight = 1f; // Base weight
            foreach (var obj in localPlayer.ItemSlots)
            {
                if (obj != null)
                {
                    totalWeight += (obj.itemProperties.weight - 1f);
                }
            }
            localPlayer.carryWeight = Mathf.Max(1 + ((totalWeight - 1) * (1 - weightReductionPercent)), 1f);
            LethalPlugin.Log.LogInfo($"Strength level: {skill.GetLevel()}, New carry weight: {localPlayer.carryWeight}");
        }
    }
}