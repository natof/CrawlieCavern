using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static IEnumerator LoadGameScene() {
        AsyncOperation scene = SceneManager.LoadSceneAsync("game", new LoadSceneParameters());
        scene.allowSceneActivation = true;
        return null;
    }
}