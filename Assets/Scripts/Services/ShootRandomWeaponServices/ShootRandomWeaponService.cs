using Core.Results.DefaultNamespace.Results;
using Core.Systems;
using Services.ShootRandomWeaponServices;

namespace Services.ShootServices
{
    public class ShootRandomWeaponService : IShootRandomWeaponService
    {
        private readonly InventorySystem _inventorySystem;

        public ShootRandomWeaponService(InventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }

        public ShootResult Shoot() => 
            _inventorySystem.ShootRandomWeapon();
    }
}