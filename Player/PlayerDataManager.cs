using System.IO;
using UnityEngine;

namespace DefaultNamespace {
    public class PlayerDataManager {
        private readonly string _filePath = "./";

        public PlayerDataManager() {
            PlayerData = new PlayerData();
            _filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        }

        public PlayerData PlayerData { get; private set; }

        public void Load() {
            if (!File.Exists(_filePath)) {
                return;
            }
            string json = File.ReadAllText(_filePath);
            PlayerData = JsonUtility.FromJson<PlayerData>(json);
        }

        public void Save() {
            string json = JsonUtility.ToJson(PlayerData);
            File.WriteAllText(_filePath, json);
        }
    }
}