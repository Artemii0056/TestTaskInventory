using System.Collections.Generic;
using Core;
using Core.Configs;
using Core.Configs.Ammo;
using Core.Configs.Armors;
using Core.Configs.Weapons;
using UI.InventoryScreen.Views;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        public List<AmmoConfig> GetAmmoConfigs();
        public List<WeaponConfig> GetWeaponConfigs();
        public List<ArmorConfig> GetArmorConfigs();
        Sprite GetSpriteByType(InventoryItemType itemType);
        AmmoConfig GetAmmoConfigByType(InventoryItemType itemType);
        WeaponConfig GetWeaponConfigByType(InventoryItemType itemType);
        bool IsItemOfKind(InventoryItemType type, ItemKind kind);
        InventoryItemData GetItemDataByType(InventoryItemType type);
        InventoryItemType GetAmmoItemType(AmmoType ammoType);
        InventoryConfig InventoryConfig { get; }
        InventorySlotView InventorySlotView { get; }
    }
}