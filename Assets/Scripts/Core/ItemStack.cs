using Inventories;

namespace Core
{
    public class ItemStack
    {
        public InventoryItemType InventoryItemType { get; private set; }
        public int Count { get; private set; }
    }
}