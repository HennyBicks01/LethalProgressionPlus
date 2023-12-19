using System;
using System.Collections.Generic;
using System.Text;
using GameNetcodeStuff;

namespace LethalProgression.Skills
{
    internal class Jump
    {
        public static void JumpUpdate(int updatedValue, int newJumpLevel)
        {
            if (!LP_NetworkManager.xpInstance.skillList.IsSkillListValid())
                return;

            if (!LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.Jump))
                return;

            Skill skill = LP_NetworkManager.xpInstance.skillList.skills[UpgradeType.Jump];
            PlayerControllerB localPlayer = GameNetworkManager.Instance.localPlayerController;

            // Calculate the Jump enhancement based on Jump level
            float JumpEnhancement = (newJumpLevel * skill.GetMultiplier() / 100f) * 13f;
            localPlayer.jumpForce += JumpEnhancement;
            LethalPlugin.Log.LogInfo($"Jump level: {skill.GetLevel()}, New Jump: {localPlayer.jumpForce}");
        }
    }
}