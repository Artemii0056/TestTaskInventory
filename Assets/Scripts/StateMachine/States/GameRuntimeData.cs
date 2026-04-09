using Core.Inventory;

namespace StateMachine.States
{
    public class GameRuntimeData
    {
        public InventoryData InventoryData { get; }
        public GameRuntimeData(InventoryData inventoryData)
        {
            InventoryData = inventoryData;
        }
    }
}