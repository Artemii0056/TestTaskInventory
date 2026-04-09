using Core.Inventory;
using Core.Results;
using Core.Systems;
using Services.ItemsFactory;

namespace Services.AddRandomItemServices
{
    public class AddRandomItemService : IAddRandomItemService
    {
        private readonly IItemFactory _itemFactory;
        private readonly InventorySystem _inventorySystem;

        public AddRandomItemService(
            IItemFactory itemFactory,
            InventorySystem inventorySystem)
        {
            _itemFactory = itemFactory;
            _inventorySystem = inventorySystem;
        }

        public AddRandomItemResult AddRandom()
        {
            ItemStack stack = _itemFactory.CreateRandom();

            if (_inventorySystem.TryAddItem(stack, out InventorySlotData slot) == false)
                return new AddRandomItemResult(false, stack.Type, -1);

            return new AddRandomItemResult(true, slot.ItemStack.Type, slot.Id);
        }
    }
}