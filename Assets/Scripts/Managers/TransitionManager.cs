using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransitionManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI dayBefore;
    [SerializeField] private TextMeshProUGUI dayAfter;

    [SerializeField, Range(0f, 5f)] private float transitionDelay;

    private DayManager dayManager;

    private void Start() {
        if (DayManager.Instance != null) {
            Debug.Log("There's Manager");
            int day = DayManager.Instance.GetDay();
            dayBefore.text = day.ToString();
            dayAfter.text = (day + 1).ToString();
        }

        StartCoroutine(TransitionToNewDay());
    }

    IEnumerator TransitionToNewDay() {
        yield return new WaitForSeconds(transitionDelay);

        Loader.Instance.GoBackToMainScene();
    }
}
