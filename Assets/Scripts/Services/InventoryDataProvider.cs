using Core.Inventory;
using Infrastructure.SaveLoad;
using Services.InventoryDataFactoris;

namespace Services
{
    public class InventoryDataProvider : IInventoryDataProvider
    {
        private readonly IGameSaveService _gameSaveService;
        private readonly IInventoryDataFactory _inventoryDataFactory;

        public InventoryDataProvider(
            IGameSaveService gameSaveService,
            IInventoryDataFactory inventoryDataFactory)
        {
            _gameSaveService = gameSaveService;
            _inventoryDataFactory = inventoryDataFactory;
        }

        public InventoryData CreateOrLoad()
        {
            if (_gameSaveService.HasSave())
            {
                InventoryData inventoryData = _gameSaveService.LoadInventory();

                if (inventoryData != null)
                    return inventoryData;
            }

            return _inventoryDataFactory.Create();
        }
    }
}