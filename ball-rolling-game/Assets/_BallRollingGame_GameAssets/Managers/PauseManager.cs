using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //TODO: Deactivate pause screen, fade-out transition, 3-2-1 GO
            UIManager.instance.ResumeGame();
            GameManager.instance.SetGameState(GameManager.GameState.GAME_RESUMING_COUNTDOWN);
            StartCoroutine(ResumeTransition());
        }
    }

    public void PauseGame()
    {
        //TODO: Activate the pause screen
        Time.timeScale = 0.0f;
        UIManager.instance.PauseGame();
        GameManager.instance.SetGameState(GameManager.GameState.GAME_PAUSED);
    }

    private IEnumerator ResumeTransition()
    {

        yield return new WaitForSecondsRealtime(3.5f);
        Time.timeScale = 1.0f;
        GameManager.instance.SetGameState(GameManager.GameState.PLAYING_GAME);
        Debug.Log("Game should be resumed now...");
    }
}