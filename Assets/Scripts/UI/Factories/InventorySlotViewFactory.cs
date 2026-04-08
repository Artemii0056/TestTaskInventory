using UI.InventoryScreen.Views;
using UnityEngine;

namespace UI.Factories
{
    public class InventorySlotViewFactory //TODO Подрубить пул!!!
    {
        private InventorySlotView _prefab;

        public InventorySlotViewFactory(InventorySlotView prefab)
        {
            _prefab = prefab;
        }

        public InventorySlotView Create()
        {
            return Object.Instantiate(_prefab);
        }
    }
}