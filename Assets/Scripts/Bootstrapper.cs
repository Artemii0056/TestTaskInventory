using Core;
using Core.Architecture;
using DefaultNamespace;
using Infrastructure.ResourceLoad;
using Infrastructure.StaticData;
using Inventories.Configs.Ammo.AmmoFactories;
using Inventories.View;
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
        
        IUniqueIdService uniqueIdService = new UniqueIdService();
        IInventoryFactory factory = new InventorySystemFactory(uniqueIdService);
        
        ResourceLoader resourceLoader = new ResourceLoader();
        IStaticDataService staticDataService = new StaticDataService(resourceLoader);
        InventorySlotViewFactory inventorySlotViewFactory = new InventorySlotViewFactory(_inventorySlotViewPrefab, staticDataService);
            
        ItemCatalog itemCatalog = new ItemCatalog(staticDataService);
        IRandomService randomService = new RandomService();
        IAmmoFactory ammoFactory = new AmmoFactory(randomService,itemCatalog );
            
        InventorySystem inventorySystem = factory.Create(50, _openedCount);
        IDebugMessageService debugMessageService = new DebugMessageService();
        
        Wallet wallet = new Wallet(0);

        InventoryScreenPresenter presenter = new InventoryScreenPresenter(_inventoryActionsView, _inventoryGridView, _inventoryInfoView, inventorySystem, wallet, ammoFactory, debugMessageService, inventorySlotViewFactory);
        _inventoryScreenPresenter = presenter;
        
        presenter.Refresh();
    }

    private void OnDisable()
    {
        _inventoryScreenPresenter.Dispose();
    }
}