using Core.Inventory;

namespace Infrastructure.SaveLoad
{
    public interface IGameSaveService
    {
        bool HasSave();
        InventoryData LoadInventory();
        void SaveInventory(InventoryData inventoryData);
        InventoryData LoadInventoryOrDefault(InventoryData defaultInventoryData);
    }
}