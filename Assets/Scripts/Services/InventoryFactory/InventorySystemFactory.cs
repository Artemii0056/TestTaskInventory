using System.Collections.Generic;
using Core;
using Core.Inventory;
using Core.Systems;
using Core.Wallets;
using Infrastructure.StaticData;
using Services.IdGenerator;
using Services.RandomServices;

namespace Services.InventoryFactory
{
    public class InventorySystemFactory : IInventoryFactory
    {
        private readonly IUniqueIdService _uniqueIdService;
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;
        private readonly IWallet _wallet;
        
        public InventorySystemFactory(IUniqueIdService uniqueIdService, IStaticDataService staticDataService, IRandomService randomService, IWallet wallet)
        {
            _uniqueIdService = uniqueIdService;
            _staticDataService = staticDataService;
            _randomService = randomService;
            _wallet = wallet;
        }

        public InventorySystem Create(int allCount, int openedCount)
        {
            List<InventorySlotData> slots = new List<InventorySlotData>();
            
            FillPart(openedCount, slots, true);
            
            int remainingCount = allCount - openedCount;
            
            FillPart(remainingCount, slots, false);
            
            InventoryData data = new InventoryData(slots);
            
            return new InventorySystem(data, _staticDataService, _randomService, _wallet);
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