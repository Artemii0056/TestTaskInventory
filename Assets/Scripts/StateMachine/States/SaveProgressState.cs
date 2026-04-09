using Core.Wallets;
using GameSaveDatas;
using Infrastructure.SaveLoad;

namespace StateMachine.States
{
    public class SaveProgressState : IPayloadState<GameRuntimeData>
    {
        private readonly IGameSaveService _gameSaveService;
        private readonly IInventorySaveMapper _inventorySaveMapper;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWallet _wallet;

        public SaveProgressState(
            IGameSaveService gameSaveService,
            IInventorySaveMapper inventorySaveMapper,
            IGameStateMachine gameStateMachine, 
            IWallet wallet)
        {
            _gameSaveService = gameSaveService;
            _inventorySaveMapper = inventorySaveMapper;
            _gameStateMachine = gameStateMachine;
            _wallet = wallet;
        }

        public void Enter(GameRuntimeData payload)
        {
            var saveData = new GameSaveData
            {
                Coins = _wallet.Coins,
                Inventory = _inventorySaveMapper.ToSaveData(payload.InventoryData)
            };

            _gameSaveService.Save(saveData);

            _gameStateMachine.Enter<InventoryState, GameRuntimeData>(payload);
        }

        public void Exit()
        {
        }
    }
}