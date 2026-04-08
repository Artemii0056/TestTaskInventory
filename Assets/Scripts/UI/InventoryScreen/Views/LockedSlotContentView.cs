using TMPro;
using UnityEngine;

namespace UI.InventoryScreen.Views
{
    public class LockedSlotContentView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;

        public void Render(int price)
        {
            _priceText.text = price.ToString();
        }
    }
}