namespace Core.Slots
{
    public class SlotChange
    {
        public SlotChange(int id, InventoryItemType itemType, int previousCount, int newCount)
        {
            Id = id;
            ItemType = itemType;
            PreviousCount = previousCount;
            NewCount = newCount;
        }

        public int Id { get; }
        public InventoryItemType ItemType { get; }
        public int PreviousCount { get; }
        public int NewCount { get; }

        public int Delta => NewCount - PreviousCount;
    }
}