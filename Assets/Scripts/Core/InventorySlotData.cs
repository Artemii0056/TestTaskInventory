using System;

namespace Core
{
    public class InventorySlotData
    {
        public InventorySlotData(int id, bool isUnlocked)
        {
            Id = id;
            IsUnlocked = isUnlocked;
        }

        public int Id { get; private set; }
        public bool IsUnlocked { get; private set; }
        public ItemStack ItemStack { get; private set; }
        
        public bool IsEmpty => ItemStack == null;
        public bool HasItem => ItemStack != null;
        
        public void SetItem(ItemStack itemStack)
        {
            if (IsUnlocked == false)
                throw new InvalidOperationException("Slot is locked");
        
            if (itemStack == null)
                throw new ArgumentNullException(nameof(itemStack));
        
            if (IsEmpty == false)
                throw new InvalidOperationException("Slot already occupied");
        
            ItemStack = itemStack;
        }
        
        public void Unlock()
        {
            if (IsUnlocked)
                return;

            IsUnlocked = true;
        }
        
        public void RemoveAmount(int amount)
        {
            if (IsEmpty)
                throw new InvalidOperationException("Slot is empty");

            ItemStack.Decrease(amount);

            if (ItemStack.Count == 0)
                ItemStack = null;
        }
        
        public void Clear()
        {
            ItemStack = null;
        }
    }
}