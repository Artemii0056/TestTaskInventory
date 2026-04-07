using Inventories;

namespace Core
{
    public class ItemStack //структура?
    {
        public ItemStack(InventoryItemType inventoryItemType, int count)
        {
            InventoryItemType = inventoryItemType;
            Count = count;
        }

        public InventoryItemType InventoryItemType { get; private set; }
        public int Count { get; private set; }
    }
}