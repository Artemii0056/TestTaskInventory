using System.Collections.Generic;
using Core;
using Core.Architecture;
using Infrastructure.StaticData;

namespace DefaultNamespace
{
    public class InventorySystemFactory : IInventoryFactory
    {
        private readonly IUniqueIdService _uniqueIdService;
        private readonly IStaticDataService _staticDataService;
        
        public InventorySystemFactory(IUniqueIdService uniqueIdService) => 
            _uniqueIdService = uniqueIdService;

        public InventorySystem Create(int allCount, int openedCount)
        {
            List<InventorySlotData> slots = new List<InventorySlotData>();
            
            FillPart(openedCount, slots, false);
            
            int remainingCount = allCount - openedCount;
            
            FillPart(remainingCount, slots, true);
            
            InventoryData data = new InventoryData(slots);
            
            return new InventorySystem(data, _staticDataService);
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