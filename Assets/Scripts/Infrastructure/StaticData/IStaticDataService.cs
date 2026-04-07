using System.Collections.Generic;
using Inventories.Ammo;
using Inventories.Armors;
using Inventories.Weapons;

namespace Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        public List<AmmoConfig> GetAmmoConfigs();
        public List<WeaponConfig> GetWeaponConfigs();
        public List<ArmorConfig> GetArmorConfigs();
    }
}