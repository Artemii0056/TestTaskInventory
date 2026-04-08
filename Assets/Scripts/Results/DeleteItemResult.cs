using Inventories;

namespace Results
{
    public class DeleteItemResult
    {
        public DeleteItemResult(int count, InventoryItemType itemType, int slotId)
        {
            Count = count;
            ItemType = itemType;
            SlotId = slotId;
        }
        
        public int SlotId { get; private set; }
        public int Count { get; private set; }
        public InventoryItemType ItemType { get; private set; }
    }
}