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

        [SerializeField] private List<Transform> spills;
        private int spillIndex;

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
            // NOTE: Currently, Dialog needs Result set to "1" for Mal,
            // "2" for Bien, "3" for Perfecto. Let me know if you'd like
            // to change this design.
            switch (streak) {
                case 0:
                    MiniGameResult.SetResult("1"); // "1" = Mal
                    break;
                case 1 or 2:
                    MiniGameResult.SetResult("2"); // "2" = Bien
                    break;
                default:
                    MiniGameResult.SetResult("3"); // "3" = Perfecto
                    break;
            }

            yield return new WaitForSeconds(transitionDelay);

            Loader.Instance.GoBackToMainScene();
        }

        public void PourResult(bool success, float xPos) {
            if (success) streak++;
            else {
                Vector3 pos = spills[spillIndex].position;
                pos.x = xPos;
                spills[spillIndex++].position = pos;
            }
            OnPourComplete?.Invoke(this, new CoffeeEventArgs { result = success });
            
        }
    }
}
