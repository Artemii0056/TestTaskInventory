using System;
using System.Collections.Generic;
using Infrastructure.StaticData;

namespace Services.InventoryUnlockServices
{
    public class InventoryPriceData : IInventoryPriceData
    {
        private readonly Dictionary<int, int> _lockedPrices;

        public InventoryPriceData(IStaticDataService staticDataService)
        {
            _lockedPrices = new Dictionary<int, int>();
            
            foreach (var slot in staticDataService.InventoryConfig.LockedPrices) 
                _lockedPrices[slot.Id] = slot.Price;
        }

        public int GetPrice(int slotId)
        {
            slotId -= 1; 
            
            if (_lockedPrices.TryGetValue(slotId, out int price))
                return price;

            throw new ArgumentException($"Price for slot {slotId} not found");
        }
    }
}