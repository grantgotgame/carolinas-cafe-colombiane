using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    private Bounds mugBounds;

    private void Start() {
        mugBounds = GetComponent<BoxCollider2D>().bounds;
    }

    private void Update() {
        if (particleSystem.gameObject.activeSelf && particleSystem.isStopped) {
            particleSystem.gameObject.SetActive(false);
        }
    }

    public bool IsInsideMug(float xPosition) {
        return xPosition < mugBounds.max.x && xPosition > mugBounds.min.x;
    }

    public void SetParticleSystem() {
        particleSystem.gameObject.SetActive(true);
    }
}
