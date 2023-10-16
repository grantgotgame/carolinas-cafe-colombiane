using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterID = CharacterDictionarySO.CharacterID;

public static class PointSystem
{
    public static void ResetPoints() {
        PlayerPrefs.DeleteKey("Beatriz");
        PlayerPrefs.DeleteKey("Jairo");
        PlayerPrefs.DeleteKey("Alejandro");
        PlayerPrefs.DeleteKey("Alex");
        PlayerPrefs.DeleteKey("Santiago");
        PlayerPrefs.DeleteKey("Acacia");
    }

    public static void AddPoints(CharacterID character, int value) {
        int addValue = PlayerPrefs.GetInt(character.ToString(), 0) + value;
        PlayerPrefs.SetInt(character.ToString(), addValue);
    }

    public static int GetPoints(CharacterID character) {
        if (character.ToString() == "Alejandro" || character.ToString() == "Alex") {
            return PlayerPrefs.GetInt("Alejandro", 0) + PlayerPrefs.GetInt("Alex", 0);
        }
        return PlayerPrefs.GetInt(character.ToString(), 0);
    }

    public static int GetPoints(int id) {
        switch (id) {
            case 0:
                return PlayerPrefs.GetInt("Beatriz", 0);
            case 1:
                return PlayerPrefs.GetInt("Jairo", 0);
            case 2:
                return PlayerPrefs.GetInt("Alejandro", 0) + PlayerPrefs.GetInt("Alex", 0);
            case 3:
                return PlayerPrefs.GetInt("Santiago", 0);
            default:
                return PlayerPrefs.GetInt("Acacia", 0);

        }
        /*
        if (character.ToString() == "Alejandro" || character.ToString() == "Alex") {
            return PlayerPrefs.GetInt("Alejandro", 0) + PlayerPrefs.GetInt("Alex", 0);
        }
        return PlayerPrefs.GetInt(character.ToString(), 0);
        */
    }
}
