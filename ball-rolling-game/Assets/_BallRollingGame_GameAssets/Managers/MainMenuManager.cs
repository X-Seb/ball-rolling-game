using TMPro;
using UnityEngine;

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
        AudioSingleton.Instance.PlayBackgroundMusic();
        mainMenu.SetActive(true);
        levelSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        ChangeContinueButtonText();
    }

    public void ContinueButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        PlayerPrefs.SetInt("FirstPlay", 1);
        LeavingScene();
        LevelLoader.instance.LoadSceneAsync(
            PlayerPrefs.GetInt("LastLevelPlayed", 1));
    }
    
    public void LevelSelectionButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        levelSelectionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SettingsButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void CreditsButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CreditsBackButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void SettingsBackButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void LevelSelectionBackButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        mainMenu.SetActive(true);
        levelSelectionMenu.SetActive(false);
    }

    public void HowToPlayButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        mainMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }

    public void HowToPlayBackButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
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