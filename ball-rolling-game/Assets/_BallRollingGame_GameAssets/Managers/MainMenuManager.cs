using TMPro;
using UnityEngine;

//This script managed the UI in the main menu scene. It shouldn't be anywhere else.
//Buttons in the main menu scene call functions from this script.
public class MainMenuManager : MonoBehaviour
{
    [Header("Canvases:")]
    public GameObject mainCanvas;
    [Header("UI Menus: ")]
    public GameObject mainMenu;
    public GameObject levelSelectionMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject howToPlayMenu;
    [Header("Text: ")]
    [SerializeField] private TextMeshProUGUI _continueText;

    private void Start()
    {
        AudioSingleton.Instance.PlayMusic(AudioSingleton.Music.LEVEL_MUSIC);
        mainMenu.SetActive(true);
        levelSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        ChangeContinueButtonText();
    }

    public void ContinueButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        PlayerPrefs.SetInt("FirstPlay", 1);
        LeavingScene();
        LevelLoader.instance.LoadSceneAsync(
            PlayerPrefs.GetInt("LastLevelPlayed", 1));
    }
    
    public void LevelSelectionButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        levelSelectionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SettingsButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void CreditsButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CreditsBackButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void SettingsBackButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void LevelSelectionBackButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        mainMenu.SetActive(true);
        levelSelectionMenu.SetActive(false);
    }

    public void HowToPlayButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        mainMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }

    public void HowToPlayBackButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON);
        mainMenu.SetActive(true);
        howToPlayMenu.SetActive(false);
    }

    public void LeavingScene()
    {
        mainMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        creditsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private void ChangeContinueButtonText()
    {
        if (!PlayerPrefs.HasKey("FirstPlay"))
        {
            _continueText.text = "Start!";
        }
        else
        {
            _continueText.text = "Continue";
        }
    }
}