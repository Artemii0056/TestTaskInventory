using Core.Architecture;
using Infrastructure.StaticData;
using Inventories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
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

            if (isUnlocked == false)
            {
                return Create(inventorySlotView, type, count);
            }

            return Create(inventorySlotView, price);
        }

        private InventorySlotView Create(InventorySlotView inventorySlotView, int price)
        {
            inventorySlotView.LockedSlotContentView.Render(price);
            return inventorySlotView;
        }

        private InventorySlotView Create(InventorySlotView inventorySlotView, InventoryItemType type, int count)
        {
            //Debug.Log(type + " : " + count);

            Sprite icon;

            if (type == InventoryItemType.None)
            {
                icon = null;
            }
            else
            {
                icon = _staticDataService.GetSpriteByType(type);
            }

            Debug.Log($"InFactory {type} : {count} : {icon == null}");
            inventorySlotView.OpenedSlotContentView.Init(icon, count);

            return inventorySlotView;
        }
    }
}