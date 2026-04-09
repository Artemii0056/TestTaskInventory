using Core.Inventory;
using Core.Systems;
using Core.Wallets;
using GameSaveDatas;
using Infrastructure.ResourceLoad;
using Infrastructure.StaticData;
using Services;
using Services.AddMoneyServices;
using Services.AddRandomAmmoServices;
using Services.AddRandomItemServices;
using Services.AmmoFactories;
using Services.Debbuger;
using Services.DeleteRandomItemServices;
using Services.GameSaveDatas;
using Services.IdGenerator;
using Services.InventoryDataFactoris;
using Services.InventoryUnlockServices;
using Services.ItemsFactory;
using Services.RandomServices;
using Services.SaveProgressServices;
using Services.ShootRandomWeaponServices;
using StateMachine;
using StateMachine.States;
using UI.Factories;
using UI.InventoryScreen.Presenters;
using UI.InventoryScreen.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope //TODO Убрать из папки DI кинутьв Скрипты
{
    [SerializeField] private InventoryActionsView _inventoryActionsView;
    [SerializeField] private InventoryGridView _inventoryGridView;
    [SerializeField] private InventoryInfoView _inventoryInfoView;

    protected override void Configure( IContainerBuilder builder)
    {
        builder.RegisterComponent(_inventoryActionsView);
        builder.RegisterComponent(_inventoryGridView);
        builder.RegisterComponent(_inventoryInfoView);

        RegisterFactories(builder);
        
        RegisterServices(builder);
        
        RegisterSaveServices(builder);

        builder.Register<InventorySystem>(Lifetime.Singleton);

        builder.Register<InventoryScreenPresenter>(Lifetime.Singleton);
        
        builder.Register<InventoryData>(resolver =>
        {
            IInventoryDataProvider provider = resolver.Resolve<IInventoryDataProvider>();
            return provider.CreateOrLoad();
        }, Lifetime.Singleton);
        
        builder.Register<StateFactory>(Lifetime.Singleton).As<IStateFactory>();
        builder.Register<GameStateMachine>(Lifetime.Singleton).As<IGameStateMachine>();

        builder.Register<BootstrapState>(Lifetime.Singleton);
        builder.Register<LoadProgressState>(Lifetime.Singleton);
        builder.Register<GameState>(Lifetime.Singleton);
        builder.Register<SaveProgressState>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<GameEntryPoint>();
    }

    private void RegisterSaveServices(IContainerBuilder builder)
    {
        builder.Register<JsonSaveLoadAdapter>(Lifetime.Singleton).As<ISaveLoadAdapter>();
        builder.Register<InventorySaveMapper>(Lifetime.Singleton).As<IInventorySaveMapper>();
        builder.Register<GameSaveService>(Lifetime.Singleton).As<IGameSaveService>();
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.Register<IWallet>(_ => new Wallet(0), Lifetime.Singleton);
        builder.Register<RandomService>(Lifetime.Singleton).As<IRandomService>();
        builder.Register<StaticDataService>(Lifetime.Singleton).As<IStaticDataService>();
        builder.Register<DebugMessageService>(Lifetime.Singleton).As<IDebugMessageService>();
        builder.Register<ResourceLoader>(Lifetime.Singleton).As<IResourceLoader>();
        builder.Register<UniqueIdService>(Lifetime.Singleton).As<IUniqueIdService>();
        builder.Register<IInventoryUnlockService, InventoryUnlockService>(Lifetime.Singleton);
        
        builder.Register<IInventoryPriceData, InventoryPriceData>(Lifetime.Singleton);
        builder.Register<IAddMoneyService, AddMoneyService>(Lifetime.Singleton);
        builder.Register<IAddRandomAmmoService, AddRandomAmmoService>(Lifetime.Singleton);
        builder.Register<IAddRandomItemService, AddRandomItemService>(Lifetime.Singleton);
        builder.Register<IShootRandomWeaponService, ShootRandomWeaponService>(Lifetime.Singleton);
        builder.Register<IDeleteRandomItemService, DeleteRandomItemService>(Lifetime.Singleton);
        builder.Register<IInventorySlotRenderService, InventorySlotRenderService>(Lifetime.Singleton);
        
        builder.Register<ISaveProgressService, SaveProgressService>(Lifetime.Singleton);
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<AmmoFactory>(Lifetime.Singleton).As<IAmmoFactory>();
        builder.Register<ItemFactory>(Lifetime.Singleton).As<IItemFactory>();
        
        builder.Register<InventoryDataFactory>(Lifetime.Singleton).As<IInventoryDataFactory>();
        builder.Register<InventoryDataProvider>(Lifetime.Singleton).As<IInventoryDataProvider>();
        builder.Register<InventorySlotViewFactory>(Lifetime.Singleton);
    }
}
