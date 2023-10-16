using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
    public static Loader Instance { get; private set; }

    [SerializeField] private Animator transition;
    [SerializeField, Range(0f, 5f)] private float transitionSeconds;

    public bool IsInTransition { get; private set; }

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        IsInTransition = false;
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

    public void LoadEndGame() {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int sceneIndex) {
        // play animation
        transition.SetTrigger("Start");

        if (SceneManager.GetActiveScene().buildIndex == 1)
            SelectMusicBasedOnDay(sceneIndex);

        yield return new WaitForSeconds(transitionSeconds);

        if (SceneManager.GetActiveScene().buildIndex != 1)
            SelectMusicBasedOnDay(sceneIndex);

        if (sceneIndex == 3) AudioManager.Instance.PlayMiniGameBackground();
        SceneManager.LoadScene(sceneIndex);
    }

    private void SelectMusicBasedOnDay(int sceneIndex) {
        if (sceneIndex == 2) {
            if (DayManager.Instance == null || DayManager.Instance.GetDay() == 1 || DayManager.Instance.GetDay() == 5) {
                AudioManager.Instance.PlayGameLoopBackground();
            } else if (
                DayManager.Instance.GetDay() == 2) {
                AudioManager.Instance.PlaySadBackground();
            } else {
                AudioManager.Instance.PlayMiniGameBackground();
            }
        }

        if (sceneIndex == 1) {
            AudioManager.Instance.PlayGameLoopBackground();
        }
    }

    private void Update() {
        /*
        if (Input.GetKeyDown(KeyCode.P)) {
            PlayMinigame();
        } else if (Input.GetKeyDown(KeyCode.L)) {
            GoBackToMainScene();
        }
        */
        if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) IsInTransition = true;
        else IsInTransition = false;
    }
}
