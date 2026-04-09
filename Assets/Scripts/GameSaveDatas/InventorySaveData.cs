using System;
using System.Collections.Generic;

namespace GameSaveDatas
{
    [Serializable]
    public class InventorySaveData
    {
        public List<InventorySlotSaveData> Slots = new();
    }
}