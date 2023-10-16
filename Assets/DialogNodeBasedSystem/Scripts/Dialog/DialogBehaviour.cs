using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Character = CharacterDictionarySO.Character;

namespace cherrydev
{
    public class DialogBehaviour : MonoBehaviour
    {
        public static DialogBehaviour Instance;

        [SerializeField] private float dialogCharDelay;
        [SerializeField] private List<KeyCode> nextSentenceKeyCode;

        //[SerializeField] private UnityEvent onDialogStart;
        [SerializeField] private UnityEvent onDialogFinished;

        [SerializeField] private CharacterDictionarySO characterDictionarySO;

        private DialogNodeGraph currentNodeGraph;
        private Node currentNode;
        private Animator customerAnimator;
        private bool skipTextAvailable;
        private bool skipText;

        public static event Action<Character, Character> OnDialogStart;

        public static event Action<DialogData> OnSentenceNodeActive;

        public static event Action OnDialogSentenceEnd;

        public static event Action<Character, Character, bool> OnSentenceNodeStart;

        public static event Action<DialogData> OnAnswerNodeActive;

        public static event Action<int, int, AnswerNode> OnAnswerButtonSetUp;

        public static event Action<int> OnAnswerNodeButtonActivate;

        public static event Action<Character, Character, bool> OnAnswerNodeStart;

        public static event Action<int, string> OnAnswerNodeSetUp;

        public static event Action<string> OnDialogSkipText;

        public static event Action<char, Character> OnDialogTextCharWrote;

        public static event Action<Character, Character, bool> OnLastNode;

        public static event Action OnProcessEndOfDialog;

        private void Awake() {
            if (Instance == null) Instance = this;
            else {
                Instance.SetAnimator();
                Destroy(gameObject);
            }
        }

        private void Start() {
            SetAnimator();
            skipTextAvailable = false;
            skipText = false;
        }

        private void Update() {
            if (skipTextAvailable && IsSkipPressed()) {
                skipText = true;
            }
        }

        public void SetAnimator() {
            customerAnimator = GetComponent<Animator>();
        }

        /// <summary>
        /// Start a dialog
        /// </summary>
        /// <param name="dialogNodeGraph"></param>
        public void StartDialog(DialogNodeGraph dialogNodeGraph, string data = "")
        {
            if (dialogNodeGraph.nodesList == null)
            {
                Debug.LogWarning("Dialog Graph's node list is empty");
                return;
            }

            //onDialogStart?.Invoke();

            currentNodeGraph = dialogNodeGraph;
            if (data == "") {
                currentNode = currentNodeGraph.nodesList[0];
            } else {
                foreach (Node node in currentNodeGraph.nodesList) {
                    if (node.storedData.miniGameValue == data) {
                        // Minigame result return
                        currentNode = node;
                        if (int.TryParse(currentNode.storedData.miniGameValue, out int addValue))
                            PointSystem.AddPoints(node.character, addValue);
                        else {
                            Debug.LogError("Incorrect Minigame Result. See DialogNode for current mistake");
                            return;
                        }
                        break;
                    }
                }
                if (currentNode == null) {
                    Debug.LogError("No node with matching data");
                    return;
                }
            }

            if (currentNode.storedData.miniGameValue.Length == 0)
                StartCoroutine(ProcessStartOfDialogue());
            else {
                if (customerAnimator == null) SetAnimator();
                customerAnimator.SetTrigger("Idle");
                HandleDialogGraphCurrentNode(currentNode);
            }
        }

        IEnumerator ProcessStartOfDialogue() {
            Character character = characterDictionarySO.GetCharacterByID(currentNode.character);
            Character otherCharacter = characterDictionarySO.GetCharacterByID(currentNode.storedData.otherSpeaker);
            OnDialogStart?.Invoke(character, otherCharacter);

            //Wait before character slides
            yield return new WaitForSeconds(0.5f);

            if (customerAnimator == null) SetAnimator();
            customerAnimator.SetTrigger("Slide");
            
            //Wait for the slide to finish
            yield return new WaitForSeconds(1f);

            HandleDialogGraphCurrentNode(currentNode);

        }

