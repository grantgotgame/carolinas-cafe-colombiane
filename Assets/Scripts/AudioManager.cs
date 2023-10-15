using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioClip startTheme; // Sound effect to play
    public AudioClip startGameEffect; // Sound effect to play
    public AudioClip gameLoopBackground; // Sound effect to play
    public AudioClip miniGameBackground;
    public AudioClip talkEffect;

    [SerializeField]
    private AudioSource _musicSource;

    private void Awake()
    {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        /*var audioManagers = FindObjectsOfType<AudioManager>();
        if (audioManagers.Length > 1)
        {
            Destroy(gameObject);
        }*/

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

    public void PlayMiniGameBackground() {
        // Play the sound effect
        if (_musicSource.clip == miniGameBackground) return;
        _musicSource.Stop();
        _musicSource.clip = miniGameBackground;
        _musicSource.Play();
    }

    public void PlayGameLoopBackground()
    {
        // Play the sound effect
        Debug.Log($"{_musicSource.clip} and {_musicSource.clip == gameLoopBackground}");
        if (_musicSource.clip == gameLoopBackground) return;
        _musicSource.Stop();
        _musicSource.clip = gameLoopBackground;
        _musicSource.Play();
    }

    public void PlayStartGameEffect()
    {
        // Play the sound effect
        _musicSource.PlayOneShot(startGameEffect);
    }

    public void PlayTalkEffect() {// Play the sound effect
        _musicSource.PlayOneShot(talkEffect);
    }

    public void StopMusic()
    {
        // Play the sound effect
        _musicSource.Stop();
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
            _musicSource.volume = volumeSaved;
        }

    }

}
