using Core.Systems;
using Core.Wallets;
using GameSaveDatas;
using Infrastructure.SaveLoad;

namespace Services.SaveProgressServices
{
    public class SaveProgressService : ISaveProgressService
    {
        private readonly IGameSaveService _gameSaveService;
        private readonly IInventorySaveMapper _inventorySaveMapper;
        private readonly InventorySystem _inventorySystem;
        private readonly IWallet _wallet;

        public SaveProgressService(
            IGameSaveService gameSaveService,
            IInventorySaveMapper inventorySaveMapper,
            InventorySystem inventorySystem,
            IWallet wallet)
        {
            _gameSaveService = gameSaveService;
            _inventorySaveMapper = inventorySaveMapper;
            _inventorySystem = inventorySystem;
            _wallet = wallet;
        }

        public void Save()
        {
            _gameSaveService.Save(new GameSaveData
            {
                Coins = _wallet.Coins,
                Inventory = _inventorySaveMapper.ToSaveData(_inventorySystem.Data)
            });
        }
    }
}