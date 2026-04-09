using Core.Inventory;
using Services;

namespace StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IInventoryDataProvider _inventoryDataProvider;
        private readonly GameStateMachine _gameStateMachine;

        public LoadProgressState(
            IInventoryDataProvider inventoryDataProvider,
            GameStateMachine gameStateMachine)
        {
            _inventoryDataProvider = inventoryDataProvider;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            InventoryData inventoryData = _inventoryDataProvider.CreateOrLoad();
            _gameStateMachine.Enter<InventoryState, InventoryData>(inventoryData);
        }

        public void Exit()
        {
        }
    }
}