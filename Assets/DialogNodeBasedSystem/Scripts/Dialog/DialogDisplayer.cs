using System.Collections;
using UnityEngine;

namespace cherrydev
{
    public class DialogDisplayer : MonoBehaviour
    {
        [SerializeField] private SentencePanel dialogSentencePanel;
        [SerializeField] private AnswerPanel dialogAnswerPanel;
        [SerializeField] private DialogBehaviour dialogBehaviour;

        private void OnEnable()
        {
            //dialogBehaviour.AddListenerToOnDialogFinished(DisableDialogPanel);

            DialogBehaviour.OnProcessEndOfDialog += DisableDialogPanel;

            DialogBehaviour.OnDialogStart += dialogSentencePanel.EnterNewCharacter;

            DialogBehaviour.OnAnswerButtonSetUp += SetUpAnswerButtonsClickEvent;

            DialogBehaviour.OnDialogSentenceEnd += dialogSentencePanel.ResetDialogText;

            DialogBehaviour.OnDialogTextCharWrote += dialogSentencePanel.AddCharToDialogText;

            DialogBehaviour.OnSentenceNodeActive += EnableDialogSentencePanel;
            //DialogBehaviour.OnSentenceNodeActive += DisableDialogAnswerPanel;
            DialogBehaviour.OnSentenceNodeStart += dialogSentencePanel.AssignDialogNameTextAndSprite;
            DialogBehaviour.OnAnswerNodeStart += dialogSentencePanel.AssignDialogNameTextAndSprite;

            DialogBehaviour.OnAnswerNodeActive += DisableDialogSentencePanel;
            DialogBehaviour.OnAnswerNodeActive += EnableDialogAnswerPanel;

            DialogBehaviour.OnAnswerNodeButtonActivate += dialogAnswerPanel.EnableCertainAmountOfButtons;

            DialogBehaviour.OnAnswerNodeSetUp += SetUpAnswerDialogPanel;
        }

        private void OnDisable() {
            DialogBehaviour.OnProcessEndOfDialog -= DisableDialogPanel;
            DialogBehaviour.OnAnswerButtonSetUp -= SetUpAnswerButtonsClickEvent;

            DialogBehaviour.OnDialogStart -= dialogSentencePanel.EnterNewCharacter;

            DialogBehaviour.OnDialogSentenceEnd -= dialogSentencePanel.ResetDialogText;

            DialogBehaviour.OnDialogTextCharWrote -= dialogSentencePanel.AddCharToDialogText;

            DialogBehaviour.OnSentenceNodeActive -= EnableDialogSentencePanel;
            //DialogBehaviour.OnSentenceNodeActive -= DisableDialogAnswerPanel;

            DialogBehaviour.OnSentenceNodeStart -= dialogSentencePanel.AssignDialogNameTextAndSprite;
            DialogBehaviour.OnAnswerNodeStart -= dialogSentencePanel.AssignDialogNameTextAndSprite;

            DialogBehaviour.OnAnswerNodeActive -= DisableDialogSentencePanel;
            DialogBehaviour.OnAnswerNodeActive -= EnableDialogAnswerPanel;

            DialogBehaviour.OnAnswerNodeButtonActivate -= dialogAnswerPanel.EnableCertainAmountOfButtons;

            DialogBehaviour.OnAnswerNodeSetUp -= SetUpAnswerDialogPanel;
        }

        /// <summary>
        /// Disable dialog answer and sentence panel
        /// </summary>
        private void DisableDialogPanel() {
            //StartCoroutine(TransitionToMinigame());
            dialogSentencePanel.SetupCharactersWithoutDialogue();
        }
        /*
        IEnumerator TransitionToMinigame()
        {
            yield return new WaitForSeconds(1f);

            Loader.Instance.PlayMinigame();
        }
        */
        /// <summary>
        /// Enable dialog answer panel
        /// </summary>
        public void EnableDialogAnswerPanel(DialogData data) {
            dialogSentencePanel.DeactivateSentence();
            dialogSentencePanel.ActivatePanels(data);
            dialogAnswerPanel.ActivateAnswer(data.isOnLeftSide);
            //ActiveGameObject(dialogAnswerPanel.gameObject, true);
            dialogAnswerPanel.DisableAllButtons();
        }

        /// <summary>
        /// Disable dialog answer panel
        /// </summary>
        public void DisableDialogAnswerPanel()
        {
            ActiveGameObject(dialogAnswerPanel.gameObject, false);
        }

        /// <summary>
        /// Enable dialog sentence panel
        /// </summary>
        public void EnableDialogSentencePanel(DialogData data)
        {
            dialogSentencePanel.ActivatePanels(data);
            dialogSentencePanel.ActivateSentence();
            dialogAnswerPanel.DeactivateAnswer();
            //ActiveGameObject(dialogSentensePanel.gameObject, true);
        }

        /// <summary>
        /// Disable dialog sentence panel
        /// </summary>
        public void DisableDialogSentencePanel(DialogData data)
        {
            dialogSentencePanel.DeactivateSentence();
            //ActiveGameObject(dialogSentensePanel.gameObject, false);
        }

        /// <summary>
        /// Enable or disable game object depends on isActive bool flag
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="isActive"></param>
        public void ActiveGameObject(GameObject gameObject, bool isActive)
        {
            if (gameObject == null)
            {
                Debug.LogWarning("Game object is null");
                return;
            }

            gameObject.SetActive(isActive);
        }

        /// <summary>
        /// Setting up answer button onClick event
        /// </summary>
        /// <param name="btnIndex"></param>
        /// <param name="answerNode"></param>
        public void SetUpAnswerButtonsClickEvent(int btnIndex, int answerIndex, AnswerNode answerNode)
        {
            dialogAnswerPanel.GetButtonByIndex(btnIndex).onClick.AddListener(() =>
            {
                dialogBehaviour.SetCurrentNodeAndHandleDialogGraph(answerNode.childSentenceNodes[answerIndex]);
            });
        }

        /// <summary>
        /// Setting up answer dialog panel
        /// </summary>
        /// <param name="index"></param>
        /// <param name="answerText"></param>
        public void SetUpAnswerDialogPanel(int index, string answerText)
        {
            dialogAnswerPanel.GetButtonTextByIndex(index).text = answerText;
        }
    }
}