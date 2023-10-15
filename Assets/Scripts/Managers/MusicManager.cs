using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBgm(AudioClip music)
    {
        bgm.Stop();
        bgm.clip = music;
        bgm.Play();
    }

}
