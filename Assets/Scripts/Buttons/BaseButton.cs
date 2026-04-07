using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Button))]
    public class BaseButton : MonoBehaviour
    {
        private Button _button;

        [field: SerializeField] public TextMeshProUGUI Text;

        public event Action OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            Text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() => OnClick?.Invoke());
        }

        private void OnDisable() //TODO А надо ли
        {
            _button.onClick.AddListener(() => OnClick?.Invoke());
        }
    }
}