        /// <summary>
        /// Processing dialog current node
        /// </summary>
        /// <param name="currentNode"></param>
        private void HandleDialogGraphCurrentNode(Node currentNode)
        {
            StopAllCoroutines();

            Character character = characterDictionarySO.GetCharacterByID(currentNode.character);
            Character otherCharacter = characterDictionarySO.GetCharacterByID(currentNode.storedData.otherSpeaker);
            if (currentNode.GetType() == typeof(SentenceNode))
            {
                SentenceNode sentenceNode = (SentenceNode)currentNode;
                Debug.Log($"Checking choice \"{sentenceNode.storedData.dialogueChoice}\"");
                if (int.TryParse(sentenceNode.storedData.dialogueChoice, out int dialogueChoiceValue)) {
                    Debug.Log("In choice");
                    PointSystem.AddPoints(sentenceNode.character, dialogueChoiceValue);
                }

                OnSentenceNodeActive?.Invoke(currentNode.storedData);
                OnSentenceNodeStart?.Invoke(character, otherCharacter, currentNode.storedData.isOnLeftSide);

                if (sentenceNode.childNode == null) {
                    OnLastNode?.Invoke(character, otherCharacter, currentNode.storedData.isOnLeftSide);
                }

                WriteDialogText(sentenceNode.GetSentenceText(), character);
            }
            else if (currentNode.GetType() == typeof(AnswerNode))
            {
                AnswerNode answerNode = (AnswerNode)currentNode;
                int amountOfActiveButtons = 0;

                OnAnswerNodeActive?.Invoke(answerNode.storedData);

                int[] randAnswer = CreateRandomIndexList();

                for (int i = 0; i < answerNode.childSentenceNodes.Length; i++)
                {
                    if (answerNode.childSentenceNodes[randAnswer[i]] != null)
                    {
                        //Fills the texts in the buttons
                        OnAnswerNodeSetUp?.Invoke(i, answerNode.answers[randAnswer[i]].answer);
                        // Adds Listener to the button
                        OnAnswerButtonSetUp?.Invoke(i, randAnswer[i], answerNode);

                        amountOfActiveButtons++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (amountOfActiveButtons == 0)
                {
                    Debug.Log("This never shows up");
                    onDialogFinished?.Invoke();
                    return;
                }

                OnAnswerNodeButtonActivate?.Invoke(amountOfActiveButtons);
                OnAnswerNodeStart?.Invoke(character, otherCharacter, currentNode.storedData.isOnLeftSide);
            }
        }

        private int[] CreateRandomIndexList() {
            int[] ret = { 0, 1, 2 };

            for (int n = 0; n < ret.Length; n++) {
                int swap = UnityEngine.Random.Range(n, ret.Length);
                int temp = ret[swap];
                ret[swap] = ret[n];
                ret[n] = temp;
            }

            return ret;
        }

        /// <summary>
        /// Setting currentNode field to Node and call HandleDialogGraphCurrentNode method
        /// </summary>
        /// <param name="node"></param>
        public void SetCurrentNodeAndHandleDialogGraph(Node node)
        {
            currentNode = node;
            HandleDialogGraphCurrentNode(this.currentNode);
        }

        /// <summary>
        /// Writing dialog text
        /// </summary>
        /// <param name="text"></param>
        private void WriteDialogText(string text, Character character)
        {
            StartCoroutine(WriteDialogTextRoutine(text, character));
        }

        /// <summary>
        /// Writing dialog text coroutine
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private IEnumerator WriteDialogTextRoutine(string text, Character character)
        {
            skipTextAvailable = true;
            foreach (char textChar in text)
            {
                yield return new WaitForSeconds(dialogCharDelay);
                if (skipText) {
                    OnDialogSkipText?.Invoke(text);
                    break;
                }
                OnDialogTextCharWrote?.Invoke(textChar, character);
            }
            skipText = false;
            skipTextAvailable = false;
            yield return new WaitUntil(() => IsSkipPressed());
            
            CheckForDialogNextNode();
        }

        private bool IsSkipPressed() {
            foreach (KeyCode keyCode in nextSentenceKeyCode) {
                if (Input.GetKeyDown(keyCode)) return true;
            }
            return false;
        }

        /// <summary>
        /// Checking is next dialog node has a child node
        /// </summary>
        private void CheckForDialogNextNode()
        {
            if (currentNode.GetType() == typeof(SentenceNode))
            {
                SentenceNode sentenceNode = (SentenceNode)currentNode;
                OnDialogSentenceEnd?.Invoke();

                if (sentenceNode.childNode != null) {
                    currentNode = sentenceNode.childNode;
                    HandleDialogGraphCurrentNode(currentNode);
                }
                else
                {
                    if (currentNode.storedData.dialogueChoice == "Before Minigame") {
                        OnProcessEndOfDialog?.Invoke();
                        onDialogFinished?.Invoke();
                    } else {
                        StartCoroutine(ProcessEndOfCharacterDialogue());
                    }
                }
            }
        }

        IEnumerator ProcessEndOfCharacterDialogue() {
            OnProcessEndOfDialog?.Invoke();

            yield return new WaitForSeconds(0.5f);

            if (customerAnimator == null) SetAnimator();
            customerAnimator.SetTrigger("Slide");

            yield return new WaitForSeconds(1f);
            onDialogFinished?.Invoke();
        }

        /// <summary>
        /// Adding listener to OnDialogFinished UnityEvent
        /// </summary>
        /// <param name="action"></param>
        public void AddListenerToOnDialogFinished(UnityAction action)
        {
            onDialogFinished.AddListener(action);
        }

        public DialogNodeGraph GetCurrentDialogNodeGraph() {
            return currentNodeGraph;
        }
    }
}