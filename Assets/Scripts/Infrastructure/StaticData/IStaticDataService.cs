using System.Collections.Generic;
using Inventories;
using Inventories.Configs.Ammo;
using Inventories.Configs.Armors;
using Inventories.Configs.Weapons;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        public List<AmmoConfig> GetAmmoConfigs();
        public List<WeaponConfig> GetWeaponConfigs();
        public List<ArmorConfig> GetArmorConfigs();
        Sprite GetSpriteByType(InventoryItemType itemType);
    }
}