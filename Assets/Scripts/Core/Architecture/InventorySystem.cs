using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Architecture
{
    public class InventorySystem
    {
        private readonly InventoryData _inventoryData;

        public InventorySystem(InventoryData inventoryData)
        {
            _inventoryData = inventoryData;
        }
        
        public bool TryAddItem(ItemStack itemStack, out int slotId)
        {
            slotId = -1;
            
            List<InventorySlotData> slots = _inventoryData.Slots.ToList();
            int count = slots.Count;

            for (int i = 0; i < count; i++)
            {
                InventorySlotData slot = slots[i]; 

                if (slot.IsUnlocked)
                    continue;

                if (slot.ItemStack != null)
                    continue;

                slot.SetItem(itemStack);
                slotId = i;
                return true;
            }

            return false;
        }
        
        public bool TryRemoveItem(ItemStack itemStack)
        {
            throw new Exception();
        }
    }
}