using UnityEngine;

namespace UI.InventoryScreen.Views
{
    public class InventorySlotView : MonoBehaviour
    {
        [field: SerializeField] public OpenedSlotContentView OpenedSlotContentView;
        [field: SerializeField] public LockedSlotContentView LockedSlotContentView;

        private void Awake()
        {
            OpenedSlotContentView.gameObject.SetActive(false);
            LockedSlotContentView.gameObject.SetActive(false);
        }

        public void Show(bool isUnlocked)
        {
            if (isUnlocked==false)
            {
                LockedSlotContentView.gameObject.SetActive(true);
            }
            else
            {
                OpenedSlotContentView.gameObject.SetActive(true);
                OpenedSlotContentView.Show();
            }
        }
    }
}