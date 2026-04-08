using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData;
using UnityEngine.UI;

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

        public bool TryAddAmmo(ItemStack stack) //получить тут размер из сервиса 
        {
            var type = _staticDataService.GetAmmoConfigByType(stack.Type);


            return false;
        }

        public bool TryAddItem(ItemStack itemStack, out int slotId)
        {
            slotId = -1;

            List<InventorySlotData> slots = _inventoryData.Slots.ToList();
            int count = slots.Count;

            for (int i = 0; i < count; i++)
            {
                InventorySlotData slot = slots[i];

                if (slot.IsUnlocked == false)
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