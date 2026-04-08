using TMPro;
using UnityEngine;

namespace Core.Architecture
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