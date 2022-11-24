using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [SerializeField] GameState gameState;

    //These are all the game states that determine how the game should behave
    public enum GameState
    {
        MENU_STARTING_TRANSITION,
        START_MENU,
        GAME_STARTING_COUNTDOWN,
        PLAYING_GAME,
        GAME_PAUSED,
        GAME_RESUMING_COUNTDOWN,
        PLAYER_DIED_TRANSITION,
        PLAYER_IS_DEAD,
        FINISHED_LEVEL_TRANSITION,
        PLAYER_FINISHED_LEVEL,
        LEAVING_SCENE,
        MAIN_MENU_SCENE
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SetGameState(GameState.MAIN_MENU_SCENE);
        }
        else
        {
            SetGameState(GameState.MENU_STARTING_TRANSITION);
        }
    }

    public void SetGameState(GameState newGameState)
    {
        gameState = newGameState;
        Debug.Log("The current gameState is now " + newGameState);
    }

    public GameState ReturnCurrentGameState()
    {
        return gameState;
    }

    public void GameStarted()
    {
        Debug.Log("Game started! The player can now move around.");
        SetGameState(GameState.PLAYING_GAME);
    }
}