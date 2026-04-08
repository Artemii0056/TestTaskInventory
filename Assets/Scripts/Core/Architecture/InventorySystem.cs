using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Infrastructure.StaticData;
using Inventories;
using Inventories.Configs.Ammo;
using Inventories.Configs.Ammo.AmmoFactories;
using Inventories.Configs.Weapons;
using Results;
using Results.DefaultNamespace.Results;
using UnityEngine;

namespace Core.Architecture
{
    public class InventorySystem
    {
        private readonly InventoryData _inventoryData;
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;

        public InventorySystem(
            InventoryData inventoryData,
            IStaticDataService staticDataService, IRandomService randomService)
        {
            _inventoryData = inventoryData;
            _staticDataService = staticDataService;
            _randomService = randomService;
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
                    changes.Add(new SlotChange(slotWithAmmo.Id, ammoType, startCount, slotWithAmmo.ItemStack.Count));
                    continue;
                }

                if (TryFindFirstEmptyUnlockedSlot(out InventorySlotData emptyUnlockedSlot) == false)
                    break;

                int toAddToNewStack = Math.Min(maxStack, remainingToAdd);
                remainingToAdd -= toAddToNewStack;
                emptyUnlockedSlot.SetItem(new ItemStack(ammoType, toAddToNewStack));
                changes.Add(new SlotChange(emptyUnlockedSlot.Id, ammoType, 0, emptyUnlockedSlot.ItemStack.Count));
            }

            int addedAmount = requestedAmount - remainingToAdd;

            return new AddAmmoResult(requestedAmount, addedAmount, remainingToAdd, changes);
        }

        private bool TryFindFirstEmptyUnlockedSlot(out InventorySlotData data)
        {
            data = null;

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (!slot.IsUnlocked)
                    continue;

                if (slot.ItemStack != null)
                    continue;

                data = slot;
                return true;
            }

            return false;
        }

        private bool TryFindFirstNotFullStack(InventoryItemType type, int maxStack, out InventorySlotData slotToFind)
        {
            slotToFind = null;

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (slot.IsUnlocked == false)
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

        private bool TryFindBusySlots(out List<InventorySlotData> slots)
        {
            slots = new List<InventorySlotData>();

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (slot.HasItem)
                    slots.Add(slot);
            }

            return slots.Any();
        }

        private bool TryFindWeapons(out List<InventorySlotData> slots)
        {
            List<InventoryItemType> itemTypes = new List<InventoryItemType>();

            foreach (var config in _staticDataService.GetWeaponConfigs())
            {
                itemTypes.Add(config.InventoryItemData.Type);
            }

            slots = new List<InventorySlotData>();

            foreach (InventorySlotData slot in
                     _inventoryData.Slots) //В идеале, тут мы должны собрать именно только пушки
            {
                if (slot.ItemStack == null)
                    continue;

                foreach (var type in itemTypes)
                {
                    if (slot.ItemStack.Type == type)
                    {
                        slots.Add(slot);
                        break;
                    }
                }
            }

            return slots.Any();
        }

        public bool TryDeleteItem(out DeleteItemResult result)
            //TODO Хочу добавить ячейке состояние - Занята/Свободно/Заблокирована
        {
            result = null;

            if (TryFindBusySlots(out List<InventorySlotData> slotDatas) == false)
                return false;

            var slot = slotDatas[_randomService.GenerateValue(slotDatas.Count)];

            result = new DeleteItemResult(slot.ItemStack.Count, slot.ItemStack.Type, slot.Id);
            slot.Clear();

            return true;
        }

        public bool TryAddItem(ItemStack itemStack, out InventorySlotData slotChanged)
        {
            slotChanged = null;

            if (TryFindFirstEmptyUnlockedSlot(out InventorySlotData slot) == false)
                return false;

            slot.SetItem(new ItemStack(itemStack.Type, itemStack.Count));
            slotChanged = slot;
            return true;
        }

        private bool TryFindSlotsWithAmmo(out List<InventorySlotData> slots) 
            //В идеале, тут мы должны собрать именно только патроны
        {
            slots = new List<InventorySlotData>();

            List<InventoryItemType> itemTypes = new List<InventoryItemType>();

            foreach (var config in _staticDataService.GetAmmoConfigs())
                itemTypes.Add(config.InventoryItemData.Type);

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (slot.ItemStack == null)
                    continue;

                foreach (var type in itemTypes)
                {
                    if (slot.ItemStack.Type == type)
                    {
                        slots.Add(slot);
                        break;
                    }
                }
            }

            return slots.Any();
        }
        
        public ShootResult TryShootRandomWeapon()
        {
            List<InventorySlotData> weapons = FindWeapons();

            if (weapons.Count == 0)
                return new ShootResult(false, InventoryItemType.None, InventoryItemType.None, 0);

            InventorySlotData weaponSlot = weapons[_randomService.GenerateValue(weapons.Count)];
            InventoryItemType weaponType = weaponSlot.ItemStack.Type;

            WeaponConfig weaponConfig = _staticDataService.GetWeaponConfigByType(weaponType);
            InventoryItemType ammoType = GetAmmoItemType(weaponConfig.AmmoType);

            InventorySlotData ammoSlot = FindFirstAmmoSlot(ammoType);

            if (ammoSlot == null)
                return new ShootResult(false, weaponType, ammoType, weaponConfig.Damage);

            DecreaseAmmo(ammoSlot);

            return new ShootResult(
                true,
                weaponType,
                ammoType,
                weaponConfig.Damage,
                ammoSlot.Id);
        }
        
        private List<InventorySlotData> FindWeapons()
        {
            List<InventorySlotData> result = new();

            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (!slot.IsUnlocked)
                    continue;

                if (slot.ItemStack == null)
                    continue;

                if (!_staticDataService.IsItemOfKind(slot.ItemStack.Type, ItemKind.Weapon))
                    continue;

                result.Add(slot);
            }

            return result;
        }
        
        private InventorySlotData FindFirstAmmoSlot(InventoryItemType ammoType)
        {
            foreach (InventorySlotData slot in _inventoryData.Slots)
            {
                if (!slot.IsUnlocked)
                    continue;

                if (slot.ItemStack == null)
                    continue;

                if (slot.ItemStack.Type != ammoType)
                    continue;

                if (slot.ItemStack.Count <= 0)
                    continue;

                return slot;
            }

            return null;
        }
        
        private InventoryItemType GetAmmoItemType(AmmoType ammoType)
        {
            foreach (AmmoConfig config in _staticDataService.GetAmmoConfigs())
            {
                if (config.AmmoType == ammoType)
                    return config.InventoryItemData.Type;
            }

            return InventoryItemType.None;
        }
        
        private void DecreaseAmmo(InventorySlotData ammoSlot)
        {
            int newCount = ammoSlot.ItemStack.Count - 1;

            if (newCount <= 0)
            {
                ammoSlot.Clear();
                return;
            }

            ammoSlot.ItemStack.Decrease(1);
        }
    }
}