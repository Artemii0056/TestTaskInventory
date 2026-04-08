using System;
using System.Collections.Generic;
using DefaultNamespace.Results;
using Infrastructure.StaticData;
using Inventories;

namespace Core.Architecture
{
    public class InventorySystem
    {
        private readonly InventoryData _inventoryData;
        private readonly IStaticDataService _staticDataService;

        public InventorySystem(
            InventoryData inventoryData,
            IStaticDataService staticDataService)
        {
            _inventoryData = inventoryData;
            _staticDataService = staticDataService;
        }

        public IReadOnlyList<InventorySlotData> Slots => _inventoryData.Slots;

        public AddAmmoResult AddAmmo(InventoryItemType ammoType, int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            int maxStack = _staticDataService
                .GetAmmoConfigByType(ammoType)
                .InventoryItemData.MaxStack;
            
            int requestedAmount = amount;

            int remainingToAdd = amount;
            List<SlotChange> changes = new List<SlotChange>();

            while (remainingToAdd > 0)
            {
                if (TryFindFirstNotFullStack(ammoType, maxStack, out InventorySlotData slotWithAmmo))
                {
                    int startCount = slotWithAmmo.ItemStack.Count;
                    int freeSpace = maxStack - startCount;
                    int toAdd = Math.Min(freeSpace, remainingToAdd);

                    remainingToAdd -= toAdd;
                    slotWithAmmo.ItemStack.Increase(toAdd);
                    changes.Add(new SlotChange(slotWithAmmo.Id,ammoType, startCount, slotWithAmmo.ItemStack.Count ));
                    continue;
                }

                InventorySlotData emptyUnlockedSlot = FindFirstEmptyUnlockedSlot();

                if (emptyUnlockedSlot == null)
                    break;

                int toAddToNewStack = Math.Min(maxStack, remainingToAdd);
                remainingToAdd -= toAddToNewStack;
                emptyUnlockedSlot.SetItem(new ItemStack(ammoType, toAddToNewStack));
                changes.Add(new SlotChange(emptyUnlockedSlot.Id,ammoType, 0, slotWithAmmo.ItemStack.Count ));
            }
            
            int addedAmount = requestedAmount - remainingToAdd;

            return new AddAmmoResult(requestedAmount, addedAmount, remainingToAdd, changes);
        }

        private InventorySlotData FindFirstEmptyUnlockedSlot()
        {
            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (!slot.IsUnlocked)
                    continue;

                if (slot.ItemStack != null)
                    continue;

                return slot;
            }

            return null;
        }

        private bool TryFindFirstNotFullStack(InventoryItemType type, int maxStack, out InventorySlotData slotToFind)
        {
            slotToFind = null;

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (!slot.IsUnlocked)
                    continue;

                if (slot.ItemStack == null)
                    continue;

                if (slot.ItemStack.Type != type)
                    continue;

                if (slot.ItemStack.Count >= maxStack)
                    continue;

                slotToFind = slot;
                return true;
            }

            return false;
        }

        public bool TryAddItem(ItemStack itemStack, out int slotId)
        {
            slotId = -1;

            for (int i = 0; i < _inventoryData.Slots.Count; i++)
            {
                InventorySlotData slot = _inventoryData.Slots[i];

                if (!slot.IsUnlocked)
                    continue;

                if (slot.ItemStack != null)
                    continue;

                slot.SetItem(itemStack);
                slotId = i;
                return true;
            }

            return false;
        }

        public bool TryRemoveItem(ItemStack itemStack)
        {
            throw new Exception();
        }
    }
}