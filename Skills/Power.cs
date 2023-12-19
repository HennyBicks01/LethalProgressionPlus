using GameNetcodeStuff;
using HarmonyLib;

namespace LethalProgression.Skills
{
    internal class Power
    {
        public static void PowerUpdate(int updatedValue, int newPower)
        {
            if (!LP_NetworkManager.xpInstance.skillList.IsSkillListValid())
                return;

            if (!LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.Power))
                return;

            Skill skill = LP_NetworkManager.xpInstance.skillList.skills[UpgradeType.Power];
            float powerMultiplier = CalculatePowerMultiplier(skill.GetLevel());

            
            LethalPlugin.Log.LogInfo($"Power level: {skill.GetLevel()}, Power multiplier: {skill.GetLevel()}");
        }

        public static float CalculatePowerMultiplier(int powerLevel)
        {
            return 1f + (powerLevel * 0.10f); // Adjust as needed
        }
    }
}
