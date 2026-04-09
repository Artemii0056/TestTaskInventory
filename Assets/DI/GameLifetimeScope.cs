using Core.Inventory;
using Core.Systems;
using Core.Wallets;
using GameSaveDatas;
using Infrastructure.SaveLoad;
using Infrastructure.StaticData;
using Services;
using Services.AmmoFactories;
using Services.Debbuger;
using Services.InventoryDataFactoris;
using Services.ItemsFactory;
using Services.RandomServices;
using UI.Factories;
using UI.InventoryScreen.Presenters;
using UI.InventoryScreen.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private InventoryActionsView _inventoryActionsView;
    [SerializeField] private InventoryGridView _inventoryGridView;
    [SerializeField] private InventoryInfoView _inventoryInfoView;

    protected override void Configure(IContainerBuilder builder)
    {
        // scene views
        builder.RegisterComponent(_inventoryActionsView);
        builder.RegisterComponent(_inventoryGridView);
        builder.RegisterComponent(_inventoryInfoView);

        RegisterFactories(builder);
        
        RegisterServices(builder);
        
        RegisterSaveServices(builder);

        builder.Register<InventorySystem>(Lifetime.Singleton);

        builder.Register<InventoryScreenPresenter>(Lifetime.Singleton);
        
        builder.Register<IInventoryDataFactory, InventoryDataFactory>(Lifetime.Singleton);
        
        builder.Register<IInventoryDataProvider, InventoryDataProvider>(Lifetime.Singleton);

        builder.Register<InventoryData>(resolver =>
        {
            IInventoryDataProvider provider = resolver.Resolve<IInventoryDataProvider>();
            return provider.CreateOrLoad();
        }, Lifetime.Singleton);
    }

    private void RegisterSaveServices(IContainerBuilder builder)
    {
        builder.Register<JsonSaveLoadService>(Lifetime.Singleton).As<ISaveLoadService>();
        builder.Register<InventorySaveMapper>(Lifetime.Singleton).As<IInventorySaveMapper>();
        builder.Register<GameSaveService>(Lifetime.Singleton).As<IGameSaveService>();
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.Register<Wallet>(Lifetime.Singleton).As<IWallet>();
        builder.Register<RandomService>(Lifetime.Singleton).As<IRandomService>();
        builder.Register<StaticDataService>(Lifetime.Singleton).As<IStaticDataService>();
        builder.Register<DebugMessageService>(Lifetime.Singleton).As<IDebugMessageService>();
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<AmmoFactory>(Lifetime.Singleton).As<IAmmoFactory>();
        builder.Register<ItemFactory>(Lifetime.Singleton).As<IItemFactory>();
        builder.Register<InventorySlotViewFactory>(Lifetime.Singleton);
    }
}
