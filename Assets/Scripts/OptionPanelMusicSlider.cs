using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanelMusicSlider : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            float volumeSaved = PlayerPrefs.GetFloat("GameVolume");
            volumeSlider.value = volumeSaved;
        }
        else
        {
            PlayerPrefs.SetFloat("GameVolume", audioSource.volume);
            volumeSlider.value = audioSource.volume;
        }
        // Set initial slider value to match audio source volume
    }

    public void OnSliderValueChanged()
    {
        // Update audio source volume based on the slider value
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("GameVolume", audioSource.volume);
    }
}
