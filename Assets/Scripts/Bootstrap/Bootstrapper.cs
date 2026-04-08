using Core.Systems;
using Core.Wallets;
using Infrastructure.ResourceLoad;
using Infrastructure.StaticData;
using Services.AmmoFactories;
using Services.Debbuger;
using Services.IdGenerator;
using Services.InventoryFactory;
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

        private int _openedCount;

        private InventoryScreenPresenter _inventoryScreenPresenter;

        private void Start()
        {
            _openedCount = 15;
            Init();
        }

        public void Init()
        {
            Wallet wallet = new Wallet(0);
            ResourceLoader resourceLoader = new ResourceLoader();
            IStaticDataService staticDataService = new StaticDataService(resourceLoader);
            IRandomService randomService = new RandomService();
            IItemFactory itemFactory = new ItemFactory(staticDataService, randomService);

            IUniqueIdService uniqueIdService = new UniqueIdService();

            IInventoryFactory factory = new InventorySystemFactory(uniqueIdService, staticDataService, randomService, wallet);
            InventorySlotViewFactory inventorySlotViewFactory =
                new InventorySlotViewFactory(_inventorySlotViewPrefab);

            IAmmoFactory ammoFactory = new AmmoFactory(randomService, staticDataService);

            InventorySystem inventorySystem = factory.Create(50, _openedCount);
            IDebugMessageService debugMessageService = new DebugMessageService();


            InventoryScreenPresenter presenter = new InventoryScreenPresenter(_inventoryActionsView, _inventoryGridView,
                _inventoryInfoView, inventorySystem, wallet, ammoFactory, debugMessageService, inventorySlotViewFactory,
                itemFactory, randomService, staticDataService);
            _inventoryScreenPresenter = presenter;

            presenter.Refresh();
        }

        private void OnDisable()
        {
            _inventoryScreenPresenter.Dispose();
        }
    }
}