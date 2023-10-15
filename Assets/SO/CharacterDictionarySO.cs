using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CharacterDictionarySO : ScriptableObject
{
    [System.Serializable]
    public struct Character {
        public string name;
        public Sprite characterSprite;
        public Sprite nameBadgeSprite;
        public int pitch;
    }

    public enum CharacterID {
        Carolina,
        Beatriz,
        Jairo,
        Alejandro,
        Santiago,
        Acacia,
        Alex,
        None
    }

    [SerializeField]
    public List<Character> characterDictionary;

    public Character GetCharacterByID(CharacterID characterID) {
        return characterDictionary[((int)characterID)];
    }


}
