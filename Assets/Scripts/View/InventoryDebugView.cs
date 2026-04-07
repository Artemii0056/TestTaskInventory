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
        private InventoryPresenter _inventoryPresenter;

        public void Init(InventoryPresenter inventoryPresenter, Wallet wallet, InventorySystem inventorySystem)
        {
            _inventoryPresenter = inventoryPresenter;
            _inventorySystem = inventorySystem;
            _wallet = wallet;
            
            _addAmmoButton.OnClick += OnAddAmmoClick;
            _addItemButton.OnClick += OnAddItemClick;
            _deleteItemButton.OnClick += OnDeleteItemClick;
            _shootButton.OnClick += OnShootClick;
            _moneyButton.OnClick += OnMoneyClick;
        }

        private void OnEnable()
        {
            
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
            Debug.Log(_inventoryPresenter == null);
            _inventoryPresenter.AddAmmoClicked();
        }
        
        private void OnAddItemClick()
        {
            _inventoryPresenter.AddItemClicked();
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