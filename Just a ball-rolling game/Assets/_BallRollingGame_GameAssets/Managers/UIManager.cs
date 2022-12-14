using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//This script managed all of the UI in the level scenes.
//It triggers animations from different animators.
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Canvases: ")]
    [SerializeField] private GameObject startingCanvas;
    public GameObject mainCanvas;
    [Header("UI Menus: ")]
    [SerializeField] private GameObject startingScreenUI;
    [SerializeField] private GameObject gameStartingCountdownUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameUI;
    [Header("UI elements:")]
    [SerializeField] private TextMeshProUGUI _currentTimeText;
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    [SerializeField] private TextMeshProUGUI _funnyText;
    [Header("Scripts: ")]
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private GameTimer gameTimer;
    [Header("Animators: ")]
    [SerializeField] private Animator startingScreenAnimator;
    [SerializeField] private Animator gameStartingCountdownAnimator;
    [SerializeField] private Animator gameOverUIAnimator;
    [SerializeField] private Animator victoryUIAnimator;
    [Header("Other information:")]
    [SerializeField] private int _lastLevelBuildIndex;
    [SerializeField] private string[] _funnyTextOptions;
    [SerializeField] private Level _level;

    private void Awake()
    {
        //Makes this a static class
        instance = this;
    }
    private void Start()
    {
        if (LevelLoader.instance.ReturnQuickStart() == false)
        {
            Debug.Log("No quickstart!");
            //Stop the music, then gradually increase it to 1
            AudioSingleton.Instance.StopMusic();
            AudioSingleton.Instance.SetVolumeGradually(1.0f, 3.0f);
            AudioSingleton.Instance.PlayMusic(AudioSingleton.Music.MENU);

            //Make sure only the startingMenu from the StartingCanvas is active
            startingCanvas.SetActive(true);
            startingScreenUI.SetActive(true);
            mainCanvas.SetActive(false);
            pauseUI.SetActive(false);
            gameOverUI.SetActive(false);
            victoryUI.SetActive(false);
            gameUI.SetActive(false);

            //start the StartingMenu animation: 
            StartCoroutine(SceneJustLoaded());

            GameManager.instance.SetGameState(GameManager.GameState.MENU_STARTING_TRANSITION);
        }
        else if (LevelLoader.instance.ReturnQuickStart() == true)
        {
            Debug.Log("Quick Start!");
            //Go straight to playing the game after setting volume to 0
            AudioSingleton.Instance.SetVolumeGradually(0.0f, 0.01f);
            GameStarted();

            LevelLoader.instance.SetQuickStart(false);
            GameManager.instance.SetGameState(GameManager.GameState.PLAYING_GAME);
        }
    }

    private void Update()
    {
        if (InputCapture.instance.ReturnIsPlayerPressingKey(KeyCode.R))
        {
            //Does the exact same thing as restarting the level from the button
            RestartLevelButton();
        }
    }

    public void StartGameFromStartingMenuButton()
    {
        //This is called by the green "Start" button in the StartingMenu
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON, 0.8f);
        mainCanvas.SetActive(true);
        startingCanvas.SetActive(false);
        StartCoroutine(GameStarting());
        ResetStartingMenuToDefaultState();
    }

    public void GameStarted()
    {
        //Gradually increase the volume while playing the level's original music
        AudioSingleton.Instance.PlayMusic(AudioSingleton.Music.LEVEL_MUSIC);
        AudioSingleton.Instance.SetVolumeGradually(1.0f, 2.0f);
        //Only activate the gameUI, so the player can start playing.
        mainCanvas.SetActive(true);
        gameUI.SetActive(true);
        startingCanvas.SetActive(false);
        pauseUI.SetActive(false);
        victoryUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameStartingCountdownUI.SetActive(false);
        //You're playing the game now.
        Time.timeScale = 1.0f;
        GameManager.instance.SetGameState(GameManager.GameState.PLAYING_GAME);
    }

    public void PauseGameButton()
    {
        if (GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON, 0.8f);
            pauseManager.PauseGame();
        }
    }

    public void ResumeGameButton()
    {
        if (GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.GAME_PAUSED)
        {
            AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON, 0.8f);
            pauseManager.ResumeGame();
        }
    }

    public void RestartLevelButton()
    {
        if (GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME ||
            GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.GAME_PAUSED ||
            GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYER_IS_DEAD ||
            GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYER_FINISHED_LEVEL)
        {
            AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON, 0.8f);
            LevelLoader.instance.SetQuickStart(true);
            LevelLoader.instance.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadMainMenuButton()
    {
        ChangingScene();
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON, 0.8f);
        LevelLoader.instance.LoadSceneAsync(0);
    }

    public void LoadNextLevelButton()
    {
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.BUTTON, 0.8f);
        ChangingScene();
        if (SceneManager.GetActiveScene().buildIndex != _lastLevelBuildIndex)
        {
            LevelLoader.instance.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void PauseGame()
    {
        gameUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        //Start the game right away
        GameStarted();
    }

    public void GameOver()
    {
        //The player just died
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        //Start the GameOverUI fade-in animtion (I think it lasts 4 seconds)
        gameOverUIAnimator.SetTrigger("PlayerDied");
    }

    public void PlayerWon()
    {
        gameUI.SetActive(false);
        victoryUI.SetActive(true);
        //Change the text informations
        var text1 = ((Mathf.Round((gameTimer._totalElapsedTime * 100))) / 100).ToString();
        _currentTimeText.text = text1;

        if (PlayerPrefs.HasKey("Level_" + _level.levelBuildIndex + "_FastestTime"))
        {
            var num = (PlayerPrefs.GetFloat("Level_" + _level.levelBuildIndex.ToString() + "_FastestTime"));
            var text2 = ((Mathf.Round((num * 100))) / 100).ToString();
            _bestTimeText.text = text2;
        }

        RandomizeFunnyText();

        //Start the animation
        victoryUIAnimator.SetTrigger("PlayerWon");
    }

    public void ChangingScene()
    {
        startingCanvas.SetActive(false);
        mainCanvas.SetActive(false);
    }

    private void RandomizeFunnyText()
    {
        int index = Random.Range(0, _funnyTextOptions.Length);
        _funnyText.text = _funnyTextOptions[index];
    }

    private IEnumerator SceneJustLoaded()
    {
        //Plays the animation of the starting screen
        startingScreenAnimator.SetTrigger("SceneJustLoaded");
        yield return new WaitForSeconds(3);
        GameManager.instance.SetGameState(GameManager.GameState.START_MENU);
    }

    private IEnumerator GameStarting()
    {
        AudioSingleton.Instance.SetVolumeGradually(0.0f, 3.0f);
        mainCanvas.SetActive(true);
        gameStartingCountdownUI.SetActive(true);
        gameStartingCountdownAnimator.SetTrigger("GameStarting");
        //Wait for the 3-2-1 GO! animation to finish
        yield return new WaitForSeconds(3.5f);
        //Make sure to reset the 3-2-1 GO! animtion to it's default, blank state
        gameStartingCountdownAnimator.SetTrigger("ResetToDefaultState");
        //Since the animation is done, the game is now playing:
        GameStarted();
    }

    public void ResetStartingMenuToDefaultState()
    {
        startingScreenAnimator.SetTrigger("ResetToDefaultState");
    }
}