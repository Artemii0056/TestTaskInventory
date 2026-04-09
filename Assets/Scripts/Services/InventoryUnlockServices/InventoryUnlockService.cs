using System.Collections.Generic;
using Core.Inventory;
using Core.Wallets;

namespace Services.InventoryUnlockServices
{
    public class InventoryUnlockService : IInventoryUnlockService
    {
        private readonly IInventoryPriceData _inventoryPriceData;
        private readonly IWallet _wallet;

        public InventoryUnlockService(
            IWallet wallet,
            IInventoryPriceData inventoryPriceData)
        {
            _wallet = wallet;
            _inventoryPriceData = inventoryPriceData;
        }

        public bool TryUnlockSlot(IReadOnlyList<InventorySlotData> slots, int slotId)
        {
            int slotIndex = GetSlotIndexById(slots, slotId);

            if (slotIndex < 0)
                return false;

            InventorySlotData slot = slots[slotIndex];

            if (slot.IsUnlocked)
                return false;

            if (IsPreviousSlotLocked(slots, slotIndex))
                return false;

            int price = _inventoryPriceData.GetPrice(slot.Id);

            if (_wallet.TrySpend(price) == false)
                return false;

            slot.Unlock();
            return true;
        }

        private int GetSlotIndexById(IReadOnlyList<InventorySlotData> slots, int slotId)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].Id == slotId)
                    return i;
            }

            return -1;
        }

        private bool IsPreviousSlotLocked(IReadOnlyList<InventorySlotData> slots, int slotIndex)
        {
            if (slotIndex == 0)
                return false;

            return slots[slotIndex - 1].IsUnlocked == false;
        }
    }
}