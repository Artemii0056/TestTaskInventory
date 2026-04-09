using Core.Inventory;
using UI.InventoryScreen.Views;

namespace Services
{
    public interface IInventorySlotRenderService
    {
        void Render(InventorySlotData slot, InventorySlotView slotView);
    }
}