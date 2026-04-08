using System;
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

            for (int i = 0; i < _inventoryData.Slots.Count; i++)
            {
                InventorySlotData slot = _inventoryData.Slots.ToList()[i]; //TODO

                if (slot.IsLocked)
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