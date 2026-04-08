using System.Collections.Generic;
using Core;
using Core.Configs;
using Core.Inventory;
using Infrastructure.StaticData;
using Services.RandomServices;

namespace Services.ItemsFactory
{
    public class ItemFactory : IItemFactory
    {
        private readonly IRandomService _randomService;
        
        private readonly List<IItemConfig> _itemConfigs;
        
        public ItemFactory(IStaticDataService staticDataService, IRandomService randomService)
        {
            _randomService = randomService;
            _itemConfigs = new List<IItemConfig>();

            _itemConfigs.AddRange(staticDataService.GetArmorConfigs());
            _itemConfigs.AddRange(staticDataService.GetWeaponConfigs());
        }

        public ItemStack CreateRandom()
        {
            IItemConfig config = _itemConfigs[_randomService.GenerateValue(_itemConfigs.Count)];

            return new ItemStack(config.InventoryItemData.Type, config.InventoryItemData.MaxStack);
        }
    }
}