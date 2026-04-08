using System;
using System.Collections.Generic;
using Infrastructure.StaticData;
using Inventories;
using Inventories.Configs;
using Inventories.Configs.Weapons;
using Results;
using Results.DefaultNamespace.Results;
using Services;
using Services.RandomServices;

namespace Core.Architecture
{
    public class InventorySystem
    {
        private readonly InventoryData _inventoryData;
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;
        private readonly InventorySlotSelector _inventorySlotSelector;

        public InventorySystem(
            InventoryData inventoryData,
            IStaticDataService staticDataService,
            IRandomService randomService)
        {
            _inventoryData = inventoryData;
            _staticDataService = staticDataService;
            _randomService = randomService;
            _inventorySlotSelector = new InventorySlotSelector(_inventoryData, _staticDataService);
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
            int remainingAmount = amount;
            List<SlotChange> changes = new();

            int toAdd;

            while (remainingAmount > 0)
            {
                if (_inventorySlotSelector.TryFindNotFullStack(ammoType, maxStack, out InventorySlotData slotWithAmmo))
                {
                    int startCount = slotWithAmmo.ItemStack.Count;
                    int freeSpace = maxStack - startCount;
                    toAdd = Math.Min(freeSpace, remainingAmount);

                    remainingAmount -= toAdd;
                    slotWithAmmo.ItemStack.Increase(toAdd);

                    changes.Add(new SlotChange(
                        slotWithAmmo.Id,
                        ammoType,
                        startCount,
                        slotWithAmmo.ItemStack.Count));

                    continue;
                }

                if (!_inventorySlotSelector.TryFindEmptyUnlockedSlot(out InventorySlotData emptySlot))
                    break;

                toAdd = Math.Min(maxStack, remainingAmount);
                remainingAmount -= toAdd;
                emptySlot.SetItem(new ItemStack(ammoType, toAdd));

                changes.Add(new SlotChange(
                    emptySlot.Id,
                    ammoType,
                    0,
                    emptySlot.ItemStack.Count));
            }

            int addedAmount = requestedAmount - remainingAmount;
            return new AddAmmoResult(requestedAmount, addedAmount, remainingAmount, changes);
        }

        public bool TryAddItem(ItemStack itemStack, out InventorySlotData changedSlot)
        {
            changedSlot = null;

            if (!_inventorySlotSelector.TryFindEmptyUnlockedSlot(out InventorySlotData slot))
                return false;

            slot.SetItem(new ItemStack(itemStack.Type, itemStack.Count));
            changedSlot = slot;
            return true;
        }

        public bool TryDeleteItem(out DeleteItemResult result)
        {
            result = null;

            List<InventorySlotData> busySlots = _inventorySlotSelector.FindBusySlots();

            if (busySlots.Count == 0)
                return false;

            InventorySlotData slot = busySlots[_randomService.GenerateValue(busySlots.Count)];

            result = new DeleteItemResult(
                slot.ItemStack.Count,
                slot.ItemStack.Type,
                slot.Id);

            slot.Clear();
            return true;
        }

        public ShootResult ShootRandomWeapon()
        {
            List<InventorySlotData> weapons = _inventorySlotSelector.FindSlotsByKind(ItemKind.Weapon);

            if (weapons.Count == 0)
                return new ShootResult(false, InventoryItemType.None, InventoryItemType.None, 0);

            InventorySlotData weaponSlot = weapons[_randomService.GenerateValue(weapons.Count)];
            InventoryItemType weaponType = weaponSlot.ItemStack.Type;

            WeaponConfig weaponConfig = _staticDataService.GetWeaponConfigByType(weaponType);
            InventoryItemType ammoType = _staticDataService.GetAmmoItemType(weaponConfig.AmmoType);

            InventorySlotData ammoSlot = _inventorySlotSelector.FindFirstSlotByItemType(ammoType);

            if (ammoSlot == null)
                return new ShootResult(false, weaponType, ammoType, weaponConfig.Damage);

            ConsumeOneAmmo(ammoSlot);

            return new ShootResult(
                true,
                weaponType,
                ammoType,
                weaponConfig.Damage,
                ammoSlot.Id);
        }

        public float GetTotalWeight()
        {
            float totalWeight = 0f;

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (!slot.IsUnlocked || slot.ItemStack == null)
                    continue;

                InventoryItemData itemData = _staticDataService.GetItemDataByType(slot.ItemStack.Type);
                totalWeight += itemData.Weight * slot.ItemStack.Count;
            }

            return totalWeight;
        }

        private void ConsumeOneAmmo(InventorySlotData ammoSlot)
        {
            if (ammoSlot.ItemStack.Count <= 1)
            {
                ammoSlot.Clear();
                return;
            }

            ammoSlot.ItemStack.Decrease(1);
        }
    }
}