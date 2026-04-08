namespace Core
{
    public class SlotChange
    {
        public SlotChange(int slotId, int addedAmount)
        {
            SlotId = slotId;
            AddedAmount = addedAmount;
        }
        
        public int SlotId { get; }
        public int AddedAmount { get; }
    }
}