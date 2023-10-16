using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeMinigame {
    public class CoffeePotMovement : MonoBehaviour {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float paddingDistance;
        [SerializeField] private Transform pourLocation;
        [SerializeField] private Mug mug;

        private Vector3 direction;
        private SpriteRenderer spriteRenderer;
        private bool pour;
        private CoffeePotController potController;

        private void Start() {
            potController = GetComponent<CoffeePotController>();
            potController.OnPour += OnPour;
            spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            direction = Vector3.left;
        }

        private void OnPour(object sender, CoffeePotController.CoffeeEventArgs e) {
            pour = e.result;
        }

        // Update is called once per frame
        void Update() {
            if (!pour) {
                SetMoveDirection();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }

        private void SetMoveDirection() {
            Vector2 minMax = new Vector2(spriteRenderer.bounds.min.x, spriteRenderer.bounds.max.x);
            float screenMin = Camera.main.ScreenToWorldPoint(Vector3.zero).x + paddingDistance;
            float screenMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0f)).x - paddingDistance;
            if (minMax.x <= screenMin)
                direction = Vector3.right;
            if (minMax.y >= screenMax)
                direction = Vector3.left;
        }

        public void CheckInsideMug() {
            potController.PourResult(mug.IsInsideMug(pourLocation.position.x), pourLocation.position.x);
        }
    }
}
