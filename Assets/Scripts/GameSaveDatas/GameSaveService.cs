using GameSaveDatas;

namespace Infrastructure.SaveLoad
{
    public class GameSaveService : IGameSaveService
    {
        private readonly ISaveLoadService _saveLoadService;

        public GameSaveService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public bool HasSave()
        {
            return _saveLoadService.HasSave();
        }

        public GameSaveData Load()
        {
            return _saveLoadService.Load();
        }

        public void Save(GameSaveData data)
        {
            _saveLoadService.Save(data);
        }
    }
}