using UnityEngine;

namespace Core.Architecture
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private OpenedSlotContentView _openedSlotContentView;
        [SerializeField] private LockedSlotContentView _lockedSlotContentView;
        
        private bool _isUnlocked = false;
    }
}