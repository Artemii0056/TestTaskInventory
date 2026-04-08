using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class InventoryActionButtonView : MonoBehaviour
    {
        private Button _button;

        [field: SerializeField] public TextMeshProUGUI Text{ get; private set; }
        [field: SerializeField] public InventoryActionType InventoryActionType { get; private set; }

        public event Action<InventoryActionType> OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            Text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable()
            => _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked() => 
            OnClick?.Invoke(InventoryActionType);
    }
}