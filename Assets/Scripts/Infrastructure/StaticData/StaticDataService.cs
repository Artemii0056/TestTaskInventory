using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.ResourceLoad;
using Inventories;
using Inventories.Configs.Ammo;
using Inventories.Configs.Armors;
using Inventories.Configs.Weapons;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IResourceLoader _resourceLoader;
        
        private List<AmmoConfig> _ammoConfigs;
        private List<ArmorConfig> _armorConfigs;
        private List<WeaponConfig> _weaponConfigs;
        
        public StaticDataService(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;

            LoadAmmoConfigs();
            LoadArmorConfigs();
            LoadWeaponConfigs();
        }

        public Sprite GetSpriteByType(InventoryItemType itemType)
        {
            foreach (var config in _ammoConfigs)
            {
                if (config.InventoryItemData.Type == itemType)
                    return config.InventoryItemData.Sprite;
            }
            
            foreach (var config in _armorConfigs)
            {
                if (config.InventoryItemData.Type == itemType)
                    return config.InventoryItemData.Sprite;
            }
            
            foreach (var config in _weaponConfigs)
            {
                if (config.InventoryItemData.Type == itemType)
                    return config.InventoryItemData.Sprite;
            }

            Debug.Log(itemType);
            
            throw new Exception("Не нашел");
        }

        public AmmoConfig GetAmmoConfigByType(InventoryItemType itemType)
        {
            foreach (var config in _ammoConfigs)
            {
                if (config.InventoryItemData.Type == itemType)
                {
                    return config;
                }
            }

            throw new Exception("123");
        }

        public List<AmmoConfig> GetAmmoConfigs() =>  
            _ammoConfigs.ToList();

        public List<WeaponConfig> GetWeaponConfigs() => 
            _weaponConfigs.ToList();

        public List<ArmorConfig> GetArmorConfigs() => 
            _armorConfigs.ToList();

        private void LoadAmmoConfigs() => 
            _ammoConfigs = _resourceLoader.LoadAll<AmmoConfig>(Constants.ConfigsAmmosPath).ToList();

        private void LoadArmorConfigs() => 
            _armorConfigs = _resourceLoader.LoadAll<ArmorConfig>(Constants.ConfigsArmorsPath).ToList();

        private void LoadWeaponConfigs() => 
            _weaponConfigs = _resourceLoader.LoadAll<WeaponConfig>(Constants.ConfigsWeaponsPath).ToList();
    }
}