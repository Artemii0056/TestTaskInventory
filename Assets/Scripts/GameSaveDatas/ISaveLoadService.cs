namespace GameSaveDatas
{
    public interface ISaveLoadService
    {
        bool HasSave();
        void Save(GameSaveData data);
        GameSaveData Load();
    }
}
