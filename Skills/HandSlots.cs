using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.UI;
using UnityEngine.InputSystem.Utilities;
using GameNetcodeStuff;
using LethalProgression.Config;

namespace LethalProgression.Skills
{
    internal class HandSlots
    {
        public static int currentSlotCount = 4;
        private static int nextSlotThreshold = 8;
        public static void HandSlotsUpdate(int updateValue, int newValue)
        {
            if (LethalPlugin.ReservedSlots)
                return;

            if (!LP_NetworkManager.xpInstance.skillList.IsSkillListValid())
                return;

            if (!LP_NetworkManager.xpInstance.skillList.IsSkillValid(UpgradeType.HandSlot))
                return;

            XP xpInstance = LP_NetworkManager.xpInstance;

            // Calculate slotsToAdd with increasing denominator
            int level = (int)xpInstance.skillList.skills[UpgradeType.HandSlot].GetTrueValue();
            int addedSlots = Slot(level);
            int slotCount = 4 + addedSlots;

            GameObject inventory = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/Inventory");
            List<string> slotIgnore = new List<string>() { "Slot0", "Slot1", "Slot2", "Slot3" };
            for (int i = 0; i < inventory.transform.childCount; i++)
            {
                Transform child = inventory.transform.GetChild(i);
                if (slotIgnore.Contains(child.gameObject.name))
                    continue;

                Object.Destroy(child.gameObject);
            }

            // Prepare the arrays
            Image[] ItemSlotIconFrames = new Image[slotCount];
            ItemSlotIconFrames[0] = HUDManager.Instance.itemSlotIconFrames[0];
            ItemSlotIconFrames[1] = HUDManager.Instance.itemSlotIconFrames[1];
            ItemSlotIconFrames[2] = HUDManager.Instance.itemSlotIconFrames[2];
            ItemSlotIconFrames[3] = HUDManager.Instance.itemSlotIconFrames[3];

            Image[] ItemSlotIcons = new Image[slotCount];
            ItemSlotIcons[0] = HUDManager.Instance.itemSlotIcons[0];
            ItemSlotIcons[1] = HUDManager.Instance.itemSlotIcons[1];
            ItemSlotIcons[2] = HUDManager.Instance.itemSlotIcons[2];
            ItemSlotIcons[3] = HUDManager.Instance.itemSlotIcons[3];

            GameObject Slot3 = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/Inventory/Slot3");
            GameObject Slot4 = xpInstance.guiObj.templateSlot;
            GameObject CurrentSlot = Slot3;
            currentSlotCount = slotCount;

            // Spawn more UI slots.
            for (int i = 0; i < (int)addedSlots; i++)
            {
                GameObject NewSlot = Object.Instantiate(Slot4);

                // This might break if someone else makes slots with these names in their mod..
                NewSlot.name = $"Slot{3 + (i + 1)}";

                NewSlot.transform.SetParent(inventory.transform);

                // Change locations.
                Vector3 localPosition = CurrentSlot.transform.localPosition;
                NewSlot.transform.SetLocalPositionAndRotation(
                new Vector3(localPosition.x, localPosition.y, localPosition.z),
                CurrentSlot.transform.localRotation);
                CurrentSlot = NewSlot;

                ItemSlotIconFrames[3 + (i + 1)] = NewSlot.GetComponent<Image>();
                ItemSlotIcons[3 + (i + 1)] = NewSlot.transform.GetChild(0).GetComponent<Image>();
                NewSlot.SetActive(true);
            }

            int totalSlots = ItemSlotIconFrames.Length;
            float slotWidth, slotHeight;
            Vector3[] rowPositions;
            int slotsPerRow;

            int numberOfRows = Mathf.Clamp(Mathf.CeilToInt(Mathf.Sqrt(totalSlots / 4f)), 2, 8);
            slotWidth = 78 / numberOfRows; // Adjust as necessary
            slotHeight = slotWidth;
            rowPositions = new Vector3[numberOfRows];
            slotsPerRow = 4 * numberOfRows;

            // Calculate positions for each row
            for (int row = 0; row < numberOfRows; row++)
            {
                rowPositions[row] = new Vector3(-185, -row * slotHeight, 0);
            }

            for (int i = 0; i < totalSlots; i++)
            {
                int row = i / slotsPerRow;
                int positionInRow = i % slotsPerRow;

                Vector3 newPosition = rowPositions[row] + new Vector3(positionInRow * slotWidth, 0, 0);

                ItemSlotIconFrames[i].rectTransform.sizeDelta = new Vector2(slotWidth, slotHeight);
                ItemSlotIconFrames[i].transform.localPosition = newPosition;
            }

            HUDManager.Instance.itemSlotIconFrames = ItemSlotIconFrames;
            HUDManager.Instance.itemSlotIcons = ItemSlotIcons;

            // Update the server about the new hand slot count
            ulong playerID = GameNetworkManager.Instance.localPlayerController.playerClientId;
            xpInstance.ServerHandSlots_ServerRpc(playerID, addedSlots);

            static int Slot(int level)
            {
                // Starting point for the first slot beyond the base
                int nextThreshold = int.Parse(SkillConfig.hostConfig["Hand Slots Initial Cost"]); ;
                int increment = int.Parse(SkillConfig.hostConfig["Hand Slots Initial Cost"]) + int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]);  // Increment for the next threshold

                // Loop to find the next threshold greater than the current level
                while (level >= nextThreshold)
                {
                    nextThreshold += increment;
                    increment += int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]);
                }

                int Slots = (increment - (int.Parse(SkillConfig.hostConfig["Hand Slots Initial Cost"]) + int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]))) / int.Parse(SkillConfig.hostConfig["Hand Slots Increment"]);

                return Slots;
            }
        }

    }
}
