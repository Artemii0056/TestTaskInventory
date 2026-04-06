using Inventories.Configs;
using UnityEngine;

namespace Inventories.ArmorItems
{
    [CreateAssetMenu(fileName = nameof(InventoryItemData), menuName = "StaticData/Armors/" + nameof(ArmorConfig))]
    public class ArmorConfig : ScriptableObject
    {
        [field: Header("Inventory info")]
        [field: SerializeField] public InventoryItemData InventoryItemData { get; private set; }
        
        [field: SerializeField] public ArmorType ArmorType { get; private set; }
        [field: SerializeField] public float Protection { get; private set; }
    }
}