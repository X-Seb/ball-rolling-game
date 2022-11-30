using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a Singleton that managed all the audio in the game.
//It plays random SFX from the lists.
//Each level has a different _levelBackgroundMusic so each level will have different background music

public class AudioSingleton : MonoBehaviour
{
    public static AudioSingleton Instance;

    [Header("Audio sources used to play the sfx and music: ")]
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    [Header("List of sound clip lists: ")]
    [SerializeField] private AudioClip[] _explosionAudioClips;
    [SerializeField] private AudioClip[] _victoryAudioClips;
    [SerializeField] private AudioClip[] _buttonAudioClips;
    [SerializeField] private AudioClip[] _collisionAudioClips;

    [Header("Specific audio clips: ")]
    [SerializeField] private AudioClip _sadMusic;
    [SerializeField] private AudioClip _levelBackgroundMusic;
    [SerializeField] private AudioClip _victoryBackgroundMusic;

    [Header("Volume for reference only: ")]
    [SerializeField] private float _sfxVolume = 1.0f;

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

    public void PlayVictoryMusic()
    {
        _musicAudioSource.clip = _victoryBackgroundMusic;
        _musicAudioSource.Play();
    }

    public void PlayVictorySound()
    {
        int index = Random.Range(0, _victoryAudioClips.Length);
        AudioClip clip = _victoryAudioClips[index];
        _sfxAudioSource.PlayOneShot(clip, _sfxVolume);
    }

    public void PlayBackgroundMusic()
    {
        _musicAudioSource.clip = _levelBackgroundMusic;
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