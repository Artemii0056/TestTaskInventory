using Core.Inventory;

namespace Services.InventoryDataFactoris
{
    public interface IInventoryDataFactory
    {
        InventoryData Create();
    }
}