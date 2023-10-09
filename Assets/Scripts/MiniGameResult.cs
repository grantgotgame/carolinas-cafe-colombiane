using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterID = CharacterDictionarySO.CharacterID;
using cherrydev;

public static class MiniGameResult {

    public class Result {
        public string result;
        public CharacterID customer;
        public DialogNodeGraph nextDialog;
    }

    private static Result customer = new Result();
    private static string prevResult;

    public static void SetupMinigame(CharacterID customer, DialogNodeGraph nextConversation) {
        MiniGameResult.customer.customer = customer;
        MiniGameResult.customer.nextDialog = nextConversation;
    }
    public static void SetResult(string miniGameResult) {
        customer.result = miniGameResult;
    }

    private static bool IsNewResult() {
        return customer.result != prevResult;
    }

    public static Result GetResult() {
        if (IsNewResult()) {
            prevResult = customer.result;
            return customer;
        }
        return null;
    }

    public static void ResetMinigame() {
        customer.customer = CharacterID.Carolina;
        prevResult = string.Empty;
        customer.result = string.Empty;
        customer.nextDialog = null;
    }
}
