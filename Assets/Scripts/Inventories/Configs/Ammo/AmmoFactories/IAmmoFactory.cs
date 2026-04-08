using Core;

namespace Inventories.Configs.Ammo.AmmoFactories
{
    public interface IAmmoFactory
    {
        ItemStack CreateRandom();
    }
}