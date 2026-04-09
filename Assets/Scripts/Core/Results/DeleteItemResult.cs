namespace Core.Results
{
    public class DeleteItemResult
    {
        public DeleteItemResult(int count, InventoryItemType itemType, int slotId, bool isSuccess)
        {
            Count = count;
            ItemType = itemType;
            SlotId = slotId;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
        public int SlotId { get; private set; }
        public int Count { get; private set; }
        public InventoryItemType ItemType { get; private set; }
    }
}