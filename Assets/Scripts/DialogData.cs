using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogData
{
    [HideInInspector] public string nodeName;
    public string dialogueChoice;
    public string miniGameValue;
    public bool isOnLeftSide;
    public CharacterDictionarySO.CharacterID otherSpeaker;
}
