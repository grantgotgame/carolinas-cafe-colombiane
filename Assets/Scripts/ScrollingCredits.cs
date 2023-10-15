using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollingCredits : MonoBehaviour
{
    private ScrollRect scrollingCredits;
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private float scrollSpeed;
    private float screenHeight;

    private void Start() {
        screenHeight = Camera.main.pixelHeight;
        scrollingCredits = GetComponent<ScrollRect>();
    }

    private void Update() {
        if (scrollingCredits.verticalNormalizedPosition > 0) {
            scrollingCredits.verticalNormalizedPosition += -scrollSpeed * Time.deltaTime / screenHeight;
        }
    }
}
