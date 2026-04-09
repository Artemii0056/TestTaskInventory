using System;
using System.Collections.Generic;
using Core.Slots;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = nameof(InventoryConfig), menuName = "StaticData/Inventory/" + nameof(InventoryConfig))]
    public class InventoryConfig : ScriptableObject
    {
        [field: SerializeField] public int CountUnlockSlots { get; private set; }
        [field: SerializeField] public int CountAllSlots { get; private set; }

        [SerializeField] private List<SlotPrice> _lockedPrices;

        public IReadOnlyList<SlotPrice> LockedPrices => _lockedPrices;
        
        [Header("Default price generation")]
        [SerializeField] private int _startPrice = 20;
        [SerializeField] private int _priceStep = 5;
        

#if UNITY_EDITOR
        public void FillDefaultPrices()
        {
            _lockedPrices.Clear();

            for (int slotIndex = CountUnlockSlots; slotIndex < CountAllSlots; slotIndex++)
            {
                int lockedIndex = slotIndex - CountUnlockSlots;
                int price = _startPrice + _priceStep * lockedIndex;

                _lockedPrices.Add(new SlotPrice(slotIndex, price));
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}