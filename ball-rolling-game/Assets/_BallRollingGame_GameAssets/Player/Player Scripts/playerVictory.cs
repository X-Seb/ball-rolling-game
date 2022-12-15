using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class holds the function that's called when the player triggers the win condition
public class playerVictory : MonoBehaviour
{
    [Header("Scripts: ")]
    [SerializeField] private Level _level;
    [SerializeField] private GameTimer _gameTimer;

    public void PlayerWon()
    {
        Debug.Log("The player just won the game!");
        //Save the fact that the player just finished this level and which stars they just collected
        PlayerPrefs.SetInt("Level_" + _level.levelBuildIndex.ToString() + "_Completed", 1);
        PlayerPrefs.SetInt("Level_" + (_level.levelBuildIndex + 1).ToString() + "_Unlocked", 1);

        //Record the player's time to complete the level, since they never finished it before
        if (!PlayerPrefs.HasKey("Level_" + _level.levelBuildIndex.ToString() + "_FastestTime"))
        {
            PlayerPrefs.SetFloat("Level_" + _level.levelBuildIndex.ToString() + "_FastestTime", _gameTimer._totalElapsedTime);
        }

        //If the player already completed the level and this new time is faster than the old time: change the fastest time to match the new time
        else if (PlayerPrefs.GetFloat("Level_" + _level.levelBuildIndex.ToString() + "_FastestTime", -1) > 
            _gameTimer._totalElapsedTime)
        {
            PlayerPrefs.SetFloat("Level_" + _level.levelBuildIndex.ToString() + "_FastestTime", _gameTimer._totalElapsedTime);
        }

        PlayerPrefs.Save();

        //Transition to the victory UI and corresponding GameState
        GameManager.instance.SetGameState(GameManager.GameState.FINISHED_LEVEL_TRANSITION);
        UIManager.instance.PlayerWon();
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.ACHIEVEMENT, 1.0f);
        AudioSingleton.Instance.SetVolumeGradually(0.0f, 2.0f);
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.SetGameState(GameManager.GameState.PLAYER_FINISHED_LEVEL);
        AudioSingleton.Instance.PlayMusic(AudioSingleton.Music.VICTORY);
    }
}
