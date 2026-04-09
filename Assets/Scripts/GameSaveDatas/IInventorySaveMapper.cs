using Core.Inventory;

namespace GameSaveDatas
{
    public interface IInventorySaveMapper
    {
        InventorySaveData ToSaveData(InventoryData inventoryData);
        InventoryData ToRuntimeData(InventorySaveData saveData);
    }
}