using UnityEngine;

namespace DefaultNamespace {
    public class MenuCanva : Canva {
        public GameObject loadingScreen;
        public GameObject Menu;

        public void OnClick() {
            Menu.SetActive(false);
            loadingScreen.SetActive(true);
            StartCoroutine(SceneController.LoadGameScene());
        }
    }
}