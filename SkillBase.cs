using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LethalProgression.Config;
using System.Globalization;

namespace LethalProgression.Skills
{
    public enum UpgradeType
    {
        HPRegen,
        Stamina,
        Speed,
        Jump,
        Battery,
        HandSlot,
        Value,
        Oxygen,
        Strength,
        Power,
    }

    internal class SkillList
    {
        public Dictionary<UpgradeType, Skill> skills = new Dictionary<UpgradeType, Skill>();

        public void CreateSkill(UpgradeType upgrade, string name, string description, string shortname, string attribute, UpgradeType upgradeType, int cost, int maxLevel, float multiplier, Action<int, int> callback = null, bool teamShared = false)
        {
            Skill newSkill = new Skill(name, description, shortname, attribute, upgradeType, cost, maxLevel, multiplier, callback, teamShared);
            skills.Add(upgrade, newSkill);
        }

        public bool IsSkillListValid()
        {
            if (skills.Count == 0)
            {
                return false;
            }

            return true;
        }

        public bool IsSkillValid(UpgradeType upgrade)
        {
            if (!skills.ContainsKey(upgrade))
            {
                LethalPlugin.Log.LogInfo("Skill " + upgrade.ToString() + " is not in the skill list!");
                return false;
            }

            return true;
        }

        public int GetCurrentSkillLevel(UpgradeType upgradeType)
        {
            if (skills.ContainsKey(upgradeType))
            {
                return skills[upgradeType].GetLevel();
            }
            else
            {
                // If the skill is not found, return a default level, e.g., 0
                return 0;
            }
        }



        public void InitializeSkills()
        {
            if (bool.Parse(SkillConfig.hostConfig["Health Regen Enabled"]))
            {
                LethalPlugin.Log.LogInfo("HP Regen check 1");
                CreateSkill(UpgradeType.HPRegen,
                    "Health Regen",
                    "The company installs a basic healer into your suit, letting you regenerate health slowly. Only regenerate up to 100 HP.",
                    "HPR",
                    "Health Regeneration",
                    UpgradeType.HPRegen,
                    1,
                    int.Parse(SkillConfig.hostConfig["Health Regen Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Health Regen Multiplier"], CultureInfo.InvariantCulture));
            }

            if (bool.Parse(SkillConfig.hostConfig["Stamina Enabled"]))
            {
                CreateSkill(UpgradeType.Stamina,
                    "Stamina",
                    "Hours on that company gym finally coming into play. Allows you to run for longer.",
                    "STM",
                    "Stamina",
                    UpgradeType.Stamina,
                    1,
                    int.Parse(SkillConfig.hostConfig["Stamina Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Stamina Multiplier"], CultureInfo.InvariantCulture),
                    Stamina.StaminaUpdate);
            }

            if (bool.Parse(SkillConfig.hostConfig["Battery Life Enabled"]))
            {
                CreateSkill(UpgradeType.Battery,
                    "Battery Life",
                    "The company provides you with better batteries. Replace your batteries AT THE SHIP'S CHARGER to see an effect.",
                    "BAT",
                    "Battery Life",
                    UpgradeType.Battery,
                    1,
                    int.Parse(SkillConfig.hostConfig["Battery Life Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Battery Life Multiplier"], CultureInfo.InvariantCulture));
            }

            if (bool.Parse(SkillConfig.hostConfig["Hand Slots Enabled"]) && !LethalPlugin.ReservedSlots)
            {
                int currentLevel = this.GetCurrentSkillLevel(UpgradeType.HandSlot);
                int maxLevel = int.Parse(SkillConfig.hostConfig["Hand Slots Max Level"]);
                float multiplier = 1;

                // Calculate initial description
                string initialDescription = GetInitialDescriptionForHandSlots(currentLevel, multiplier);

                CreateSkill(UpgradeType.HandSlot,
                            "Hand Slot",
                            initialDescription,
                            "HND",
                            "Hand Slots",
                            UpgradeType.HandSlot,
                            1,
                            maxLevel,
                            multiplier,
                            HandSlots.HandSlotsUpdate);
            }

            // Method to get the initial description based on the current level
            string GetInitialDescriptionForHandSlots(int currentLevel, float multiplier)
            {
                Skill tempSkill = new Skill("", "", "", "", UpgradeType.HandSlot, 0, 0, multiplier); // Create a temporary skill object
                tempSkill.SetLevel(currentLevel); // Set its level
                tempSkill.UpdateSkillDescription(); // Update its description
                return tempSkill.GetDescription(); // Return the updated description
            }


            if (bool.Parse(SkillConfig.hostConfig["Loot Value Enabled"]))
            {
                CreateSkill(UpgradeType.Value,
                    "Loot Value",
                    "The company gives you a better pair of eyes, allowing you to see the value in things.",
                    "VAL",
                    "Loot Value",
                    UpgradeType.Value,
                    1,
                    int.Parse(SkillConfig.hostConfig["Loot Value Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Loot Value Multiplier"], CultureInfo.InvariantCulture),
                    LootValue.LootValueUpdate);
            }

            if (bool.Parse(SkillConfig.hostConfig["Oxygen Enabled"]))
            {
                CreateSkill(UpgradeType.Oxygen,
                    "Oxygen",
                    "The company installs you with oxygen tanks. You gain extra time in the water. (Start drowning when the bar is empty.)",
                    "OXY",
                    "Extra Oxygen",
                    UpgradeType.Oxygen,
                    1,
                    int.Parse(SkillConfig.hostConfig["Oxygen Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Oxygen Multiplier"], CultureInfo.InvariantCulture));
            }

            if (bool.Parse(SkillConfig.hostConfig["Strength Enabled"]))
            {
                CreateSkill(UpgradeType.Strength,
                    "Strength",
                    "Hours on that company gym finally coming into play. Allows you to be hindered less by weight.",
                    "STR",
                    "Strength",
                    UpgradeType.Strength,
                    1,
                    int.Parse(SkillConfig.hostConfig["Strength Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Strength Multiplier"], CultureInfo.InvariantCulture),
                    Strength.StrengthUpdate);
                   
            }

            if (bool.Parse(SkillConfig.hostConfig["Power Enabled"]))
            {
                CreateSkill(UpgradeType.Power,
                    "Power",
                    "The company roids are begining to show results. Try out your new power on enemies or even friends.",
                    "POW",
                    "Power",
                    UpgradeType.Power,
                    1,
                    int.Parse(SkillConfig.hostConfig["Power Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Power Multiplier"], CultureInfo.InvariantCulture),
                    Power.PowerUpdate);

            }

            if (bool.Parse(SkillConfig.hostConfig["Jump Enabled"]))
            {
                CreateSkill(UpgradeType.Jump,
                    "Jump",
                    "The company roids are begining to show results. Try out your new Jump on enemies or even friends.",
                    "JMP",
                    "Jump",
                    UpgradeType.Jump,
                    1,
                    int.Parse(SkillConfig.hostConfig["Jump Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Jump Multiplier"], CultureInfo.InvariantCulture),
                    Jump.JumpUpdate);

            }

            if (bool.Parse(SkillConfig.hostConfig["Speed Enabled"]))
            {
                CreateSkill(UpgradeType.Speed,
                    "Speed",
                    "The company roids are begining to show results. Try out your new Speed on enemies or even friends.",
                    "SPD",
                    "Speed",
                    UpgradeType.Speed,
                    1,
                    int.Parse(SkillConfig.hostConfig["Speed Max Level"]),
                    float.Parse(SkillConfig.hostConfig["Speed Multiplier"], CultureInfo.InvariantCulture),
                    Speed.SpeedUpdate);

            }
        }
    }

