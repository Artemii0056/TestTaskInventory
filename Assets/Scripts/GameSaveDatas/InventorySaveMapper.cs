using System.Collections.Generic;
using Core;
using Core.Inventory;
using GameSaveDatas;

namespace Infrastructure.SaveLoad
{
    public class InventorySaveMapper : IInventorySaveMapper
    {
        public InventorySaveData ToSaveData(InventoryData inventoryData)
        {
            var saveData = new InventorySaveData();

            foreach (InventorySlotData slot in inventoryData.Slots)
            {
                var slotSaveData = new InventorySlotSaveData
                {
                    Id = slot.Id,
                    IsUnlocked = slot.IsUnlocked,
                    HasItem = slot.HasItem,
                    ItemType = slot.HasItem ? slot.ItemStack.Type : InventoryItemType.None,
                    Count = slot.HasItem ? slot.ItemStack.Count : 0
                };

                saveData.Slots.Add(slotSaveData);
            }

            return saveData;
        }

        public InventoryData ToRuntimeData(InventorySaveData saveData)
        {
            var slots = new List<InventorySlotData>();

            foreach (InventorySlotSaveData slotSaveData in saveData.Slots)
            {
                var slot = new InventorySlotData(slotSaveData.Id, slotSaveData.IsUnlocked);

                if (slotSaveData.HasItem)
                {
                    var itemStack = new ItemStack(slotSaveData.ItemType, slotSaveData.Count);
                    slot.SetItem(itemStack);
                }

                slots.Add(slot);
            }

            return new InventoryData(slots);
        }
    }
}