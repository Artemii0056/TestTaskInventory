namespace Core
{
    public class InventorySlotData
    {
        public int Id { get; private set; }
        public bool IsLocked { get; private set; }
        public ItemStack ItemStack { get; private set; }

        public void SetItem(ItemStack itemStack)
        {
            
        }
    }
}