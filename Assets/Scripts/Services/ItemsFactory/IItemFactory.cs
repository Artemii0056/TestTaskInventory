using Core;

namespace Services.ItemsFactory
{
    public interface IItemFactory
    {
        ItemStack CreateRandom();
    }
}