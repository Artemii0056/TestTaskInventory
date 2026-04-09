using Core.Inventory;
using Core.Systems;
using UI.InventoryScreen.Presenters;

namespace StateMachine.States
{
    public class InventoryState : IPayloadState<InventoryData>
    {
        private readonly InventorySystem _inventorySystem;
        private readonly InventoryScreenPresenter _inventoryScreenPresenter;

        public InventoryState(
            InventorySystem inventorySystem,
            InventoryScreenPresenter inventoryScreenPresenter)
        {
            _inventorySystem = inventorySystem;
            _inventoryScreenPresenter = inventoryScreenPresenter;
        }

        public void Enter(InventoryData payload)
        {
            _inventorySystem.Initialize(payload);
            _inventoryScreenPresenter.Initialize();
        }

        public void Exit()
        {
            _inventoryScreenPresenter.Dispose();
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }
    }
}