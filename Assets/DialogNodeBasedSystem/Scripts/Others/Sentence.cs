using UnityEngine;

namespace cherrydev
{
    [System.Serializable]
    public struct Sentence
    {
        public string characterName;
        public string text;
        public Sprite characterSprite;
        public int amountOfPoint;

        public Sentence(string characterName, string text, int amountOfPoint)
        {
            characterSprite = null;
            this.characterName = characterName;
            this.text = text;
            this.amountOfPoint = amountOfPoint;
        }
    }
}