using Core.Systems;

namespace Services.InventoryFactory
{
    public interface IInventoryFactory
    {
        InventorySystem Create(int allCount, int openedCount);
    }
}