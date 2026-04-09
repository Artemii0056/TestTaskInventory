using Core.Inventory;
using Core.Systems;
using Core.Wallets;
using Infrastructure.ResourceLoad;
using Infrastructure.StaticData;
using Services.AmmoFactories;
using Services.Debbuger;
using Services.IdGenerator;
using Services.InventoryDataFactoris;
using Services.InventoryFactory;
using Services.InventoryUnlockServices;
using Services.ItemsFactory;
using Services.RandomServices;
using UI.Factories;
using UI.InventoryScreen.Presenters;
using UI.InventoryScreen.Views;
using UnityEngine;

namespace Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private InventoryActionsView _inventoryActionsView;
        [SerializeField] private InventoryGridView _inventoryGridView;
        [SerializeField] private InventoryInfoView _inventoryInfoView;

        [SerializeField] private InventorySlotView _inventorySlotViewPrefab;

        private InventoryScreenPresenter _inventoryScreenPresenter;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            // Wallet wallet = new Wallet(0);
            // ResourceLoader resourceLoader = new ResourceLoader();
            // IStaticDataService staticDataService = new StaticDataService(resourceLoader);
            // IRandomService randomService = new RandomService();
            // IItemFactory itemFactory = new ItemFactory(staticDataService, randomService);
            // IUniqueIdService uniqueIdService = new UniqueIdService();
            // IInventoryDataFactory inventoryDataFactory = new InventoryDataFactory(staticDataService, uniqueIdService);
            //
            // InventoryPriceData priceData = new InventoryPriceData(staticDataService.InventoryConfig);
            //
            // InventoryData inventoryData = inventoryDataFactory.Create();
            //
            // IInventoryUnlockService unlockService = new InventoryUnlockService(inventoryData, wallet, priceData);
            // IInventorySystemFactory systemFactory = new InventorySystemFactory( staticDataService, randomService, wallet, unlockService);
            // InventorySystem inventorySystem = systemFactory.Create(inventoryData);
            // InventorySlotViewFactory inventorySlotViewFactory = new InventorySlotViewFactory(_inventorySlotViewPrefab);
            //
            // IAmmoFactory ammoFactory = new AmmoFactory(randomService, staticDataService);
            //
            // IDebugMessageService debugMessageService = new DebugMessageService();
            //
            //
            // InventoryScreenPresenter presenter = new InventoryScreenPresenter(_inventoryActionsView, _inventoryGridView,
            //     _inventoryInfoView, inventorySystem, wallet, ammoFactory, debugMessageService, inventorySlotViewFactory,
            //     itemFactory, randomService, staticDataService, unlockService, priceData);
            // _inventoryScreenPresenter = presenter;
            //
            // presenter.Refresh();
        }

        private void OnDisable()
        {
            _inventoryScreenPresenter.Dispose();
        }
    }
}