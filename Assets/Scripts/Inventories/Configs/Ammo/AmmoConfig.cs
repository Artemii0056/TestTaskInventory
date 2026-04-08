using UnityEngine;

namespace Inventories.Configs.Ammo
{
    [CreateAssetMenu(fileName = nameof(AmmoConfig), menuName = "StaticData/Ammo/" + nameof(AmmoConfig))]
    public class AmmoConfig : ScriptableObject
    {
        [field: Header("Inventory info")]
        [field: SerializeField] public InventoryItemData InventoryItemData { get; private set; }
        
        [field: SerializeField] public AmmoType AmmoType { get; private set; }
    }
}