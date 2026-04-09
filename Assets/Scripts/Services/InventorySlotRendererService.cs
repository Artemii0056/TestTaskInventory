using Core.Inventory;
using UI.InventoryScreen.Views;

namespace UI.InventoryScreen.Services
{
    public interface IInventorySlotRenderService
    {
        void Render(InventorySlotData slot, InventorySlotView slotView);
    }
}