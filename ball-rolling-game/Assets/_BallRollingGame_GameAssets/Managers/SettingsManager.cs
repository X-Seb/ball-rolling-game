using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider _slider;

    private void Start()
    {
        //Remembers the chosen audio volume when you enter start the scene
        if (PlayerPrefs.HasKey("Volume"))
        {
            mainMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
            _slider.value = PlayerPrefs.GetFloat("Volume");
        }
    }

    public void SetVolume(float volume)
    {
        //Changes the volume of the main mixer and saves it to PlayerPrefs
        mainMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }
}