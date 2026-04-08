using System;

namespace Core.Inventory
{
    public class ItemStack 
    {
        public ItemStack(InventoryItemType type, int count)
        {
            if (type == InventoryItemType.None)
                throw new ArgumentException("Invalid item type");

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            
            Type = type;
            Count = count;
        }

        public InventoryItemType Type { get; }
        public int Count { get; private set; }

        public void Increase(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Count += amount;
        }
        
        public void Decrease(int amount)
        {
            if (amount <= 0 || amount > Count)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Count -= amount;
        }

        public  bool IsSameType(ItemStack other)
        {
            return other != null && other.Type == Type;
        }
    }
}