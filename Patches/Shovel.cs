using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace LethalProgression.Skills
{
    [HarmonyPatch(typeof(Shovel), "SwingShovel")]
    internal class ShovelSwingPatch
    {
        public static void Prefix(Shovel __instance)
        {
            if (__instance.playerHeldBy != null)
            {
                Skill skill = LP_NetworkManager.xpInstance.skillList.skills[UpgradeType.Power];
                float powerMultiplier = Power.CalculatePowerMultiplier(skill.GetLevel());

                // Assuming shovelHitForce is a property of Shovel that determines its power.
                __instance.shovelHitForce = (int)(__instance.shovelHitForce * powerMultiplier);


                LethalPlugin.Log.LogInfo($"Applied power multiplier to shovel: {powerMultiplier}");
            }
        }
    }
}
