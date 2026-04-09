using System.IO;
using Infrastructure.StaticData;
using UnityEngine;

namespace GameSaveDatas
{
    public class JsonSaveLoadAdapter : ISaveLoadAdapter 
    {
        private string FilePath => Path.Combine(Application.persistentDataPath, Constants.FileNamePath);

        public bool HasSave() => File.Exists(FilePath);

        public void Save(GameSaveData data)
        {
            if (data == null)
            {
                Debug.LogError("Save failed: GameSaveData is null");
                return;
            }

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(FilePath, json);
        }

        public GameSaveData Load()
        {
            if (HasSave() == false)
                return null;

            string json = File.ReadAllText(FilePath);

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonUtility.FromJson<GameSaveData>(json);
        }
    }
}