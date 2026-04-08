using System;
using DefaultNamespace;
using Inventories.Configs.Ammo.AmmoFactories;
using Inventories.View;

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
        private InventorySlotViewFactory _inventorySlotViewFactory;

        public InventoryScreenPresenter(
            InventoryActionsView inventoryActionsView,
            InventoryGridView inventoryGridView,
            InventoryInfoView inventoryInfoView,
            InventorySystem inventorySystem,
            IWallet wallet,
            IAmmoFactory ammoFactory,
            IDebugMessageService debugMessageService, InventorySlotViewFactory inventorySlotViewFactory)
        {
            _inventoryActionsView = inventoryActionsView;
            _inventoryGridView = inventoryGridView;
            _inventoryInfoView = inventoryInfoView;
            _inventorySystem = inventorySystem;
            _wallet = wallet;
            _ammoFactory = ammoFactory;
            _debugMessageService = debugMessageService;
            _inventorySlotViewFactory = inventorySlotViewFactory;

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

            bool result = _inventorySystem.TryAddItem(ammoStack, out int slotId);

            if (result)
                _debugMessageService.ShowMessage(
                    $"Добавлено ({ammoStack.Count}) {ammoStack.Type} в слот: {slotId}");
            else
                _debugMessageService.ShowMessage("Инвентарь полон");

            Refresh();
        }

        public void Refresh()
        {
            _inventoryGridView.DeleteAll();

            InventorySlotView slotView;

            foreach (var slot in _inventorySystem.Slots)
            {
                if (slot.ItemStack == null)
                {
                    slotView = _inventorySlotViewFactory.Create(slot.IsUnlocked);
                    _inventoryGridView.Add(slotView);
                }
                else
                {
                    slotView = _inventorySlotViewFactory.Create(slot.IsUnlocked, slot.ItemStack.Type, slot.ItemStack.Count);
                    _inventoryGridView.Add(slotView);
                }
                
                slotView.Show(slot.IsUnlocked);
            }
        }

        public void Dispose()
        {
            _inventoryActionsView.ActionClicked -= OnActionClicked;
        }
    }
}