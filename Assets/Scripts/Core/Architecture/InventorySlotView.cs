using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Architecture
{
    public class InventorySlotView : MonoBehaviour
    {
        [FormerlySerializedAs("_openedSlotContentView")] [field: SerializeField] public OpenedSlotContentView OpenedSlotContentView;
        [FormerlySerializedAs("_lockedSlotContentView")] [field: SerializeField] public LockedSlotContentView LockedSlotContentView;

        private void Awake()
        {
            OpenedSlotContentView.gameObject.SetActive(false);
            LockedSlotContentView.gameObject.SetActive(false);
        }

        public void Show(bool isUnlocked)
        {
            if (isUnlocked)
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