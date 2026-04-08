using Core;
using Infrastructure.StaticData;
using UI.InventoryScreen.Views;
using UnityEngine;

namespace UI.Factories
{
    public class InventorySlotViewFactory
    {
        private InventorySlotView _prefab;
        private IStaticDataService _staticDataService;

        public InventorySlotViewFactory(InventorySlotView prefab, IStaticDataService staticDataService)
        {
            _prefab = prefab;
            _staticDataService = staticDataService;
        }

        public InventorySlotView Create(bool isUnlocked, InventoryItemType type = InventoryItemType.None, int count = 0,
            int price = 0)
        {
            InventorySlotView inventorySlotView = Object.Instantiate(_prefab);

            if (isUnlocked)
            {
                return FillOpenned(inventorySlotView, type, count);
            }
            else
            {
                return FillLocked(inventorySlotView, price);
            }
        }

        private InventorySlotView FillLocked(InventorySlotView inventorySlotView, int price)
        {
            inventorySlotView.LockedSlotContentView.Render(price);
            return inventorySlotView;
        }

        private InventorySlotView FillOpenned(InventorySlotView inventorySlotView, InventoryItemType type, int count)
        {
            Sprite icon;

            if (type == InventoryItemType.None)
            {
                icon = null;
            }
            else
            {
                icon = _staticDataService.GetSpriteByType(type);
            }

            inventorySlotView.OpenedSlotContentView.Init(icon, count);

            return inventorySlotView;
        }
    }
}