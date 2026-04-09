namespace GameSaveDatas
{
    public interface ISaveLoadAdapter
    {
        bool HasSave();
        void Save(GameSaveData data);
        GameSaveData Load();
    }
}
