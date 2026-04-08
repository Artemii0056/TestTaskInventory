using System.Collections.Generic;
using Core.Architecture;
using UnityEngine;

namespace Inventories.View
{
    public class InventoryGridView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        
        private List<InventorySlotView> _slots;

        public void Add(InventorySlotView view)
        {
            _slots.Add(view);
            view.transform.SetParent(_content);
        }
    }
}