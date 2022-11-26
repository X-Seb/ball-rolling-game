using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] float _volume = 0.0f;
    [SerializeField] SliderJoint2D _volumeSlider;

    public void SetVolume(float volume)
    {
        _volume = volume;
        Debug.Log(volume);
    }
}