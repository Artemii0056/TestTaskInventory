using Core;
using Core.Inventory;

namespace Services.AmmoFactories
{
    public interface IAmmoFactory
    {
        ItemStack CreateRandom();
    }
}