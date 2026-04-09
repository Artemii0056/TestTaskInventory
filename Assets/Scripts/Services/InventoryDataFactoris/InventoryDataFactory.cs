using System.Collections.Generic;
using Core;
using Core.Inventory;
using Infrastructure.StaticData;
using Services.IdGenerator;

namespace Services.InventoryDataFactoris
{
    public class InventoryDataFactory : IInventoryDataFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IUniqueIdService _uniqueIdService;

        public InventoryDataFactory(IStaticDataService staticDataService,
            IUniqueIdService uniqueIdService)
        {
            _staticDataService = staticDataService;
            _uniqueIdService = uniqueIdService;
        }

        public InventoryData Create()
        {
            InventoryConfig config = _staticDataService.InventoryConfig;
            
            List<InventorySlotData> slots = new List<InventorySlotData>();
            
            FillPart(config.CountUnlockSlots, slots, true);
            
            int remainingCount = config.CountAllSlots - config.CountUnlockSlots;
            
            FillPart(remainingCount, slots, false);
            
            return new InventoryData(slots);
        }
        
        private void FillPart(int remainingCount, List<InventorySlotData> slots, bool status)
        {
            for (int i = 0; i < remainingCount; i++)
            {
                slots.Add(new InventorySlotData(_uniqueIdService.GetNextId(), status));
            }
        }
    }
}