using Core.Architecture;

namespace DefaultNamespace
{
    public interface IInventoryFactory
    {
        InventorySystem Create(int allCount, int openedCount);
    }
}