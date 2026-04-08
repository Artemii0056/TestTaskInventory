using UnityEngine;

namespace Inventories.Configs.Ammo.AmmoFactories
{
    public class RandomService : IRandomService
    {
        public int  GenerateValue(int min, int max)
        {
            if (min >= max)
                throw new System.Exception("min is greater than max");
            
            return Random.Range(min, max);
        }
        
        public int  GenerateValue(int max)
        {
            return Random.Range(0, max);
        }
    }
}