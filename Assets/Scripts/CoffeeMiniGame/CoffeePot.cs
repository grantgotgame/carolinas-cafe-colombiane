using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeMinigame {
    public class CoffeePot : MonoBehaviour {

        private const string POUR = "Pour";

        [SerializeField] private float moveSpeed;
        [SerializeField] private float paddingDistance;
        [SerializeField] private Transform pourLocation;
        [SerializeField] private Mug mug;

        private Vector3 direction;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private bool pour;

        private void Start() {
            spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            direction = Vector3.left;
        }

        // Update is called once per frame
        void Update() {
            if (!pour) {
                SetMoveDirection();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                if (Input.GetMouseButtonDown(0)) {
                    pour = true;
                    animator.SetTrigger(POUR);
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

        public void ResetPour() {
            pour = false;
        }

        public void CheckInsideMug() {
            if (mug.IsInsideMug(pourLocation.position.x)) {
                mug.SetParticleSystem();
            } else {
                Debug.Log("Outside");
            }
        }
    }
}
