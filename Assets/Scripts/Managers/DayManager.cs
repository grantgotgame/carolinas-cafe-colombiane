using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [SerializeField, Range(0f, 5f)] private float transitionDelay;

    public DayDialogue dayOne;
    public DayDialogue dayTwo;
    public DayDialogue dayThree;
    public DayDialogue dayFour;
    public DayDialogue dayFive;

    private DayDialogue currentDay;
    private List<DayDialogue> dayCollection = new List<DayDialogue>();
    private int day;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Instance.OnNodeGraphStarted();
            Destroy(gameObject);
        }
        dayCollection.Add(dayOne);
        dayCollection.Add(dayTwo);
        dayCollection.Add(dayThree);
        dayCollection.Add(dayFour);
        dayCollection.Add(dayFive);
        if (TestScriptDebug.isTesting) {
            day = TestScriptDebug.chooseDay - 1;
            currentDay = dayCollection[day++];
            currentDay.currentDialogue = TestScriptDebug.chooseCharacter;
        } else {
            day = 0;
            currentDay = dayCollection[day++];
        }
    }

    private void Start() {
        OnNodeGraphStarted();
    }

    public void OnNodeGraphStarted() {
        string result = MiniGameResult.GetResult();
        MiniGameResult.ResetMinigame();
        if (DialogBehaviour.Instance != null) {
            DialogBehaviour.Instance.AddListenerToOnDialogFinished(OnNodeGraphFinished);
        }
        if (result == string.Empty) {
            if (currentDay.IsOutOfDialogue()) currentDay = dayCollection[day++];
            DialogBehaviour.Instance.StartDialog(currentDay.GetCurrentConversation());
        } else {
            DialogBehaviour.Instance.StartDialog(currentDay.GetNextConversation(), result);
        }
    }

    public void OnNodeGraphFinished() {
        DialogNodeGraph currentNodeGraph = DialogBehaviour.Instance.GetCurrentDialogNodeGraph();
        if (currentDay.dialogGraphs.Contains(currentNodeGraph)) {
            StartCoroutine(TransitionToMinigame());
        } else {
            StartCoroutine(TransitionToNextCustomer());
        }
    }

    IEnumerator TransitionToMinigame() {
        yield return new WaitForSeconds(transitionDelay);

        Loader.Instance.PlayMinigame();
    }
    IEnumerator TransitionToNextCustomer() {
        currentDay.currentDialogue++;

        yield return new WaitForSeconds(transitionDelay);

        OnNodeGraphStarted();
    }

    public int GetDay() {
        return day;
    }
}

[System.Serializable]
public class DayDialogue {
    public List<DialogNodeGraph> dialogGraphs;
    [HideInInspector] public int currentDialogue = 0;

    public DialogNodeGraph GetCurrentConversation() {
        return dialogGraphs[currentDialogue];
    }

    public DialogNodeGraph GetNextConversation() {
        return dialogGraphs[currentDialogue].nextDialog;
    }
    public bool IsOutOfDialogue() {
        return currentDialogue >= dialogGraphs.Count;
    }
}