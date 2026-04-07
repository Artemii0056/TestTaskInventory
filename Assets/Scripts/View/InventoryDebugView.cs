using Core.Architecture;
using UnityEngine;

namespace View
{
    public class InventoryDebugView : MonoBehaviour
    {
        private InventorySystem _inventorySystem;

        public void Construct(InventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }
    }
}