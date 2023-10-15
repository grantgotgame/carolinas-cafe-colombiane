using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
    public static Loader Instance { get; private set; }

    [SerializeField] private Animator transition;
    [SerializeField, Range(0f, 5f)] private float transitionSeconds;

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayMinigame() {
        StartCoroutine(LoadLevel(3));
    }

    public void GoBackToMainScene() {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadNewDay() {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int sceneIndex) {
        // play animation
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSeconds);

        if (sceneIndex == 2) AudioManager.Instance.PlayGameLoopBackground();
        if (sceneIndex == 3) AudioManager.Instance.PlayMiniGameBackground();
        SceneManager.LoadScene(sceneIndex);
    }

    private void Update() {
        /*
        if (Input.GetKeyDown(KeyCode.P)) {
            PlayMinigame();
        } else if (Input.GetKeyDown(KeyCode.L)) {
            GoBackToMainScene();
        }
        */
    }
}
