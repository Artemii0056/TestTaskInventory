using Core;
using Core.Architecture;
using Infrastructure.ResourceLoad;
using Infrastructure.StaticData;
using Inventories.Ammo.AmmoFactories;
using UnityEngine;
using View;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private InventoryDebugView _view;
        
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            ResourceLoader resourceLoader = new ResourceLoader();
            IStaticDataService staticDataService = new StaticDataService(resourceLoader);
            
            ItemCatalog itemCatalog = new ItemCatalog(staticDataService);
            RandomService randomService = new RandomService();
            IAmmoFactory ammoFactory = new AmmoFactory(randomService,itemCatalog );
            
            InventoryData data = new InventoryData();
            InventorySystem inventorySystem = new InventorySystem(data);
            InventoryView view = new InventoryView();
            
            InventoryPresenter inventoryPresenter = new InventoryPresenter(inventorySystem, view, ammoFactory);
            Wallet wallet = new Wallet(10);
            _view.Init(inventoryPresenter, wallet, inventorySystem);
        }
    }
}