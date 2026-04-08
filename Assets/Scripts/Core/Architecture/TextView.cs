using TMPro;
using UnityEngine;

namespace Core.Architecture
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] private string _baseText;

        [SerializeField] private TextMeshProUGUI _text;

        public void Setup(TextMeshProUGUI text) =>
            _text.text = $"{_baseText}: {text.text}";
    }
}