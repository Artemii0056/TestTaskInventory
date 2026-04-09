using Core.Inventory;

namespace Services.InventoryUnlockServices
{
    public interface IInventoryUnlockService
    {
        bool TryUnlockSlot(InventoryData inventoryData, int slotId);
    }
}