using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterID = CharacterDictionarySO.CharacterID;
using cherrydev;

public static class MiniGameResult {
    private static string result;
    private static string prevResult;

    public static void SetResult(string miniGameResult) {
        result = miniGameResult;
    }

    private static bool IsNewResult() {
        return result != prevResult;
    }

    public static string GetResult() {
        if (IsNewResult()) {
            prevResult = result;
            return result;
        }
        return string.Empty;
    }

    public static void ResetMinigame() {
        prevResult = string.Empty;
        result = string.Empty;
    }
}
