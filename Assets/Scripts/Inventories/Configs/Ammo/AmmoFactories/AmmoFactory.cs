using System.Collections.Generic;
using System.Linq;
using Core;
using DefaultNamespace;
using UnityEngine;

namespace Inventories.Ammo.AmmoFactories
{
    public class AmmoFactory : IAmmoFactory
    {
        private readonly IRandomService _randomService;
        private readonly List<AmmoConfig> _ammoConfigs;
        
        public AmmoFactory(IRandomService randomService, ItemCatalog itemCatalog)
        {
            _randomService = randomService;
            _ammoConfigs = itemCatalog.AmmoConfigs.ToList();
        }
        
        public ItemStack CreateRandom()
        {
            int count = _randomService.GenerateValue(10, 30);
            AmmoConfig ammoConfig = _ammoConfigs[_randomService.GenerateValue(_ammoConfigs.Count)];
            
            return new ItemStack(ammoConfig.InventoryItemData.Type, count); 
        }
    }
    
    public interface IAmmoFactory
    {
        ItemStack CreateRandom();
    }

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

    public interface IRandomService
    {
        int GenerateValue(int min, int max);
        int  GenerateValue(int max);
    }
}