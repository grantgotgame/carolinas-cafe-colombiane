using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeMinigame {
    public class CoffeePot : MonoBehaviour {

        [SerializeField] private float moveSpeed;
        [SerializeField] private float paddingDistance;

        private Vector3 direction;
        private SpriteRenderer spriteRenderer;
        private bool pour;

        private void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            direction = Vector3.left;
        }

        // Update is called once per frame
        void Update() {
            if (!pour) {
                SetMoveDirection();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                if (Input.GetMouseButtonDown(0)) {
                    //Start Pour
                }
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
    }
}
