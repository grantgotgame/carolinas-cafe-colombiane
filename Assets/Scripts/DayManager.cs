using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [SerializeField] private DialogBehaviour dialogBehaviour;
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
        if (result == string.Empty) {
            DialogBehaviour.Instance.StartDialog(dialogGraph[currentDialog]);
        } else {
            DialogBehaviour.Instance.StartDialog(dialogGraph[currentDialog].nextDialog, result);
        }
    }

    public void OnNodeGraphFinished() {
    }
}
