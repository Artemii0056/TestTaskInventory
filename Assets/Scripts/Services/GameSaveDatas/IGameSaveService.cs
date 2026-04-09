using GameSaveDatas;

namespace Services.GameSaveDatas
{
    public interface IGameSaveService
    {
        bool HasSave();
        GameSaveData Load();
        void Save(GameSaveData data);
    }
}