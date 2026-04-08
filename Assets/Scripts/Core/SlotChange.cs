namespace Core
{
    public class SlotChange
    {
        public SlotChange(int slotId, InventoryItemType itemType, int previousCount, int newCount)
        {
            SlotId = slotId;
            ItemType = itemType;
            PreviousCount = previousCount;
            NewCount = newCount;
        }

        public int SlotId { get; }
        public InventoryItemType ItemType { get; }
        public int PreviousCount { get; }
        public int NewCount { get; }

        public int Delta => NewCount - PreviousCount;
    }
}