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

        public bool TryUnlockSlot(InventoryData inventoryData, int slotId)
        {
            int slotIndex = GetSlotIndexById(inventoryData, slotId);

            if (slotIndex < 0)
                return false;

            InventorySlotData slot = inventoryData.Slots[slotIndex];

            if (slot.IsUnlocked)
                return false;

            if (IsPreviousSlotLocked(inventoryData, slotIndex))
                return false;

            int price = _inventoryPriceData.GetPrice(slot.Id);

            if (_wallet.TrySpend(price) == false)
                return false;

            slot.Unlock();
            return true;
        }

        private int GetSlotIndexById(InventoryData inventoryData, int slotId)
        {
            for (int i = 0; i < inventoryData.Slots.Count; i++)
            {
                if (inventoryData.Slots[i].Id == slotId)
                    return i;
            }

            return -1;
        }

        private bool IsPreviousSlotLocked(InventoryData inventoryData, int slotIndex)
        {
            if (slotIndex == 0)
                return false;

            return inventoryData.Slots[slotIndex - 1].IsUnlocked == false;
        }
    }
}