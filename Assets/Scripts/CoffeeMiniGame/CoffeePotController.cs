using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeMinigame {
    public class CoffeePotController : MonoBehaviour {

        public event EventHandler<CoffeeEventArgs> OnPour;
        public event EventHandler<CoffeeEventArgs> OnPourComplete;
        public event EventHandler OnGameEnd;

        [SerializeField, Range(0f, 5f)] private float transitionDelay = 1.5f;

        public class CoffeeEventArgs : EventArgs {
            public bool result;
        }
        public int triesRemaining { get; private set; }
        public int streak { get; private set; }

        private bool pour;
        private void Awake() {
            triesRemaining = 3;
            streak = 0;
        }

        // Update is called once per frame
        void Update() {
            if (!pour && triesRemaining > 0 && Input.GetMouseButtonDown(0)) {
                OnPour?.Invoke(this, new CoffeeEventArgs { result = true });
                pour = true;
                triesRemaining--;
            }
        }

        public void ResetPour() {
            if (triesRemaining > 0) {
                pour = false;
                OnPour?.Invoke(this, new CoffeeEventArgs { result = false });
            } else {
                OnGameEnd?.Invoke(this, EventArgs.Empty);
                StartCoroutine(EndMiniGame());
            }
        }

        IEnumerator EndMiniGame() {
            MiniGameResult.SetResult(streak.ToString());

            yield return new WaitForSeconds(transitionDelay);

            Loader.Instance.GoBackToMainScene();
        }

        public void PourResult(bool success) {
            if (success) streak++;
            OnPourComplete?.Invoke(this, new CoffeeEventArgs { result = success });
        }
    }
}
