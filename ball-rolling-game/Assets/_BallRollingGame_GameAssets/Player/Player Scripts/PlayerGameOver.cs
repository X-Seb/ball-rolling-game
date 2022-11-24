using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    public void PlayerDied()
    {
        //TODO: music fade-out, death sfx, death music fades-in
        //TODO: Make the player explode!
        Debug.Log("The player just died.");
        UIManager.instance.GameOver();
        AudioSingleton.Instance.PlayExplosionSound();
        AudioSingleton.Instance.PlaySadMusic();
        GameManager.instance.SetGameState(GameManager.GameState.PLAYER_DIED_TRANSITION);
    }
}