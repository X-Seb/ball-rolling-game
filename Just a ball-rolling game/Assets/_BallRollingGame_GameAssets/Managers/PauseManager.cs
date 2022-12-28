using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Changes the time scale of the game scenes and updates the game state
public class PauseManager : MonoBehaviour
{
    private void Start()
    {
        //TODO: Set the time to 0 and set it to 1 when the game starts
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (InputCapture.instance.ReturnIsPlayerPressingKey(KeyCode.P))
        {
            SwitchPause();
        }
    }
    public void SwitchPause()
    {
        switch (GameManager.instance.ReturnCurrentGameState())
        {
            case GameManager.GameState.PLAYING_GAME:
                PauseGame();
                break;
            case GameManager.GameState.GAME_PAUSED:
                ResumeGame();
                break;
            default:
                break;
        }
    }

    public void ResumeGame()
    {
        if (GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.GAME_PAUSED)
        {
            //TODO: Deactivate pause screen, start playing right away
            UIManager.instance.ResumeGame();
            Time.timeScale = 1.0f;
            GameManager.instance.SetGameState(GameManager.GameState.PLAYING_GAME);
            Debug.Log("Game should be resumed now...");
        }
    }

    public void PauseGame()
    {
        //TODO: Activate the pause screen
        Time.timeScale = 0.0f;
        UIManager.instance.PauseGame();
        GameManager.instance.SetGameState(GameManager.GameState.GAME_PAUSED);
    }
}