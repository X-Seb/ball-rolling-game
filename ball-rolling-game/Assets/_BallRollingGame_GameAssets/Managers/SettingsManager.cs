using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            mainMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        }
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("Volume", volume);

        PlayerPrefs.SetFloat("Volume", volume);
    }
}