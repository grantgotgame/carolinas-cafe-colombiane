using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeMinigame {
    public class Mug : MonoBehaviour {
        [SerializeField] private Transform particleSpawnLocation;
        [SerializeField] private GameObject starParticlePrefab;
        [SerializeField] private CoffeeText coffeeTextAnimator;
        [SerializeField] private CoffeePotController potController;
        private Bounds mugBounds;

        private void Start() {
            potController.OnPourComplete += OnPourComplete;
            mugBounds = GetComponent<BoxCollider2D>().bounds;
        }

        private void Update() {
            /*if (particles.gameObject.activeSelf && particles.isStopped) {
                particles.gameObject.SetActive(false);
            }*/
        }

        public bool IsInsideMug(float xPosition) {
            return xPosition < mugBounds.max.x && xPosition > mugBounds.min.x;
        }

        private void OnPourComplete(object sender, CoffeePotController.CoffeeEventArgs e) {
            if (e.result)
                Instantiate(starParticlePrefab, particleSpawnLocation);
        }
    }
}
