using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Configs;
using Core.Configs.Ammo;
using Core.Configs.Armors;
using Core.Configs.Weapons;
using Infrastructure.ResourceLoad;
using UI.InventoryScreen.Views;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IResourceLoader _resourceLoader;
        
        private List<AmmoConfig> _ammoConfigs;
        private List<ArmorConfig> _armorConfigs;
        private List<WeaponConfig> _weaponConfigs;
        
        private Dictionary<InventoryItemType, InventoryItemData> _itemDataByType;

        public InventoryConfig InventoryConfig { get; private set; }
        public InventorySlotView InventorySlotView { get; private set; }

        public StaticDataService(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public void LoadAll()
        {
            LoadAmmoConfigs();
            LoadArmorConfigs();
            LoadWeaponConfigs();
            LoadInventoryConfig();

            BuildItemDataDictionary();
            LoadInventorySlotView();
        }

        

        public InventoryItemType GetAmmoItemType(AmmoType ammoType)
        {
            foreach (AmmoConfig config in GetAmmoConfigs())
            {
                if (config.AmmoType == ammoType)
                    return config.InventoryItemData.Type;
            }

            return InventoryItemType.None;
        }
        
        public InventoryItemData GetItemDataByType(InventoryItemType type)
        {
            if (_itemDataByType.TryGetValue(type, out var data))
                return data;

            throw new Exception($"ItemData not found for type: {type}");
        }
        
        public bool IsItemOfKind(InventoryItemType type, ItemKind kind)
        {
            return GetItemDataByType(type).Kind == kind;
        }

        public Sprite GetSpriteByType(InventoryItemType itemType)
        {
            return GetItemDataByType(itemType).Sprite;
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

            throw new Exception($"AmmoConfig not found for type: {itemType}");
        }

        public WeaponConfig GetWeaponConfigByType(InventoryItemType itemType)
        {
            foreach (var config in _weaponConfigs)
            {
                if (config.InventoryItemData.Type == itemType)
                    return config;
            }
            
            throw new Exception($"WeaponConfig not found for type: {itemType}");
        }

        public List<AmmoConfig> GetAmmoConfigs() =>  
            _ammoConfigs.ToList();

        public List<WeaponConfig> GetWeaponConfigs() => 
            _weaponConfigs.ToList();

        public List<ArmorConfig> GetArmorConfigs() => 
            _armorConfigs.ToList();
        
        private void LoadInventoryConfig() => 
            InventoryConfig = _resourceLoader.LoadScriptableObject<InventoryConfig>(Constants.InventoryConfigPath);

        private void LoadInventorySlotView() => 
            InventorySlotView = _resourceLoader.Load<InventorySlotView>(Constants.InventorySlotPath);

        private void AddItemData(InventoryItemData data)
        {
            if (_itemDataByType.ContainsKey(data.Type))
                throw new Exception($"Duplicate item type: {data.Type}");

            _itemDataByType.Add(data.Type, data);
        }
        
        private void BuildItemDataDictionary()
        {
            _itemDataByType = new Dictionary<InventoryItemType, InventoryItemData>();

            foreach (WeaponConfig config in _weaponConfigs)
                AddItemData(config.InventoryItemData);

            foreach (AmmoConfig config in _ammoConfigs)
                AddItemData(config.InventoryItemData);

            foreach (ArmorConfig config in _armorConfigs)
                AddItemData(config.InventoryItemData);
        }

        private void LoadAmmoConfigs() => 
            _ammoConfigs = _resourceLoader.LoadAll<AmmoConfig>(Constants.ConfigsAmmosPath).ToList();

        private void LoadArmorConfigs() => 
            _armorConfigs = _resourceLoader.LoadAll<ArmorConfig>(Constants.ConfigsArmorsPath).ToList();

        private void LoadWeaponConfigs() => 
            _weaponConfigs = _resourceLoader.LoadAll<WeaponConfig>(Constants.ConfigsWeaponsPath).ToList();
    }
}