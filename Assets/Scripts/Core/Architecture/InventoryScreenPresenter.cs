using System;
using Inventories;
using Inventories.Configs.Ammo.AmmoFactories;
using Results;
using Results.DefaultNamespace.Results;

namespace Core.Architecture
{
    public class InventoryScreenPresenter
    {
        private readonly InventoryActionsView _inventoryActionsView;
        private readonly InventoryGridView _inventoryGridView;
        private InventoryInfoView _inventoryInfoView;

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
            IDebugMessageService debugMessageService, InventorySlotViewFactory inventorySlotViewFactory,
            IItemFactory itemFactory, IRandomService randomService)
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
                    AddAmmoAction();
                    break;

                case InventoryActionType.AddItem:
                    AddItem();
                    break;

                case InventoryActionType.DeleteItem:
                    DeleteSelectedItem();
                    break;

                case InventoryActionType.Shoot:
                    ShootSelectedWeapon();
                    break;

                case InventoryActionType.AddMoney:
                    IncreaseMoney();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }

            Refresh();
        }

        private void ShootSelectedWeapon()
        {
            ShootResult result = _inventorySystem.TryShootRandomWeapon();
           
            if (result.WeaponType == InventoryItemType.None)
            {
                _debugMessageService.ShowMessage("Нет оружия");
                return;
            }

            if (!result.IsSuccess)
            {
                _debugMessageService.ShowMessage($"Нет патронов для {result.WeaponType}");
                Refresh();
                return;
            }

            _debugMessageService.ShowMessage(
                $"Выстрел из {result.WeaponType}, патроны: {result.AmmoType}, урон: {result.Damage}");

            Refresh();

        }

        private void DeleteSelectedItem()
        {
            if (_inventorySystem.TryDeleteItem(out DeleteItemResult result))
            {
                _debugMessageService.ShowMessage($"Удалено ({result.Count}) [{result.ItemType}] из слота: [{result.SlotId}]");
                return;
            }
            
            _debugMessageService.ShowMessage($"Инвентарь пуст"); 
        }

        private void IncreaseMoney()
        {
            int value = _randomService.GenerateValue(10, 99);

            _wallet.Increase(value);

            _debugMessageService.ShowMessage($"Выводит в консоль сообщение Добавлено ({value}) монет");
        }

        private void AddItem()
        {
            ItemStack stack = _itemFactory.CreateRandom();

            if (_inventorySystem.TryAddItem(stack, out InventorySlotData data) == false)
            {
                _debugMessageService.ShowMessage("Инвентарь полон");
                return;
            }

            _debugMessageService.ShowMessage(
                $"Добавлено ({data.ItemStack.Type}) в слот: {data.Id}");
        }

        private void AddAmmoAction()
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
            _inventoryGridView.DeleteAll();

            foreach (var slot in _inventorySystem.Slots)
            {
                InventorySlotView slotView;

                if (slot.ItemStack == null)
                {
                    slotView = _inventorySlotViewFactory.Create(slot.IsUnlocked);
                    _inventoryGridView.Add(slotView);
                }
                else
                {
                    slotView = _inventorySlotViewFactory.Create(slot.IsUnlocked, slot.ItemStack.Type,
                        slot.ItemStack.Count);
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