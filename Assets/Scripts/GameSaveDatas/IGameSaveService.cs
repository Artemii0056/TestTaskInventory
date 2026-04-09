using GameSaveDatas;

namespace Infrastructure.SaveLoad
{
    public interface IGameSaveService
    {
        bool HasSave();
        GameSaveData Load();
        void Save(GameSaveData data);
    }
}