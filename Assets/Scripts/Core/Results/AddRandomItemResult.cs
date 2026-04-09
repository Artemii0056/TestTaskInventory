namespace Core.Results
{
    public readonly struct AddRandomItemResult
    {
        public AddRandomItemResult(bool isSuccess, InventoryItemType itemType, int slotId)
        {
            IsSuccess = isSuccess;
            ItemType = itemType;
            SlotId = slotId;
        }

        public bool IsSuccess { get; }
        public InventoryItemType ItemType { get; }
        public int SlotId { get; }
    }
}