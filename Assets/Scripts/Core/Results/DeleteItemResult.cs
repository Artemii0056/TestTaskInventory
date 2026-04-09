namespace Core.Results
{
    public class DeleteItemResult
    {
        public DeleteItemResult(int count, InventoryItemType itemType, int slotId, bool success)
        {
            Count = count;
            ItemType = itemType;
            SlotId = slotId;
            Success = success;
        }

        public bool Success { get; }

        public int SlotId { get; private set; }
        public int Count { get; private set; }
        public InventoryItemType ItemType { get; private set; }
    }
}