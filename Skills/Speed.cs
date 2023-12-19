using System;
using System.Collections.Generic;
using System.Text;
using GameNetcodeStuff;

namespace LethalProgression.Skills
{
    internal class Speed
    {
        public static void SpeedUpdate(int updatedValue, int newSpeedLevel)
        {
            if (!LP_NetworkManager.xpInstance.skillList.IsSkillListValid())
                return;

            if (!LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.Speed))
                return;

            Skill skill = LP_NetworkManager.xpInstance.skillList.skills[UpgradeType.Speed];
            PlayerControllerB localPlayer = GameNetworkManager.Instance.localPlayerController;

            // Calculate the speed enhancement based on Speed level
            float speedEnhancement = (newSpeedLevel * skill.GetMultiplier() / 100f) * 4.6f;
            localPlayer.movementSpeed += speedEnhancement;
            LethalPlugin.Log.LogInfo($"Speed level: {skill.GetLevel()}, New movement speed: {localPlayer.movementSpeed}");
        }
    }
}


