using System;

namespace GameSaveDatas
{
    [Serializable]
    public class GameSaveData
    {
        public int Coins;
        public InventorySaveData Inventory;
    }
}