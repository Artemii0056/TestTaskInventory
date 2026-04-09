using Core.Inventory;
using Core.Wallets;
using GameSaveDatas;
using Services.GameSaveDatas;
using Services.InventoryDataFactoris;

namespace StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameSaveService _gameSaveService;
        private readonly IInventoryDataFactory _inventoryDataFactory;
        private readonly IInventorySaveMapper _inventorySaveMapper;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWallet _wallet;

        public LoadProgressState(
            IGameSaveService gameSaveService,
            IInventoryDataFactory inventoryDataFactory,
            IInventorySaveMapper inventorySaveMapper,
            IGameStateMachine gameStateMachine, 
            IWallet wallet)
        {
            _gameSaveService = gameSaveService;
            _inventoryDataFactory = inventoryDataFactory;
            _inventorySaveMapper = inventorySaveMapper;
            _gameStateMachine = gameStateMachine;
            _wallet = wallet;
        }

        public void Enter()
        {
            GameSaveData saveData = _gameSaveService.Load();

            InventoryData inventoryData;

            if (saveData == null)
            {
                inventoryData = _inventoryDataFactory.Create();
            }
            else
            {
                inventoryData = saveData.Inventory != null
                    ? _inventorySaveMapper.ToRuntimeData(saveData.Inventory)
                    : _inventoryDataFactory.Create();

            _wallet.SetCoins(saveData.Coins);
            }


            GameRuntimeData runtimeData = new GameRuntimeData(inventoryData);

            _gameStateMachine.Enter<GameState, GameRuntimeData>(runtimeData);
        }

        public void Exit()
        {
        }
    }
}