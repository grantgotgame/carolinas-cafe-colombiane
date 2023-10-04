using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeMinigame {
    public class CoffeePotAnimator : MonoBehaviour {
        private const string POUR = "Pour";

        private Animator animator;
        private CoffeePotController potController;

        // Start is called before the first frame update
        void Start() {
            potController = GetComponent<CoffeePotController>();
            potController.OnPour += OnPour;
            animator = GetComponent<Animator>();
        }

        private void OnPour(object sender, CoffeePotController.CoffeeEventArgs e) {
            if (e.result) {
                animator.SetTrigger(POUR);
            }
        }
    }
}
