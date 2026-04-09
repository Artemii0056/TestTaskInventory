using Core.Inventory;
using Core.Results;
using Core.Systems;
using Services.AmmoFactories;

namespace Services.AddRandomAmmoServices
{
    public class AddRandomAmmoService : IAddRandomAmmoService
    {
        private readonly IAmmoFactory _ammoFactory;
        private readonly InventorySystem _inventorySystem;

        public AddRandomAmmoService(
            IAmmoFactory ammoFactory,
            InventorySystem inventorySystem)
        {
            _ammoFactory = ammoFactory;
            _inventorySystem = inventorySystem;
        }

        public AddAmmoResult AddRandom()
        {
            ItemStack ammoStack = _ammoFactory.CreateRandom();
            AddAmmoResult result = _inventorySystem.AddAmmo(ammoStack.Type, ammoStack.Count);

            return result;
        }
    }
}