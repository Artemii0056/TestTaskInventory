using System.Collections.Generic;
using Infrastructure.StaticData;
using Inventories;
using Inventories.Configs.Ammo;
using Inventories.Configs.Armors;
using Inventories.Configs.Weapons;

public class ItemCatalog : IItemCatalog 
{
    public IReadOnlyCollection<ArmorConfig> ArmorConfigs;
    public IReadOnlyCollection<AmmoConfig> AmmoConfigs;
    public IReadOnlyCollection<WeaponConfig> WeaponConfigs;
        
    private readonly IStaticDataService _staticDataService;

    public ItemCatalog(IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
            
        ArmorConfigs = _staticDataService.GetArmorConfigs();
        AmmoConfigs = _staticDataService.GetAmmoConfigs();
        WeaponConfigs = _staticDataService.GetWeaponConfigs();
        
       // ArmorItemTypes = ArmorConfigs.
    }

    public List<InventoryItemType> ArmorItemTypes;
}

public interface IItemCatalog
{
}