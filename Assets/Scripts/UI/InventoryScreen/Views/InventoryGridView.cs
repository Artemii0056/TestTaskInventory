using System.Collections.Generic;
using UnityEngine;

namespace UI.InventoryScreen.Views
{
    public class InventoryGridView : MonoBehaviour
    {
        [SerializeField] private Transform _content;

        private List<InventorySlotView> _slots;

        public InventoryGridView()
        {
            _slots = new List<InventorySlotView>();
        }
        
        public IReadOnlyList<InventorySlotView> Slots => _slots;

        public void Add(InventorySlotView view)
        {
            _slots.Add(view);
            view.transform.SetParent(_content);
        }
    }
}