using Inventories.Ammo;
using Inventories.Configs;
using UnityEngine;

namespace Inventories.Weapons
{
    [CreateAssetMenu(fileName = nameof(WeaponConfig), menuName = "StaticData/Weapons/" + nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject
    {
        [field: Header("Inventory info")]
        [field: SerializeField] public InventoryItemData InventoryItemData { get; private set; }
        
        [field: SerializeField] public ItemKind Kind { get; private set; }
        [field: SerializeField] public AmmoType AmmoType { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}