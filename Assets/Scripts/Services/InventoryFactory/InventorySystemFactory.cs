using Core.Inventory;
using Core.Systems;
using Core.Wallets;
using Infrastructure.StaticData;
using Services.InventoryUnlockServices;
using Services.RandomServices;

namespace Services.InventoryFactory
{
    public class InventorySystemFactory : IInventorySystemFactory //TODO Не используется? 
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;
        private readonly IWallet _wallet;
        private readonly IInventoryUnlockService _inventoryUnlockService;
        
        public InventorySystemFactory(
            IStaticDataService staticDataService, 
            IRandomService randomService, 
            IWallet wallet, IInventoryUnlockService inventoryUnlockService)
        {
            _staticDataService = staticDataService;
            _randomService = randomService;
            _wallet = wallet;
            _inventoryUnlockService = inventoryUnlockService;
        }

        public InventorySystem Create(InventoryData data)
        {
            return new InventorySystem( _staticDataService, _randomService, _wallet, _inventoryUnlockService);
        }
    }
}