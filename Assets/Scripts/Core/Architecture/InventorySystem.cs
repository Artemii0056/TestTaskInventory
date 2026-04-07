using System;
using UnityEngine;

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
            Debug.Log($"{itemStack.InventoryItemType}: {itemStack.Count}");
            return false;
        }
        
        public bool TryRemoveItem(ItemStack itemStack)
        {
            throw new Exception();
        }
    }
}