using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

//This script managed the settings menu.
//It sets the global volume to whatever the player inputed in the slider.
public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider _slider;
    public TMP_Dropdown _dropdown;

    private void Start()
    {
        //Remembers the chosen audio volume when you enter start the scene
        if (PlayerPrefs.HasKey("Volume"))
        {
            mainMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
            _slider.value = PlayerPrefs.GetFloat("Volume");
        }

        //Remember the chosen quality level when you enter the scene
        if (PlayerPrefs.HasKey("Quality"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
            _dropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("Quality"));
        }
    }

    public void SetVolume(float volume)
    {
        //Changes the volume of the main mixer and saves it to PlayerPrefs
        mainMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
        PlayerPrefs.Save();
    }
}