using UnityEngine;

namespace Inventories.Configs.Armors
{
    [CreateAssetMenu(fileName = nameof(ArmorConfig), menuName = "StaticData/Armors/" + nameof(ArmorConfig))]
    public class ArmorConfig : ScriptableObject, IItemConfig
    {
        [field: Header("Inventory info")]
        [field: SerializeField] public InventoryItemData InventoryItemData { get; private set; }
        
        [field: SerializeField] public ArmorType ArmorType { get; private set; }
        [field: SerializeField] public float Protection { get; private set; }
    }
}