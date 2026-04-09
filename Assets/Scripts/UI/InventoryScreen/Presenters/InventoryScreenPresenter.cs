using System;
using Core;
using Core.Inventory;
using Core.Results;
using Core.Systems;
using Core.Wallets;
using Services;
using Services.AddMoneyServices;
using Services.AddRandomAmmoServices;
using Services.AddRandomItemServices;
using Services.Debbuger;
using Services.DeleteRandomItemServices;
using Services.SaveProgressServices;
using Services.ShootRandomWeaponServices;
using UI.Factories;
using UI.InventoryScreen.Views;

namespace UI.InventoryScreen.Presenters
{
    public class InventoryScreenPresenter : IDisposable
    {
        private readonly InventoryActionsView _inventoryActionsView;
        private readonly InventoryGridView _inventoryGridView;
        private readonly InventoryInfoView _inventoryInfoView;

        private readonly InventorySystem _inventorySystem;
        private readonly IWallet _wallet;
        private readonly IDebugMessageService _debugMessageService;
        private readonly InventorySlotViewFactory _inventorySlotViewFactory;
        
        private readonly IAddMoneyService _addMoneyService;
        private readonly IAddRandomAmmoService _addRandomAmmoService;
        private readonly IAddRandomItemService _addRandomItemService;
        private readonly IShootRandomWeaponService _shootRandomWeaponService;
        private readonly IDeleteRandomItemService _deleteItemService;
        private readonly IInventorySlotRenderService _inventorySlotRenderService;

        private readonly ISaveProgressService _saveProgressService;
        
        public InventoryScreenPresenter(
            InventoryActionsView inventoryActionsView,
            InventoryGridView inventoryGridView,
            InventoryInfoView inventoryInfoView,
            InventorySystem inventorySystem,
            IWallet wallet,
            IDebugMessageService debugMessageService,
            InventorySlotViewFactory inventorySlotViewFactory,
            IAddMoneyService addMoneyService, 
            IAddRandomAmmoService addRandomAmmoService, 
            IAddRandomItemService addRandomItemService,
            IShootRandomWeaponService shootRandomWeaponService, 
            IDeleteRandomItemService deleteItemService, 
            ISaveProgressService saveProgressService, 
            IInventorySlotRenderService inventorySlotRenderService)
        {
            _inventoryActionsView = inventoryActionsView;
            _inventoryGridView = inventoryGridView;
            _inventoryInfoView = inventoryInfoView;
            _inventorySystem = inventorySystem;
            _wallet = wallet;
            _debugMessageService = debugMessageService;
            _inventorySlotViewFactory = inventorySlotViewFactory;
            _addMoneyService = addMoneyService;
            _addRandomAmmoService = addRandomAmmoService;
            _addRandomItemService = addRandomItemService;
            _shootRandomWeaponService = shootRandomWeaponService;
            _deleteItemService = deleteItemService;
            _saveProgressService = saveProgressService;
            _inventorySlotRenderService = inventorySlotRenderService;
        }

        public void Initialize()
        {
            _inventoryActionsView.ActionClicked += OnActionClicked;
            
            foreach (InventorySlotData slot in _inventorySystem.Slots)
            {
                InventorySlotView slotView = _inventorySlotViewFactory.Create();

                slotView.Init(slot.Id);
                slotView.Clicked += OnSlotClicked;

                _inventorySlotRenderService.Render(slot, slotView);
                
                _inventoryGridView.Add(slotView);
            }
            
            Refresh();
        }
        
        public void Dispose()
        {
            _inventoryActionsView.ActionClicked -= OnActionClicked; 
            
            foreach (var slot in _inventoryGridView.Slots)
            {
                slot.Clicked -= OnSlotClicked;
            }
        }
        
        private void OnActionClicked(InventoryActionType actionType)
        {
            switch (actionType)
            {
                case InventoryActionType.AddAmmo:
                    HandleAddAmmo();
                    break;

                case InventoryActionType.AddItem:
                    HandleAddItem();
                    break;

                case InventoryActionType.DeleteItem:
                    HandleDeleteItem();
                    break;

                case InventoryActionType.Shoot:
                    HandleShoot();
                    break;

                case InventoryActionType.AddMoney:
                    HandleAddMoney();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }

            Refresh();
            
            _saveProgressService.Save();
        }

        private void HandleShoot()
        {
            var result = _shootRandomWeaponService.Shoot();

            if (result.WeaponType == InventoryItemType.None)
            {
                _debugMessageService.ShowErrorMessage("Нет оружия");
                return;
            }

            if (!result.IsSuccess)
            {
                _debugMessageService.ShowMessage($"Нет патронов для {result.WeaponType}");
                return;
            }

            _debugMessageService.ShowMessage(
                $"Выстрел из {result.WeaponType}, патроны: {result.AmmoType}, урон: {result.Damage}");
        }
        
        private void HandleDeleteItem()
        {
            DeleteItemResult result = _deleteItemService.Delete();

            if (!result.Success)
            {
                _debugMessageService.ShowErrorMessage("Инвентарь пуст");
                return;
            }

            _debugMessageService.ShowMessage(
                $"Удалено ({result.Count}) [{result.ItemType}] из слота: [{result.SlotId}]");
        }

        private void HandleAddMoney()
        {
            AddMoneyResult result = _addMoneyService.AddRandom();
            _debugMessageService.ShowMessage($"Добавлено ({result.Amount}) монет");
        }

        private void HandleAddItem()
        {
            AddRandomItemResult result = _addRandomItemService.AddRandom();

            if (!result.IsSuccess)
            {
                _debugMessageService.ShowErrorMessage("Инвентарь полон");
                return;
            }

            _debugMessageService.ShowMessage(
                $"Добавлено {result.ItemType} в слот: {result.SlotId}");
        }

        private void HandleAddAmmo()
        {
            AddAmmoResult result = _addRandomAmmoService.AddRandom();

            foreach (SlotChange change in result.Changes)
            {
                _debugMessageService.ShowMessage(
                    $"Добавлено ({change.Delta}) {change.ItemType} в слот: {change.SlotId}");
            }

            if (result.RemainingAmount > 0)
                _debugMessageService.ShowErrorMessage("Инвентарь полон");
        }

        private void Refresh()
        {
            RefreshGrid();
            RefreshInfo();
        }

        private void RefreshGrid()
        {
            for (int i = 0; i < _inventorySystem.Slots.Count; i++)
            {
                InventorySlotData slotData = _inventorySystem.Slots[i];
                InventorySlotView slotView = _inventoryGridView.Slots[i];

                _inventorySlotRenderService.Render(slotData, slotView);
            }
        }

        private void OnSlotClicked(int id) 
        {
            InventorySlotData slot = _inventorySystem.GetSlotById(id);

            if (!slot.IsUnlocked)
            {
                HandleLockedSlotClick(id);
                return;
            }

            Refresh();
        }

        private void HandleLockedSlotClick(int id)
        {
            if (_inventorySystem.TryUnlockSlot(id))
                _saveProgressService.Save();

            Refresh();
        }

        private void RefreshInfo()
        {
            _inventoryInfoView.DrawMoney(_wallet.Coins);
            _inventoryInfoView.DrawWeight(_inventorySystem.GetTotalWeight());
        }
    }
}