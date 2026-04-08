using Core;
using Core.Architecture;
using Infrastructure.ResourceLoad;
using Infrastructure.StaticData;
using Inventories.Configs.Ammo.AmmoFactories;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private InventoryActionsView _inventoryActionsView;
    [SerializeField] private InventoryGridView _inventoryGridView;
    [SerializeField] private InventoryInfoView _inventoryInfoView;
    
    [SerializeField] private InventorySlotView _inventorySlotViewPrefab;

    private int _openedCount;
    
    private InventoryScreenPresenter _inventoryScreenPresenter;
    
    private void Start()
    {
        _openedCount = 15;
        Init();
    }

    public void Init()
    {
        ResourceLoader resourceLoader = new ResourceLoader();
        IStaticDataService staticDataService = new StaticDataService(resourceLoader);
        IRandomService randomService = new RandomService();
        IItemFactory itemFactory = new ItemFactory(staticDataService, randomService);
        
        IUniqueIdService uniqueIdService = new UniqueIdService();
        
        IInventoryFactory factory = new InventorySystemFactory(uniqueIdService, staticDataService, randomService);
        InventorySlotViewFactory inventorySlotViewFactory = new InventorySlotViewFactory(_inventorySlotViewPrefab, staticDataService);
            
        ItemCatalog itemCatalog = new ItemCatalog(staticDataService);
        IAmmoFactory ammoFactory = new AmmoFactory(randomService,itemCatalog );
            
        InventorySystem inventorySystem = factory.Create(50, _openedCount);
        IDebugMessageService debugMessageService = new DebugMessageService();
        
        Wallet wallet = new Wallet(0);

        InventoryScreenPresenter presenter = new InventoryScreenPresenter(_inventoryActionsView, _inventoryGridView, _inventoryInfoView, inventorySystem, wallet, ammoFactory, debugMessageService, inventorySlotViewFactory, itemFactory, randomService);
        _inventoryScreenPresenter = presenter;
        
        presenter.Refresh();
    }

    private void OnDisable()
    {
        _inventoryScreenPresenter.Dispose();
    }
}