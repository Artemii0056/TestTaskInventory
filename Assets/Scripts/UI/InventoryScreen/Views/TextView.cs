using TMPro;
using UnityEngine;

namespace UI.InventoryScreen.Views
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] private string _baseText;

        [SerializeField] private TextMeshProUGUI _text;

        public void Setup(string text) =>
            _text.text = $"{_baseText}: {text}";
    }
}