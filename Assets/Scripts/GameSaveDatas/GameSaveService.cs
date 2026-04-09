using Core.Inventory;
using GameSaveDatas;

namespace Infrastructure.SaveLoad
{
    public class GameSaveService : IGameSaveService
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IInventorySaveMapper _inventorySaveMapper;

        public GameSaveService(
            ISaveLoadService saveLoadService,
            IInventorySaveMapper inventorySaveMapper)
        {
            _saveLoadService = saveLoadService;
            _inventorySaveMapper = inventorySaveMapper;
        }

        public bool HasSave()
        {
            return _saveLoadService.HasSave();
        }

        public InventoryData LoadInventory()
        {
            if (_saveLoadService.HasSave() == false)
                return null;

            GameSaveData gameSaveData = _saveLoadService.Load();

            if (gameSaveData == null || gameSaveData.Inventory == null)
                return null;

            return _inventorySaveMapper.ToRuntimeData(gameSaveData.Inventory);
        }

        public void SaveInventory(InventoryData inventoryData)
        {
            var gameSaveData = new GameSaveData
            {
                Inventory = _inventorySaveMapper.ToSaveData(inventoryData)
            };

            _saveLoadService.Save(gameSaveData);
        }

        public InventoryData LoadInventoryOrDefault(InventoryData defaultInventoryData)
        {
            InventoryData inventoryData = LoadInventory();

            return inventoryData ?? defaultInventoryData;
        }
    }
}