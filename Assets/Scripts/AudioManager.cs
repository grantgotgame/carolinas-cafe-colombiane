using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip startTheme; // Sound effect to play
    public AudioClip startGameEffect; // Sound effect to play
    public AudioClip gameLoopBackground; // Sound effect to play

    [SerializeField]
    private AudioSource _musicSource;

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        var audioManagers = FindObjectsOfType<AudioManager>();
        if (audioManagers.Length > 1)
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("GameVolume"))
        {
            GetComponent<AudioSource>().volume =   PlayerPrefs.GetFloat("GameVolume");
            //GetComponent<AudioSource>().volume = volumeSaved;
        }
        else
        {
            //GetComponent<AudioSource>().volume = 0.01f;
        }
        // Set initial slider value to match audio source volume
    }

    public void PlayGameLoopBackground()
    {
        // Play the sound effect
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameLoopBackground);
    }

    public void PlayStartGameEffect()
    {
        // Play the sound effect
        gameObject.GetComponent<AudioSource>().PlayOneShot(startGameEffect);
    }

    public void StopMusic()
    {
        // Play the sound effect
        gameObject.GetComponent<AudioSource>().Stop();
    }

    public void LowerGameplayMusicVolume()
    {
        _musicSource.volume = 0.3f;
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            float volumeSaved = PlayerPrefs.GetFloat("GameVolume");
            GetComponent<AudioSource>().volume = volumeSaved;
        }

    }

}
