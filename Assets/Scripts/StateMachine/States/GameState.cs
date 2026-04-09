using Core.Systems;
using UI.InventoryScreen.Presenters;

namespace StateMachine.States
{
    public class GameState : IPayloadState<GameRuntimeData>
    {
        private readonly InventorySystem _inventorySystem;
        private readonly InventoryScreenPresenter _inventoryScreenPresenter;

        public GameState(
            InventorySystem inventorySystem,
            InventoryScreenPresenter inventoryScreenPresenter)
        {
            _inventorySystem = inventorySystem;
            _inventoryScreenPresenter = inventoryScreenPresenter;
        }

        public void Enter(GameRuntimeData payload)
        {
            _inventorySystem.Initialize(payload.InventoryData);
            _inventoryScreenPresenter.Initialize();
        }

        public void Exit()
        {
            _inventoryScreenPresenter.Dispose();
        }
    }
}