using System;
using System.Collections.Generic;
using Buttons;
using UnityEngine;

namespace Core.Architecture
{
    public class InventoryActionsView : MonoBehaviour
    {
        [SerializeField] private List<InventoryActionButtonView> _buttonViews;

        public event Action<InventoryActionType> ActionClicked;
        
        private void OnEnable()
        {
            foreach (var button in _buttonViews) 
                button.OnClick += OnButtonClick;
        }
        
        private void OnDisable()
        {
            foreach (var button in _buttonViews) 
                button.OnClick -= OnButtonClick;
        }
        
        private void OnButtonClick(InventoryActionType type) => 
            ActionClicked?.Invoke(type);
    }
}