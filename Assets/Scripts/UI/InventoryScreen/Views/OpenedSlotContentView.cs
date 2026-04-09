using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InventoryScreen.Views
{
    public class OpenedSlotContentView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countText;

        [SerializeField] private InventorySlotInfo _slotInfo; //TODO По ходу класс не нужен

        private int _count;

        public void Render(Sprite icon, int count)
        {
            _icon.sprite = icon;
            _icon.enabled = icon != null;

            _slotInfo.gameObject.SetActive(count > 1);
            _countText.text = count.ToString();
        }
        
        public void RenderEmpty()
        {
            _icon.sprite = null;
            _icon.enabled = false;

            _slotInfo.gameObject.SetActive(false);
            _countText.text = string.Empty;
        }
    }
}