using TMPro;
using UnityEngine;

namespace Core
{
    public class InventorySlotInfo : MonoBehaviour 
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}