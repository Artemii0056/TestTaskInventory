using Core.Inventory;
using GameSaveDatas;
using Services.InventoryDataFactoris;

namespace Services
{
    public class InventoryDataProvider : IInventoryDataProvider
    {
        private readonly IGameSaveService _gameSaveService;
        private readonly IInventoryDataFactory _inventoryDataFactory;
        private readonly IInventorySaveMapper _inventorySaveMapper;

        public InventoryDataProvider(
            IGameSaveService gameSaveService,
            IInventoryDataFactory inventoryDataFactory,
            IInventorySaveMapper inventorySaveMapper)
        {
            _gameSaveService = gameSaveService;
            _inventoryDataFactory = inventoryDataFactory;
            _inventorySaveMapper = inventorySaveMapper;
        }

        public InventoryData CreateOrLoad()
        {
            if (_gameSaveService.HasSave())
            {
                GameSaveData saveData = _gameSaveService.Load();

                if (saveData?.Inventory != null)
                    return _inventorySaveMapper.ToRuntimeData(saveData.Inventory);
            }

            return _inventoryDataFactory.Create();
        }
    }
}