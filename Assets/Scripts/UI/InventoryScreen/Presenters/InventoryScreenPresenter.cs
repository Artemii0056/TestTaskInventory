using System;
using System.Collections.Generic;
using Core;
using Core.Inventory;
using Core.Results;
using Core.Results.DefaultNamespace.Results;
using Core.Systems;
using Core.Wallets;
using Infrastructure.SaveLoad;
using Infrastructure.StaticData;
using Services.AmmoFactories;
using Services.Debbuger;
using Services.InventoryUnlockServices;
using Services.ItemsFactory;
using Services.RandomServices;
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
        private readonly IAmmoFactory _ammoFactory;
        private readonly IDebugMessageService _debugMessageService;
        private readonly InventorySlotViewFactory _inventorySlotViewFactory;
        private readonly IItemFactory _itemFactory;
        private readonly IRandomService _randomService;
        private readonly IStaticDataService _staticDataService;
        private readonly IInventoryUnlockService _inventoryUnlockService;
        private readonly IInventoryPriceData _inventoryPriceData;
        private readonly IGameSaveService _gameSaveService;

        private List<SlotPrice> _slotPriceList;

        public InventoryScreenPresenter(
            InventoryActionsView inventoryActionsView,
            InventoryGridView inventoryGridView,
            InventoryInfoView inventoryInfoView,
            InventorySystem inventorySystem,
            IWallet wallet,
            IAmmoFactory ammoFactory,
            IDebugMessageService debugMessageService,
            InventorySlotViewFactory inventorySlotViewFactory,
            IItemFactory itemFactory,
            IRandomService randomService,
            IStaticDataService staticDataService,
            IInventoryUnlockService inventoryUnlockService, 
            IInventoryPriceData inventoryPriceData, IGameSaveService gameSaveService)
        {
            _inventoryActionsView = inventoryActionsView;
            _inventoryGridView = inventoryGridView;
            _inventoryInfoView = inventoryInfoView;
            _inventorySystem = inventorySystem;
            _wallet = wallet;
            _ammoFactory = ammoFactory;
            _debugMessageService = debugMessageService;
            _inventorySlotViewFactory = inventorySlotViewFactory;
            _itemFactory = itemFactory;
            _randomService = randomService;
            _staticDataService = staticDataService;
            _inventoryUnlockService = inventoryUnlockService;
            _inventoryPriceData = inventoryPriceData;
            _gameSaveService = gameSaveService;

            // _inventoryActionsView.ActionClicked += OnActionClicked;
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
            _gameSaveService.SaveInventory();
        }

        private void HandleShoot()
        {
            ShootResult result = _inventorySystem.ShootRandomWeapon();

            if (result.WeaponType == InventoryItemType.None)
            {
                _debugMessageService.ShowMessage("Нет оружия");
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
            if (_inventorySystem.TryDeleteItem(out DeleteItemResult result))
            {
                _debugMessageService.ShowMessage(
                    $"Удалено ({result.Count}) [{result.ItemType}] из слота: [{result.SlotId}]");
                return;
            }

            _debugMessageService.ShowMessage("Инвентарь пуст");
        }

        private void HandleAddMoney()
        {
            int value = _randomService.GenerateValue(10, 99);

            _wallet.Increase(value);
            _debugMessageService.ShowMessage($"Добавлено ({value}) монет");
        }

        private void HandleAddItem()
        {
            ItemStack stack = _itemFactory.CreateRandom();

            if (!_inventorySystem.TryAddItem(stack, out InventorySlotData slot))
            {
                _debugMessageService.ShowMessage("Инвентарь полон");
                return;
            }

            _debugMessageService.ShowMessage(
                $"Добавлено {slot.ItemStack.Type} в слот: {slot.Id}");
        }

        private void HandleAddAmmo()
        {
            ItemStack ammoStack = _ammoFactory.CreateRandom();
            AddAmmoResult result = _inventorySystem.AddAmmo(ammoStack.Type, ammoStack.Count);

            foreach (SlotChange change in result.Changes)
            {
                _debugMessageService.ShowMessage(
                    $"Добавлено ({change.Delta}) {change.ItemType} в слот: {change.SlotId}");
            }

            if (result.RemainingAmount > 0)
                _debugMessageService.ShowMessage("Инвентарь полон");
        }

        public void Refresh()
        {
            RefreshGrid();
            RefreshInfo();
        }

        private void RefreshGrid()
        {
            foreach (var slot in _inventoryGridView.Slots)
            {
                slot.Clicked -= OnSlotClicked;
            }

            _inventoryGridView.DeleteAll();

            foreach (InventorySlotData slot in _inventorySystem.Slots)
            {
                InventorySlotView slotView = _inventorySlotViewFactory.Create();

                slotView.Init(slot.Id);
                slotView.Clicked += OnSlotClicked;

                RenderSlot(slot, slotView);

                _inventoryGridView.Add(slotView);
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

            HandleOpenedSlotClick(id);
        }

        private void HandleOpenedSlotClick(int id)
        {
            Refresh();
        }

        private void HandleLockedSlotClick(int id)
        {
            //_inventoryUnlockService.
            _inventorySystem.TryUnlockSlot(id);

            Refresh();
        }

        private void RenderSlot(InventorySlotData slot, InventorySlotView slotView)
        {
            if (slot.IsUnlocked == false)
            {
                slotView.RenderLocked(_inventoryPriceData.GetPrice(slot.Id));
            }
            else if (slot.ItemStack == null)
            {
                slotView.RenderEmptyOpened();
            }
            else
            {
                slotView.RenderOpened(
                    _staticDataService.GetSpriteByType(slot.ItemStack.Type),
                    slot.ItemStack.Count);
            }
        }

        private void RefreshInfo()
        {
            _inventoryInfoView.DrawMoney(_wallet.Coins);
            _inventoryInfoView.DrawWeight(_inventorySystem.GetTotalWeight());
        }

        public void Dispose()
        {
            _inventoryActionsView.ActionClicked -= OnActionClicked;
        }

        public void Initialize()
        {
            _inventoryActionsView.ActionClicked += OnActionClicked;
            Refresh();
        }
    }
}