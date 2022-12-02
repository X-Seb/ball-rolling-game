using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This manages collisions between the player and different objects.
public class PlayerCollisionManager : MonoBehaviour
{
    [Header("Scripts: ")]
    [SerializeField] private PlayerGameOver playerGameOver;
    [SerializeField] private  playerVictory playerVictory;

    [Header("Player: ")]
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Die if you hit an enemy
        if (collision.gameObject.CompareTag("Enemy") &&
            GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            playerGameOver.PlayerDied();
        }

        if (collision.gameObject.CompareTag("Movable Object") && 
            GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            AudioSingleton.Instance.PlaySoundEffect(AudioSingleton.SoundEffect.COLLISION);
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Win Condition") &&
            GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            playerVictory.PlayerWon();
        }
    }
}