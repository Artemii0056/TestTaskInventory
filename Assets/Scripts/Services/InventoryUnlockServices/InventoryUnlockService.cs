using Core.Inventory;
using Core.Wallets;

namespace Services.InventoryUnlockServices
{
    public class InventoryUnlockService : IInventoryUnlockService
    {
        private readonly InventoryData _inventoryData;
        private readonly IInventoryPriceData _inventoryPriceData;
        private readonly IWallet _wallet;

        public InventoryUnlockService(
            InventoryData inventoryData,
            IWallet wallet,
            IInventoryPriceData inventoryPriceData)
        {
            _inventoryData = inventoryData;
            _wallet = wallet;
            _inventoryPriceData = inventoryPriceData;
        }

        public bool TryUnlockSlot(int slotId)
        {
            int slotIndex = GetSlotIndexById(slotId);

            if (slotIndex < 0)
                return false;

            InventorySlotData slot = _inventoryData.Slots[slotIndex];

            if (slot.IsUnlocked)
                return false;

            if (IsPreviousSlotLocked(slotIndex))
                return false;

            int price = _inventoryPriceData.GetPrice(slot.Id);

            if (_wallet.TrySpend(price) == false)
                return false;

            slot.Unlock();
            return true;
        }

        private int GetSlotIndexById(int slotId)
        {
            for (int i = 0; i < _inventoryData.Slots.Count; i++)
            {
                if (_inventoryData.Slots[i].Id == slotId)
                    return i;
            }

            return -1;
        }

        private bool IsPreviousSlotLocked(int slotIndex)
        {
            if (slotIndex == 0)
                return false;

            return _inventoryData.Slots[slotIndex - 1].IsUnlocked == false;
        }
    }

    public interface IInventoryUnlockService
    {
        bool TryUnlockSlot(int slotId);
    }
}