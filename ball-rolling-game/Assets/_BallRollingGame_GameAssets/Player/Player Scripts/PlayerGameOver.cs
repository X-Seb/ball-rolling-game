using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script holds the script that's called when the player dies.
public class PlayerGameOver : MonoBehaviour
{
    public void PlayerDied()
    {
        //TODO: music fade-out, death music fades-in
        //TODO: Make the player explode!
        Debug.Log("The player just died.");

        //Activates the right UI screens and plays the game over animation
        UIManager.instance.GameOver();

        //Plays explosion sound + deacreases the level music volume
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.EXPLOSION, 1.0f);
        AudioSingleton.Instance.SetVolumeGradually(0.0f, 3.0f);

        //Changes the game state
        GameManager.instance.SetGameState(GameManager.GameState.PLAYER_DIED_TRANSITION);

        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        //Waits 3 seconds before playing the sad music and increasing the volume over 2 seconds
        yield return new WaitForSecondsRealtime(3.0f);
        AudioSingleton.Instance.PlayMusic(AudioSingleton.Music.SAD);
        AudioSingleton.Instance.SetVolumeGradually(1.0f, 2.0f);
    }
}