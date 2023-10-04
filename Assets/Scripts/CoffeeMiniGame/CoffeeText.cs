using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CoffeeMinigame {
    public class CoffeeText : MonoBehaviour {
        private string[] STREAK = { "One", "Two", "Three" };
        private string[] RESPONSE = { "Mal...", "BIEN", "BIEN", "PERFECTO!"};
        private const string FINISH = "Finish";

        [SerializeField] private CoffeePotController potController;
        [SerializeField] private TextMeshProUGUI coffeeText;
        [SerializeField] private TextMeshProUGUI finalText;

        private Animator animator;

        private void Start() {
            animator = GetComponent<Animator>();
            potController.OnPourComplete += OnPourComplete;
            potController.OnGameEnd += OnGameEnd;
        }

        private void OnGameEnd(object sender, System.EventArgs e) {
            finalText.text = RESPONSE[potController.streak];
            animator.SetTrigger(FINISH);
        }

        private void OnPourComplete(object sender, CoffeePotController.CoffeeEventArgs e) {
            if (e.result) {
                int streak = potController.streak;
                coffeeText.text = streak.ToString();
                animator.SetTrigger(STREAK[streak - 1]);
            }
        }
    }
}
