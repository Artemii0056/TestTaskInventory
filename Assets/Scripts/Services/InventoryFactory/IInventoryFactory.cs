using Core.Architecture;

public interface IInventoryFactory
{
    InventorySystem Create(int allCount, int openedCount);
}