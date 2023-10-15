using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransitionManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI dayBefore;
    [SerializeField] private TextMeshProUGUI dayAfter;

    [SerializeField, Range(0f, 5f)] private float transitionDelay;

    private DayManager dayManager;
    private AudioSource transitionSound;

    private void Start() {
        if (DayManager.Instance != null) {
            int day = DayManager.Instance.GetDay();
            dayBefore.text = (day - 1).ToString();
            dayAfter.text = (day).ToString();
        }

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("GameVolume");

        AudioManager.Instance.LowerGameplayMusicVolume();

        StartCoroutine(TransitionToNewDay());
    }

    IEnumerator TransitionToNewDay() {
        yield return new WaitForSeconds(transitionDelay);

        AudioManager.Instance.ResumeGameplayMusicVolume();

        Loader.Instance.GoBackToMainScene();
    }
}
