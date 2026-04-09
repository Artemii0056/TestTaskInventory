using System;
using Core;

namespace GameSaveDatas
{
    [Serializable]
    public class InventorySlotSaveData
    {
        public int Id;
        public bool IsUnlocked;
        public bool HasItem;
        public InventoryItemType ItemType;
        public int Count;
    }
}