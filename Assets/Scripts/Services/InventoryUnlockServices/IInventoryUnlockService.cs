namespace Services.InventoryUnlockServices
{
    public interface IInventoryUnlockService
    {
        bool TryUnlockSlot(int slotId);
    }
}