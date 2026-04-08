using Core;

namespace Services.AmmoFactories
{
    public interface IAmmoFactory
    {
        ItemStack CreateRandom();
    }
}