using Core.Inventory;
using Core.Systems;

namespace Services.InventoryFactory
{
    public interface IInventorySystemFactory
    {
        InventorySystem Create(InventoryData data);
    }
}