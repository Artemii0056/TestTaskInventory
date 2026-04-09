using System.Collections.Generic;
using Core.Inventory;

namespace Services.InventoryUnlockServices
{
    public interface IInventoryUnlockService
    {
        bool TryUnlockSlot(IReadOnlyList<InventorySlotData> slots, int slotId);
    }
}
