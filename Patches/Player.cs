using System;
using System.Collections.Generic;
using System.Text;
using GameNetcodeStuff;
using HarmonyLib;

namespace LethalProgression.Skills
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static void UpdatePrefix(PlayerControllerB __instance)
        {
            Strength.CheckAndUpdateCarryWeight();
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void UpdatePostfix(PlayerControllerB __instance)
        {
            ApplySpeedUpdate(__instance);
        }

        private static void ApplySpeedUpdate(PlayerControllerB playerController)
        {
            if (playerController == null || !LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.Speed))
                return;

            Skill skill = LP_NetworkManager.xpInstance.skillList.skills[UpgradeType.Speed];
            float speedEnhancement = (skill.GetLevel() * skill.GetMultiplier() / 100f) * 4.6f;
            playerController.movementSpeed = 4.6f + speedEnhancement; // Reset to base speed and then apply enhancement
        }
    }
}

