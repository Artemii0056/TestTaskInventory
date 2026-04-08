using System.Collections.Generic;
using Inventories.Configs.Ammo;
using Inventories.Configs.Armors;
using Inventories.Configs.Weapons;

namespace Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        public List<AmmoConfig> GetAmmoConfigs();
        public List<WeaponConfig> GetWeaponConfigs();
        public List<ArmorConfig> GetArmorConfigs();
    }
}