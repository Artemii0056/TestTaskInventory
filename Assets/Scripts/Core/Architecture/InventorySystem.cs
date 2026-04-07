using System;

namespace Core.Architecture
{
    public class InventorySystem
    {
        private InventoryData _inventoryData;

        public InventorySystem(InventoryData inventoryData)
        {
            _inventoryData = inventoryData;
        }

        public bool TryAddItem(ItemStack itemStack)
        {
            throw new Exception();
        }
        
        public bool TryRemoveItem(ItemStack itemStack)
        {
            throw new Exception();
        }
    }
}