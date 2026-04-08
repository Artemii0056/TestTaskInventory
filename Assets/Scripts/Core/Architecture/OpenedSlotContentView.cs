using Inventories;
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

        public void Render(InventoryItemType itemType, int count) //TODO Тут нужен сервис передающий нуюное вью взависимости от типа!
        {
            bool isEmpty = itemType == InventoryItemType.None;

            _icon.gameObject.SetActive(isEmpty == false);
            _slotInfo.gameObject.SetActive(isEmpty == false && count > 1);

            if (!isEmpty)
            {
                //_icon.sprite =  //TODO Тут нужен сервис передающий нуюное вью взависимости от типа!
                _countText.text = count.ToString();
            }
        }
    }
}