    internal class Skill
    {
        private readonly string _shortName;
        private readonly string _name;
        private readonly string _attribute;
        private string _description;
        private readonly UpgradeType _upgradeType;
        private readonly int _cost;
        private readonly int _maxLevel;
        private readonly float _multiplier;
        private readonly Action<int, int> _callback;
        public bool _teamShared;
        private int _level;
        public Skill(string name, string description, string shortname, string attribute, UpgradeType upgradeType, int cost, int maxLevel, float multiplier, Action<int, int> callback = null, bool teamShared = false)
        {
            _name = name;
            _shortName = shortname;
            _attribute = attribute;
            _upgradeType = upgradeType;
            _description = description;
            _cost = cost;
            _maxLevel = maxLevel;
            _multiplier = multiplier;
            _level = 0;
            _callback = callback;
            _teamShared = teamShared;
        }

        public void UpdateSkillDescription()
        {
            if (this._upgradeType == UpgradeType.HandSlot)
            {
                int upgradesNeededForNextSlot = CalculateUpgradesNeededForNextSlot();
                _description = $"The company finally gives you a better belt! Fit more stuff! (Upgrade {upgradesNeededForNextSlot} more times for another slot.)";
            }
            // Add similar logic for other skill types if needed
        }

        public int CalculateUpgradesNeededForNextSlot()
        {
            // Assuming this._level is the current level of the user
            int currentLevel = this._level;

            // Starting point for the first slot beyond the base
            int nextThreshold = int.Parse(SkillConfig.hostConfig["Hand Slots Initial Cost"]);
            int increment = int.Parse(SkillConfig.hostConfig["Hand Slots Initial Cost"]) + int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]);  // Increment for the next threshold

            // Loop to find the next threshold greater than the current level
            while (currentLevel >= nextThreshold)
            {
                nextThreshold += increment;
                increment += int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]);
            }

            // Calculate how many more levels are needed to reach the next slot
            int levelsNeededForNextSlot = nextThreshold - currentLevel;

            return levelsNeededForNextSlot;
        }

        public int Slot()
        {
            // Assuming this._level is the current level of the user
            int currentLevel = this._level;

            // Starting point for the first slot beyond the base
            int nextThreshold = int.Parse(SkillConfig.hostConfig["Hand Slots Initial Cost"]); ;
            int increment = int.Parse(SkillConfig.hostConfig["Hand Slots Initial Cost"]) + int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]);  // Increment for the next threshold

            // Loop to find the next threshold greater than the current level
            while (currentLevel >= nextThreshold)
            {
                nextThreshold += increment;
                increment += int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]);
            }

            int Slots = (increment - (int.Parse(SkillConfig.hostConfig["Hand Slots Initial Cost"]) + int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]))) / int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]);

            return Slots;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetShortName()
        {
            return _shortName;
        }

        public string GetAttribute()
        {
            return _attribute;
        }

        public string GetDescription()
        {
            return _description;
        }

        public UpgradeType GetUpgradeType()
        {
            return _upgradeType;
        }

        public int GetCost()
        {
            return _cost;
        }

        public int GetMaxLevel()
        {
            return _maxLevel;
        }

        public int GetLevel()
        {
            return _level;
        }

        public float GetMultiplier()
        {
            return _multiplier;
        }

        public float GetTrueValue()
        {
            return _multiplier * _level;
        }

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void AddLevel(int level)
        {
            _level += level;
            int newLevel = _level;

            // Update the description when the skill level changes
            UpdateSkillDescription();

            _callback?.Invoke(level, newLevel);
        }
    }
}
