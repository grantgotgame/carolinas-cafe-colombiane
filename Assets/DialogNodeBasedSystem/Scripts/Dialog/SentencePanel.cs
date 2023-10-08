using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Character = CharacterDictionarySO.Character;

namespace cherrydev
{
    public class SentencePanel : MonoBehaviour
    {
        [Header("Left Speaker"), Space()]
        [SerializeField] private GameObject lSpeakerObject;
        [SerializeField] private GameObject lNonSpeakerObject;
        [SerializeField] private TextMeshProUGUI lDialogNameText;
        [SerializeField] private TextMeshProUGUI lDialogText;
        [SerializeField] private Image lDialogNameBadgeImage;
        [SerializeField] private Image lDialogCharacterImage;
        [Header("Right Speaker"), Space()]
        [SerializeField] private GameObject rSpeakerObject;
        [SerializeField] private GameObject rNonSpeakerObject;
        [SerializeField] private TextMeshProUGUI rDialogNameText;
        [SerializeField] private TextMeshProUGUI rDialogText;
        [SerializeField] private Image rDialogNameBadgeImage;
        [SerializeField] private Image rDialogCharacterImage;

        private bool isLeftSpeaker;
        private void Start()
        {
            rDialogText.text = string.Empty;
        }

        /// <summary>
        /// Setting dialogText text to empty string
        /// </summary>
        public void ResetDialogText()
        {
            lDialogText.text = string.Empty;
            rDialogText.text = string.Empty;
        }

        /// <summary>
        /// Assigning dialog name text and character iamge sprite
        /// </summary>
        /// <param name="name"></param>
        public void AssignDialogNameTextAndSprite(Character character, Character otherCharacter, bool isOnLeftSide)
        {
            isLeftSpeaker = isOnLeftSide;
            if (isLeftSpeaker) {
                lDialogNameText.text = character.name;
                lDialogNameBadgeImage.sprite = character.nameBadgeSprite;
                lDialogCharacterImage.sprite = character.characterSprite;
            } else {
                lDialogNameText.text = character.name;
                lDialogNameBadgeImage.sprite = character.nameBadgeSprite;
                lDialogCharacterImage.sprite = character.characterSprite;
            }
            /*rDialogNameText.text = name;

            if (sprite == null)
            {
                rDialogCharacterImage.color = new Color(rDialogCharacterImage.color.r,
                    rDialogCharacterImage.color.g, rDialogCharacterImage.color.b, 0);
                return;
            }

            rDialogCharacterImage.color = new Color(rDialogCharacterImage.color.r,
                    rDialogCharacterImage.color.g, rDialogCharacterImage.color.b, 255);
            rDialogCharacterImage.sprite = sprite;*/
        }

        /// <summary>
        /// Adding char to dialog text
        /// </summary>
        /// <param name="textChar"></param>
        public void AddCharToDialogText(char textChar)
        {
            if (isLeftSpeaker) lDialogText.text += textChar;
            else rDialogText.text += textChar;
        }

        public void ActivatePanels(DialogData data) {
            isLeftSpeaker = data.isOnLeftSide;
            lSpeakerObject.SetActive(isLeftSpeaker);
            lNonSpeakerObject.SetActive(isLeftSpeaker);
            rSpeakerObject.SetActive(!isLeftSpeaker);
            rNonSpeakerObject.SetActive(!isLeftSpeaker);
        }

        public void ActivateSentence() {
            lDialogText.gameObject.SetActive(isLeftSpeaker);
            rDialogText.gameObject.SetActive(!isLeftSpeaker);
        }

        public void DeactivateSentence() {
            lDialogText.gameObject.SetActive(false);
            rDialogText.gameObject.SetActive(false);
            lNonSpeakerObject.SetActive(false);
            rNonSpeakerObject.SetActive(false);
        }
    }
}