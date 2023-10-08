using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace cherrydev
{
    public class AnswerPanel : MonoBehaviour {
        [Header("Left Speaker"), Space()]
        [SerializeField] private GameObject lAnswerObject;
        [SerializeField] private List<Button> lButtons = new List<Button>();
        [SerializeField] private List<TextMeshProUGUI> lBbuttonTexts;
        [Header("Right Speaker"), Space()]
        [SerializeField] private GameObject rAnswerObject;
        [SerializeField] private List<Button> rButtons = new List<Button>();
        [SerializeField] private List<TextMeshProUGUI> rBbuttonTexts;

        private bool isLeftSpeaker;

        /// <summary>
        /// Returning button by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Button GetButtonByIndex(int index)
        {
            return lButtons[index];
        }

        /// <summary>
        /// Returning button text bu index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TextMeshProUGUI GetButtonTextByIndex(int index)
        {
            return lBbuttonTexts[index];
        }

        /// <summary>
        /// Setting UnityAction to button onClick event by index 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="action"></param>
        public void AddButtonOnClickListener(int index, UnityAction action)
        {
            lButtons[index].onClick.AddListener(action);
        }

        /// <summary>
        /// Enable certain amount of buttons
        /// </summary>
        /// <param name="amount"></param>
        public void EnableCertainAmountOfButtons(int amount)
        {
            if (lButtons.Count == 0)
            {
                Debug.LogWarning("Please assign button list!");
                return;
            }

            for (int i = 0; i < amount; i++)
            {
                lButtons[i].gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Disable all buttons
        /// </summary>
        public void DisableAllButtons()
        {
            foreach (Button button in lButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        public void ActivateAnswer(bool isLeftSpeaking) {
            isLeftSpeaker = isLeftSpeaking;
            lAnswerObject.SetActive(isLeftSpeaking);
            rAnswerObject.SetActive(!isLeftSpeaking);
        }

        public void DeactivateAnswer() {
            lAnswerObject.SetActive(false);
            rAnswerObject.SetActive(false);
        }
    }
}