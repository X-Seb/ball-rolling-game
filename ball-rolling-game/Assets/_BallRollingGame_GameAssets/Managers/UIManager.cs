using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Canvases: ")]
    public GameObject startingCanvas;
    public GameObject mainCanvas;
    [Header("UI Menus: ")]
    public GameObject gameStartingCountdownUI;
    public GameObject gameOverUI;
    public GameObject victoryUI;
    public GameObject pauseUI;
    public GameObject gameUI;
    [Header("Scripts: ")]
    public PauseManager pauseManager;
    [Header("Animators: ")]
    public Animator startingScreenAnimator;
    public Animator gameStartingCountdownAnimator;
    public Animator gameOverUIAnimator;
    public Animator victoryUIAnimator;
    [Header("Other information:")]
    [SerializeField] private int _lastLevelBuildIndex;

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
        //Start the animation
        victoryUIAnimator.SetTrigger("PlayerWon");
    }

    public void ChangingScene()
    {
        mainCanvas.SetActive(false);
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