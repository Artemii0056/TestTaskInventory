using Core;
using Core.Inventory;

namespace Services.ItemsFactory
{
    public interface IItemFactory
    {
        ItemStack CreateRandom();
    }
}