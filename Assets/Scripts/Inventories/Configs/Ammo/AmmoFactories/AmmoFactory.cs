using System.Collections.Generic;
using System.Linq;
using Core;

namespace Inventories.Configs.Ammo.AmmoFactories
{
    public class AmmoFactory : IAmmoFactory
    {
        private const int MaxAmmoCount = 30;
        private const int MonAmmoCount = 10;
        
        private readonly IRandomService _randomService;
        private readonly List<AmmoConfig> _ammoConfigs;

        public AmmoFactory(IRandomService randomService, ItemCatalog itemCatalog)
        {
            _randomService = randomService;
            _ammoConfigs = itemCatalog.AmmoConfigs.ToList();
        }
        
        public ItemStack CreateRandom()
        {
            int count = _randomService.GenerateValue(MonAmmoCount, MaxAmmoCount);
            AmmoConfig ammoConfig = _ammoConfigs[_randomService.GenerateValue(_ammoConfigs.Count)];
            
            return new ItemStack(ammoConfig.InventoryItemData.Type, count); 
        }
    }
}