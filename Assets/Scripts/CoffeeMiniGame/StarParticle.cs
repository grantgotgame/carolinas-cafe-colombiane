using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticle : MonoBehaviour
{
    private ParticleSystem starParticle;

    private void Start() {
        starParticle = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (starParticle.isStopped) {
            Destroy(gameObject);
        }
    }
}
