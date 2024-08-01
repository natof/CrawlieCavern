using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        public bool GameIsInProgress { get; set; }

        private Player Player { get; set; }

        private void Awake() {
            EventListener.Init();
            Player = GameObject.Find("Player").GetComponent<Player>();
        }

        public void StartGame() {
            GameIsInProgress = true;
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
        }

        public void StopGame() {
            GameIsInProgress = false;
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
        }

        public void ReloadGame() {
            Player.PlayerDataManager.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static GameManager Get() {
            return GameObject.Find("Game").GetComponent<GameManager>();
        }
    }
}