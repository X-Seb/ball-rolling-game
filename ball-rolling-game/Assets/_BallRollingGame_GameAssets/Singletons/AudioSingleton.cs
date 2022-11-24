using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    public static AudioSingleton Instance;

    [Header("Audio sources used to play the sfx and music: ")]
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    [Header("List of sound clips: ")]
    [SerializeField] private AudioClip[] _backgroundMusicAudioClips;
    [SerializeField] private AudioClip[] _explosionAudioClips;
    [SerializeField] private AudioClip[] _victoryAudioClips;
    [SerializeField] private AudioClip[] _buttonAudioClips;
    [SerializeField] private AudioClip[] _achievementAudioClips;
    [SerializeField] private AudioClip[] _collisionAudioClips;

    [Header("Specific audio clips: ")]
    [SerializeField] private AudioClip _sadMusic;

    [Header("Volume for reference only: ")]
    [SerializeField] private float _sfxVolume = 1.0f;
    [SerializeField] private float _musicVolume = 1.0f;

    //This makes this class a singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayExplosionSound()
    {
        int index = Random.Range(0, _buttonAudioClips.Length);
        AudioClip clip = _buttonAudioClips[index];
        _sfxAudioSource.PlayOneShot(clip, _sfxVolume);
    }

    public void PlaySadMusic()
    {
        _musicAudioSource.clip = _sadMusic;
        _musicAudioSource.Play();
    }

    public void PlayVictorySound()
    {
        int index = Random.Range(0, _victoryAudioClips.Length);
        AudioClip clip = _victoryAudioClips[index];
        _sfxAudioSource.PlayOneShot(clip, _sfxVolume);
    }

    public void PlayMainMenuMusic()
    {
        AudioClip music = _backgroundMusicAudioClips[0];
        _musicAudioSource.clip = music;
        _musicAudioSource.Play();
    }

    public void PlayBackgroundMusic(int index)
    {
        AudioClip music = _backgroundMusicAudioClips[index];
        _musicAudioSource.clip = music;
        _musicAudioSource.Play();
    }

    public void PlayButtonSound()
    {
        int index = Random.Range(0, _buttonAudioClips.Length);
        AudioClip clip = _buttonAudioClips[index];
        _sfxAudioSource.PlayOneShot(clip, _sfxVolume);
    }

    public void PlayCollisionSound()
    {
        int index = Random.Range(0, _collisionAudioClips.Length);
        AudioClip clip = _collisionAudioClips[index];
        _sfxAudioSource.PlayOneShot(clip, _sfxVolume);
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        _sfxAudioSource.PlayOneShot(audioClip, _sfxVolume);
    }

    public void StopMusic()
    {
        _musicAudioSource.Stop();
    }
}