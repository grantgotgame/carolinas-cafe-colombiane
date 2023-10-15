using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioClip sadBackground; // Sound effect to play
    public AudioClip startGameEffect; // Sound effect to play
    public AudioClip gameLoopBackground; // Sound effect to play
    public AudioClip miniGameBackground;
    public List<TalkingEffects> talkEffects;
    public AnimationCurve fadeCurve;

    [SerializeField]
    private AudioSource _musicSource;

    private bool lowered;
    private bool fade;
    private float fadeTime;

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
        lowered = false;
        fade = false;
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
        if (_musicSource.clip == gameLoopBackground) return;
        _musicSource.Stop();
        _musicSource.clip = gameLoopBackground;
        _musicSource.Play();
    }

    public void PlaySadBackground() {
        if (_musicSource.clip == sadBackground) return;
        _musicSource.Stop();
        _musicSource.clip = sadBackground;
        _musicSource.Play();
    }

    public void PlayStartGameEffect()
    {
        // Play the sound effect
        _musicSource.PlayOneShot(startGameEffect);
    }

    public void PlayTalkEffect(int pitch) {// Play the sound effect
        _musicSource.PlayOneShot(talkEffects[pitch].GetRandomClip(), PlayerPrefs.GetFloat("GameVolume"));
    }

    public void StopMusic()
    {
        // Play the sound effect
        _musicSource.Stop();
    }

    public void LowerGameplayMusicVolume()
    {
        lowered = true;
        fade = true;
        fadeTime = fadeCurve.keys[fadeCurve.length - 1].time;
        //_musicSource.volume = 0f;
    }

    public void ResumeGameplayMusicVolume() {
        lowered = false;
        fade = true;
        fadeTime = 0f;
        //_musicSource.volume = PlayerPrefs.GetFloat("GameVolume");
    }

    private void Update()
    {
        /*if (!lowered && PlayerPrefs.HasKey("GameVolume"))
        {
            float volumeSaved = PlayerPrefs.GetFloat("GameVolume");
            _musicSource.volume = volumeSaved;
        }*/
        if (fade) {
            if (lowered) fadeTime -= Time.deltaTime;
            else fadeTime += Time.deltaTime;

            if (fadeTime < fadeCurve.keys[0].time || fadeTime > fadeCurve.keys[fadeCurve.length - 1].time) {
                fade = false;
                return;
            }

            _musicSource.volume = fadeCurve.Evaluate(fadeTime) * PlayerPrefs.GetFloat("GameVolume");
        }
    }

}

[System.Serializable]
public class TalkingEffects {
    public List<AudioClip> clips;

    public AudioClip GetRandomClip() {
        return clips[Random.Range(0, clips.Count)];
    }
}