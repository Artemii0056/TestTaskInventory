using Core.Architecture;

namespace Services.InventoryFactory
{
    public interface IInventoryFactory
    {
        InventorySystem Create(int allCount, int openedCount);
    }
}