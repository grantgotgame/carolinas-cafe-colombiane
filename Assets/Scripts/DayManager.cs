using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [SerializeField, Range(0f, 5f)] private float transitionDelay;

    [SerializeField] private List<DialogNodeGraph> dialogGraph;

    private int currentDialog;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Instance.OnNodeGraphStarted();
            Destroy(gameObject);
        }
    }

    private void Start() {
        
        currentDialog = 0;
        //dialogBehaviour.StartDialog(dialogGraph[currentDialog]);
        OnNodeGraphStarted();
    }

    public void OnNodeGraphStarted() {
        string result = MiniGameResult.GetResult();
        MiniGameResult.ResetMinigame();
        if (result == string.Empty) {
            DialogBehaviour.Instance.StartDialog(dialogGraph[currentDialog]);
        } else {
            DialogBehaviour.Instance.StartDialog(dialogGraph[currentDialog].nextDialog, result);
        }
    }

    public void OnNodeGraphFinished() {
        DialogNodeGraph currentNodeGraph = DialogBehaviour.Instance.GetCurrentDialogNodeGraph();
        if (dialogGraph.Contains(currentNodeGraph)) {
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
        currentDialog++;

        yield return new WaitForSeconds(transitionDelay);

        OnNodeGraphStarted();
    }
}
