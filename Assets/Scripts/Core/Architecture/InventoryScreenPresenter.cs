using System;
using Core.ActionResults;
using DefaultNamespace;
using Inventories.Configs.Ammo.AmmoFactories;
using Inventories.View;
using UnityEngine;

namespace Core.Architecture
{
    public class InventoryScreenPresenter
    {
        private InventoryActionsView _inventoryActionsView;
        private InventoryGridView _inventoryGridView;
        private InventoryInfoView _inventoryInfoView;
        
        private InventorySystem _inventorySystem;
        private IWallet _wallet;
        private IAmmoFactory _ammoFactory;
        private IDebugMessageService _debugMessageService;

        public InventoryScreenPresenter(
            InventoryActionsView inventoryActionsView, 
            InventoryGridView inventoryGridView,
            InventoryInfoView inventoryInfoView, 
            InventorySystem inventorySystem, 
            IWallet wallet, 
            IAmmoFactory ammoFactory, 
            IDebugMessageService debugMessageService)
        {
            _inventoryActionsView = inventoryActionsView;
            _inventoryGridView = inventoryGridView;
            _inventoryInfoView = inventoryInfoView;
            _inventorySystem = inventorySystem;
            _wallet = wallet;
            _ammoFactory = ammoFactory;
            _debugMessageService = debugMessageService;

            _inventoryActionsView.ActionClicked += OnActionClicked;
        }
        
        private void OnActionClicked(InventoryActionType actionType)
        {
            switch (actionType)
            {
                case InventoryActionType.AddAmmo:
                    AddAmmoAction();
                    break;

                case InventoryActionType.AddItem:
                    //AddItem();
                    break;

                case InventoryActionType.DeleteItem:
                    //DeleteSelectedItem();
                    break;

                case InventoryActionType.Shoot:
                   // ShootSelectedWeapon();
                    break;

                case InventoryActionType.AddMoney:
                    IncreaseMoney();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }
        }

        private void IncreaseMoney()
        {
            _wallet.Increase(10);
        }

        private void AddAmmoAction()
        {
             ItemStack ammoStack = _ammoFactory.CreateRandom();
             
             bool result =  _inventorySystem.TryAddItem(ammoStack, out int slotId);

             if (result)
                 _debugMessageService.ShowMessage(
                     $"Добавлено ({ammoStack.Count}) {ammoStack.InventoryItemType} в слот: {slotId}");
             else
                 _debugMessageService.ShowMessage("Инвентарь полон");

             Refresh();
        }

        private void Refresh()
        {
            
        }

        public void Dispose()
        {
            _inventoryActionsView.ActionClicked -= OnActionClicked;
        }
    }
}