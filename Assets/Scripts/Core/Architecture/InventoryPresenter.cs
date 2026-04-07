using Inventories.Ammo.AmmoFactories;

namespace Core.Architecture
{
    public class InventoryPresenter
    {
        private InventorySystem _inventorySystem;
        private InventoryView _inventoryView;
        private IAmmoFactory _ammoFactory;

        public InventoryPresenter(InventorySystem inventorySystem, InventoryView inventoryView, IAmmoFactory ammoFactory)
        {
            _inventorySystem = inventorySystem;
            _inventoryView = inventoryView;
            _ammoFactory = ammoFactory;
        }

        public void AddItemClicked()
        {
            
        }
        
        public void AddAmmoClicked()
        {
            _inventorySystem.TryAddItem(_ammoFactory.CreateRandom());
        }
    }
}