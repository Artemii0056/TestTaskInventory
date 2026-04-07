using System.Collections.Generic;
using Infrastructure.StaticData;
using Inventories.Ammo;
using Inventories.Armors;
using Inventories.Weapons;

namespace DefaultNamespace
{
    public class ItemCatalog : IItemCatalog
    {
        public IReadOnlyCollection<ArmorConfig> ArmorConfigs;
        public IReadOnlyCollection<AmmoConfig> AmmoConfigs;
        public IReadOnlyCollection<WeaponConfig> WeaponConfigs;
        
        private readonly IStaticDataService _staticDataService;

        public ItemCatalog(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            
            ArmorConfigs = _staticDataService.GetArmorConfigs();
            AmmoConfigs = _staticDataService.GetAmmoConfigs();
            WeaponConfigs = _staticDataService.GetWeaponConfigs();
        }
    }

    public interface IItemCatalog
    {
    }
}