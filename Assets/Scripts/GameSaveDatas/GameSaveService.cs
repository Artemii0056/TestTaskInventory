namespace GameSaveDatas
{
    public class GameSaveService : IGameSaveService
    {
        private readonly ISaveLoadAdapter _saveLoadAdapter;

        public GameSaveService(ISaveLoadAdapter saveLoadAdapter)
        {
            _saveLoadAdapter = saveLoadAdapter;
        }

        public bool HasSave()
        {
            return _saveLoadAdapter.HasSave();
        }

        public GameSaveData Load()
        {
            return _saveLoadAdapter.Load();
        }

        public void Save(GameSaveData data)
        {
            _saveLoadAdapter.Save(data);
        }
    }
}