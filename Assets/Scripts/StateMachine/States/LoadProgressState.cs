using Core.Inventory;
using Core.Wallets;
using Services;

namespace StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IInventoryDataProvider _inventoryDataProvider;
        private readonly IGameStateMachine _gameStateMachine;

        public LoadProgressState(
            IInventoryDataProvider inventoryDataProvider,
            IGameStateMachine gameStateMachine)
        {
            _inventoryDataProvider = inventoryDataProvider;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            InventoryData inventoryData = _inventoryDataProvider.CreateOrLoad();
            //IWallet wallet = //из сохранений
            _gameStateMachine.Enter<InventoryState, InventoryData>(inventoryData);
        }

        public void Exit()
        {
        }
    }
}