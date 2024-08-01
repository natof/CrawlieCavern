using System.Collections;
using UnityEngine;

public class LaunchGameController : MonoBehaviour {
    public GameObject titleScreen;
    public GameObject audioSource;

    private void Start() {
        StartCoroutine(CloseTitleScreen());
    }

    private IEnumerator CloseTitleScreen() {
        yield return new WaitForSeconds(6);
        titleScreen.SetActive(false);
        audioSource.SetActive(true);
    }
}