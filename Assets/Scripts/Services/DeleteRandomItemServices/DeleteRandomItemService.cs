using Core.Results;
using Core.Systems;
using Services.DeleteRandomItemServices;

namespace Services.DeleteItemServices
{
    public class DeleteRandomItemService : IDeleteRandomItemService
    {
        private readonly InventorySystem _inventorySystem;

        public DeleteRandomItemService(InventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }

        public DeleteItemResult Delete()
        {
            if (_inventorySystem.TryDeleteItem(out DeleteItemResult result))
            {
                return new DeleteItemResult(
                    result.Count,
                    result.ItemType,
                    result.SlotId,
                    true);
            }

            return new DeleteItemResult(0, 0, -1, false);
        }
    }
}