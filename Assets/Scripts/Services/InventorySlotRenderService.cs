using Core.Inventory;
using Infrastructure.StaticData;
using Services.InventoryUnlockServices;
using UI.InventoryScreen.Views;

namespace Services
{
    public class InventorySlotRenderService : IInventorySlotRenderService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInventoryPriceData _inventoryPriceData;

        public InventorySlotRenderService(
            IStaticDataService staticDataService,
            IInventoryPriceData inventoryPriceData)
        {
            _staticDataService = staticDataService;
            _inventoryPriceData = inventoryPriceData;
        }

        public void Render(InventorySlotData slot, InventorySlotView slotView)
        {
            if (slot.IsUnlocked == false)
            {
                slotView.RenderLocked(_inventoryPriceData.GetPrice(slot.Id));
                return;
            }

            if (slot.ItemStack == null)
            {
                slotView.RenderEmptyOpened();
                return;
            }

            slotView.RenderOpened(
                _staticDataService.GetSpriteByType(slot.ItemStack.Type),
                slot.ItemStack.Count);
        }
    }
}