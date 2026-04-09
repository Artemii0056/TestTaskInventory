using Infrastructure.StaticData;
using UI.InventoryScreen.Views;
using UnityEngine;

namespace UI.Factories
{
    public class InventorySlotViewFactory //TODO Подрубить пул!!!
    {
        private InventorySlotView _prefab;

        public InventorySlotViewFactory(IStaticDataService staticDataService)
        {
            _prefab = staticDataService.InventorySlotView;
        }

        public InventorySlotView Create()
        {
            return Object.Instantiate(_prefab);
        }
    }
}