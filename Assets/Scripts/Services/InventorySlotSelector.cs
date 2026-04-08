using System.Collections.Generic;
using Core;
using Infrastructure.StaticData;
using Inventories;

namespace Services
{
    public class InventorySlotSelector
    {
        private readonly InventoryData _inventoryData;
        private readonly IStaticDataService _staticDataService;

        public InventorySlotSelector(
            InventoryData inventoryData,
            IStaticDataService staticDataService)
        {
            _inventoryData = inventoryData;
            _staticDataService = staticDataService;
        }

        public InventorySlotData FindFirstSlotByItemType(InventoryItemType itemType)
        {
            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (!slot.IsUnlocked)
                    continue;

                if (!HasItems(slot))
                    continue;

                if (slot.ItemStack.Type != itemType)
                    continue;

                return slot;
            }

            return null;
        }

        public List<InventorySlotData> FindSlotsByKind(ItemKind kind)
        {
            List<InventorySlotData> result = new();

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (IsBusyUnlockedSlot(slot) == false)
                    continue;

                if (!_staticDataService.IsItemOfKind(slot.ItemStack.Type, kind))
                    continue;

                result.Add(slot);
            }

            return result;
        }

        public List<InventorySlotData> FindBusySlots()
        {
            List<InventorySlotData> result = new();

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (IsBusyUnlockedSlot(slot) == false)
                    continue;

                result.Add(slot);
            }

            return result;
        }

        public bool TryFindNotFullStack(
            InventoryItemType itemType,
            int maxStack,
            out InventorySlotData result)
        {
            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (IsBusyUnlockedSlot(slot) == false)
                    continue;

                if (slot.ItemStack.Type != itemType)
                    continue;

                if (slot.ItemStack.Count >= maxStack)
                    continue;

                result = slot;
                return true;
            }

            result = null;
            return false;
        }

        public bool TryFindEmptyUnlockedSlot(out InventorySlotData result)
        {
            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (!IsEmptyUnlockedSlot(slot))
                    continue;

                result = slot;
                return true;
            }

            result = null;
            return false;
        }

        private bool IsBusyUnlockedSlot(InventorySlotData slot) =>
            slot.IsUnlocked && slot.ItemStack != null;

        private bool HasItems(InventorySlotData slot) =>
            slot.ItemStack != null && slot.ItemStack.Count > 0;

        private bool IsEmptyUnlockedSlot(InventorySlotData slot) =>
            slot.IsUnlocked && slot.ItemStack == null;
    }
}