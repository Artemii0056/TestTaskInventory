namespace Inventories.Configs.Ammo.AmmoFactories
{
    public interface IRandomService
    {
        int GenerateValue(int min, int max);
        int  GenerateValue(int max);
    }
}