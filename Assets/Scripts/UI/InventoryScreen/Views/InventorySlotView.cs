using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.InventoryScreen.Views
{
    public class InventorySlotView : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] public OpenedSlotContentView OpenedSlotContentView;
        [field: SerializeField] public LockedSlotContentView LockedSlotContentView;

        private int _slotId;

        public event Action<int> Clicked;

        private void Awake()
        {
            OpenedSlotContentView.gameObject.SetActive(false);
            LockedSlotContentView.gameObject.SetActive(false);
        }

        public void Init(int slotId) =>
            _slotId = slotId;

        public void OnPointerClick(PointerEventData eventData) =>
            Clicked?.Invoke(_slotId);

        public void RenderLocked(int price)
        {
            LockedSlotContentView.gameObject.SetActive(true);
            OpenedSlotContentView.gameObject.SetActive(false);
            LockedSlotContentView.Render(price);
        }

        public void RenderOpened(Sprite icon, int count)
        {
            LockedSlotContentView.gameObject.SetActive(false);
            OpenedSlotContentView.gameObject.SetActive(true);
            OpenedSlotContentView.Render(icon, count);
        }

        public void RenderEmptyOpened()
        {
            LockedSlotContentView.gameObject.SetActive(false);
            OpenedSlotContentView.gameObject.SetActive(true);
            OpenedSlotContentView.RenderEmpty();
        }
    }
}