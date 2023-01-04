using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This is a Singleton that managed all the audio in the game.
//It plays random SFX from the lists.
//Each level has a different _levelBackgroundMusic so each level will have different background music
public class AudioSingleton : MonoBehaviour
{
    public static AudioSingleton Instance;

    [Header("Audio sources used to play the sfx and music: ")]
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    [Header("List of sound clip arrays: ")]
    [SerializeField] private AudioClip[] _explosionAudioClips;
    [SerializeField] private AudioClip[] _achievementAudioClips;
    [SerializeField] private AudioClip[] _buttonAudioClips;
    [SerializeField] private AudioClip[] _collisionAudioClips;

    [Header("Other music audio clips for the level scenes: ")]
    [SerializeField] private AudioClip _sadBackgroundMusic;
    [SerializeField] private AudioClip _victoryBackgroundMusic;
    [SerializeField] private AudioClip _startingMenuBackgroundMusic;

    [Header("Array that holds the background music audio clip for each scene: ")]
    [SerializeField] private AudioClip[] _levelBGM;

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

    public enum SoundEffect
    {
        EXPLOSION,
        ACHIEVEMENT,
        BUTTON,
        COLLISION
    }

    public enum Music
    {
        SAD,
        VICTORY,
        MENU,
        LEVEL_MUSIC
    }

    public void PlaySoundEffect(SoundEffect type, float volume)
    {
        switch (type)
        {
            case SoundEffect.ACHIEVEMENT:
                int index = Random.Range(0, _achievementAudioClips.Length);
                _sfxAudioSource.PlayOneShot(_achievementAudioClips[index], volume);
                    break;
            case SoundEffect.BUTTON:
                int index1 = Random.Range(0, _buttonAudioClips.Length);
                _sfxAudioSource.PlayOneShot(_buttonAudioClips[index1], volume);
                break;
            case SoundEffect.COLLISION:
                int index2 = Random.Range(0, _collisionAudioClips.Length);
                _sfxAudioSource.PlayOneShot(_collisionAudioClips[index2], volume);
                break;
            case SoundEffect.EXPLOSION:
                int index3 = Random.Range(0, _explosionAudioClips.Length);
                _sfxAudioSource.PlayOneShot(_explosionAudioClips[index3], volume);
                break;
            default:
                break;

        }
    }

    public void PlayMusic(Music type)
    {
        switch (type)
        {
            case Music.SAD:
                _musicAudioSource.clip = _sadBackgroundMusic;
                Debug.Log("Now playing sad music");
                break;
            case Music.VICTORY:
                _musicAudioSource.clip = _victoryBackgroundMusic;
                Debug.Log("Now playing victory music");
                break;
            case Music.LEVEL_MUSIC:
                _musicAudioSource.clip = _levelBGM[SceneManager.GetActiveScene().buildIndex];
                Debug.Log("Now playing the level's background music");
                break;
            case Music.MENU:
                _musicAudioSource.clip = _startingMenuBackgroundMusic;
                Debug.Log("Now playing the starting menu background music");
                break;
            default:
                break;
        }

        _musicAudioSource.Play();
    }

    public void SetVolumeGradually(float volume, float seconds)
    {
        StartCoroutine(SetVolume(volume, seconds));
    }

    private IEnumerator SetVolume(float endValue, float lerpDuration)
    {
        float timeElapsed = 0.0f;
        float startValue = _musicAudioSource.volume;


        while (timeElapsed < lerpDuration)
        {
            _musicAudioSource.volume = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _musicAudioSource.volume = endValue;

    }

    public void StopMusic()
    {
        if (_musicAudioSource.clip != null || _musicAudioSource != null)
        {
            _musicAudioSource.Stop();
        }
    }
}