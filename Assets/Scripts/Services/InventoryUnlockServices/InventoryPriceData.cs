using System;
using System.Collections.Generic;
using Core;

namespace Services.InventoryUnlockServices
{
    public class InventoryPriceData
    {
        private readonly Dictionary<int, int> _lockedPrices;

        public InventoryPriceData(InventoryConfig inventoryConfig)
        {
            _lockedPrices = new Dictionary<int, int>();
            
            foreach (var slot in inventoryConfig.LockedPrices) 
                _lockedPrices[slot.Id] = slot.Price;
        }

        public int GetPrice(int slotId)
        {
            slotId -= 1; //TODO Важная фигня!
            
            if (_lockedPrices.TryGetValue(slotId, out int price))
                return price;

            throw new ArgumentException($"Price for slot {slotId} not found");
        }
    }
}