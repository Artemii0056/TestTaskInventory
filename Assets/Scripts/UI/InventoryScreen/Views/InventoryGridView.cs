using System.Collections.Generic;
using System.Linq;
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

        public void Add(InventorySlotView view)
        {
            _slots.Add(view);
            view.transform.SetParent(_content);
        }

        public void DeleteAll()
        {
            if (_slots.Count == 0)
                return;

            foreach (var slot in _slots.ToList())
            {
                Destroy(slot.gameObject);
                _slots.Remove(slot);
            }
        }
    }
}