using System;
using UnityEngine;

namespace Inventories.Configs
{
    [Serializable]
    public class InventoryItemData 
    {
        [field: SerializeField] public InventoryItemType Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        
        //[field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        [field: SerializeField] public float Weight { get; private set; }
        
        [field: SerializeField] public int MaxStack { get; private set; }
        
        [field: SerializeField] public ItemKind Kind { get; private set; }
    }
}