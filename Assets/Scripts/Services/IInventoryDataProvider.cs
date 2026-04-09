using Core.Inventory;

namespace Services
{
    public interface IInventoryDataProvider
    {
        InventoryData CreateOrLoad();
    }
}