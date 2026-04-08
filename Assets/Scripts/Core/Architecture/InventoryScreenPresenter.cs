using System;
using Inventories;
using Results;
using Results.DefaultNamespace.Results;
using Services.AmmoFactories;
using Services.Debbuger;
using Services.ItemsFactory;
using Services.RandomServices;

namespace Core.Architecture
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
            IRandomService randomService)
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

            _inventoryActionsView.ActionClicked += OnActionClicked;
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
            _inventoryGridView.DeleteAll();

            foreach (InventorySlotData slot in _inventorySystem.Slots)
            {
                InventorySlotView slotView = CreateSlotView(slot);
                _inventoryGridView.Add(slotView);
                slotView.Show(slot.IsUnlocked);
            }
        }

        private void RefreshInfo()
        {
            _inventoryInfoView.DrawMoney(_wallet.Coins);
            _inventoryInfoView.DrawWeight(_inventorySystem.GetTotalWeight());
        }

        private InventorySlotView CreateSlotView(InventorySlotData slot)
        {
            if (slot.ItemStack == null)
                return _inventorySlotViewFactory.Create(slot.IsUnlocked);

            return _inventorySlotViewFactory.Create(
                slot.IsUnlocked,
                slot.ItemStack.Type,
                slot.ItemStack.Count);
        }

        public void Dispose()
        {
            _inventoryActionsView.ActionClicked -= OnActionClicked;
        }
    }
}