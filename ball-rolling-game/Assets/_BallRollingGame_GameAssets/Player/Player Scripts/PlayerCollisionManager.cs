using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    // This manages collisions between the player and different objects.
    [Header("Scripts: ")]
    [SerializeField] private PlayerGameOver playerGameOver;
    [SerializeField] private  playerVictory playerVictory;

    [Header("Player: ")]
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private GameObject _player;

    [Header("Others: ")]
    [SerializeField] private bool _isOnPlatform = false;

    [Header("Platform stuff: (Make sure the Rigidbody is set to Kinematic")]
    [SerializeField] private Rigidbody _platformRb;

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
            AudioSingleton.Instance.PlayCollisionSound();
        }

        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            _isOnPlatform = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        _isOnPlatform = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            _platformRb = null;
            _isOnPlatform = false;
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