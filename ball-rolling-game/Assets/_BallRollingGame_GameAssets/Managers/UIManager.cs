using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Canvases: ")]
    [SerializeField] private GameObject startingCanvas;
    public GameObject mainCanvas;
    [Header("UI Menus: ")]
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
        //Make sure only the startingMenu from the StartingCanvas is active
        startingCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
        gameUI.SetActive(false);
        //start the StartingMenu animation: 
        StartCoroutine(SceneJustLoaded());
    }

    public void StartGameFromStartingMenuButton()
    {
        //This is called by the green "Start" button in the StartingMenu
        AudioSingleton.Instance.PlayButtonSound();
        mainCanvas.SetActive(true);
        startingCanvas.SetActive(false);
        StartCoroutine(GameStarting());
        ResetStartingMenuToDefaultState();
    }

    public void GameStarted()
    {
        //Only activate the gameUI, so the player can start playing.
        AudioSingleton.Instance.PlayBackgroundMusic(SceneManager.GetActiveScene().buildIndex);
        mainCanvas.SetActive(true);
        startingCanvas.SetActive(false);
        gameUI.SetActive(true);
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
            AudioSingleton.Instance.PlayButtonSound();
            pauseManager.PauseGame();
        }
    }

    public void ResumeGameButton()
    {
        if (GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.GAME_PAUSED)
        {
            AudioSingleton.Instance.PlayButtonSound();
            pauseManager.ResumeGame();
        }
    }

    public void RestartLevelButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        LevelLoader.instance.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenuButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
        LevelLoader.instance.LoadSceneAsync(0);
    }

    public void LoadNextLevelButton()
    {
        AudioSingleton.Instance.PlayButtonSound();
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
        //Turn off the pauseUI, make sure the game UI is activated
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
        //This will start the 3-2-1 GO screen
        StartCoroutine(GameStarting());
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
        mainCanvas.SetActive(true);
        gameStartingCountdownUI.SetActive(true);
        gameStartingCountdownAnimator.SetTrigger("GameStarting");
        Debug.Log("You should see 3-2-1 GO!");
        //Wait for the 3-2-1 GO! animation to finish
        yield return new WaitForSeconds(3.5f);
        //Make sure to reset the 3-2-1 GO! animtion to it's default, blank state
        gameStartingCountdownAnimator.SetTrigger("ResetToDefaultState");
        Debug.Log("The starting countdown should be reset to it's default state!");
        //Since the animation is done, the game is now playing:
        GameStarted();
    }

    public void ResetStartingMenuToDefaultState()
    {
        startingScreenAnimator.SetTrigger("ResetToDefaultState");
        Debug.Log("The starting menu should be reset to it's default state");
    }
}