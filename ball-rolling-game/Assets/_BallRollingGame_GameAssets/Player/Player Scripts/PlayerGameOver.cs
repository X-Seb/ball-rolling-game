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
        UIManager.instance.GameOver();
        AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.EXPLOSION);
        AudioSingleton.Instance.SetVolumeGradually(0.0f, 2.0f);
        GameManager.instance.SetGameState(GameManager.GameState.PLAYER_DIED_TRANSITION);
    }
}