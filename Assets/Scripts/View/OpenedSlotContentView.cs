using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Architecture
{
    public class OpenedSlotContentView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countText;

        [SerializeField] private InventorySlotInfo _slotInfo;

        private int _count;

        public void Init(Sprite icon, int count) 
        {
            if (icon != null) 
                _icon.sprite = icon;
            
            _count = count;
        }
        
        public void Show()
        {
            _slotInfo.gameObject.SetActive(_count > 1);

            _countText.text = _count.ToString();
        }
    }
}