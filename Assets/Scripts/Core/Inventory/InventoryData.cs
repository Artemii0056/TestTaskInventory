using System.Collections.Generic;

namespace Core.Inventory
{
    public class InventoryData
    {
        private List<InventorySlotData> _slots;

        public InventoryData(List<InventorySlotData> slots)
        {
            _slots = slots;
        }

        public IReadOnlyList<InventorySlotData> Slots => _slots;

        public void AddSlot(InventorySlotData slot)
        {
            _slots.Add(slot);   
        }
    }
}