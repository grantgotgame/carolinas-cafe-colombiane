using UnityEngine;

namespace cherrydev
{
    [System.Serializable]
    public struct Sentence
    {
        public string characterName;
        [TextArea(3, 10)]
        public string text;
        public Sprite characterSprite;

        public Sentence(string characterName, string text)
        {
            characterSprite = null;
            this.characterName = characterName;
            this.text = text;
        }
    }

    [System.Serializable]
    public class Answer {
        [TextArea(3, 10)]
        public string answer;
    }
}