using Buttons;
using Core;
using Core.Architecture;
using UnityEngine;

namespace View
{
    public class InventoryDebugView : MonoBehaviour
    {
        [SerializeField] private AddAmmoButton _addAmmoButton; //TODO Нет.Сделать через визитера и передавать тип 
        [SerializeField] private AddItemButton _addItemButton;
        [SerializeField] private DeleteItemButton _deleteItemButton;
        [SerializeField] private ShootButton _shootButton;
        [SerializeField] private AddMoneyButton _moneyButton;
        
        private Wallet _wallet;
        
        private InventorySystem _inventorySystem;
        
        public void Construct(InventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
            _wallet = new Wallet(10);
        }

        private void OnEnable()
        {
            _addAmmoButton.OnClick += OnAddAmmoClick;
            _addItemButton.OnClick += OnAddItemClick;
            _deleteItemButton.OnClick += OnDeleteItemClick;
            _shootButton.OnClick += OnShootClick;
            _moneyButton.OnClick += OnMoneyClick;
        }

        private void OnDisable()
        {
            _addAmmoButton.OnClick -= OnAddAmmoClick;
            _addItemButton.OnClick -= OnAddItemClick;
            _deleteItemButton.OnClick -= OnDeleteItemClick;
            _shootButton.OnClick -= OnShootClick;
            _moneyButton.OnClick -= OnMoneyClick;
        }

        private void OnAddAmmoClick()
        {
            throw new System.NotImplementedException();
        }
        
        private void OnAddItemClick()
        {
            throw new System.NotImplementedException();
        }
        
        private void OnDeleteItemClick()
        {
            throw new System.NotImplementedException();
        }
        
        private void OnShootClick()
        {
            throw new System.NotImplementedException();
        }
        
        private void OnMoneyClick()
        {
            _wallet.Increase(20);
            _moneyButton.Text.text = _wallet.Coins.ToString();
        }
    }
}