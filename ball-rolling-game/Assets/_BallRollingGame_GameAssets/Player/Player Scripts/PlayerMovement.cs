using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script takes input from the InputCapture script and uses it to move the player.
//You can only move if you're playing the game.

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float playerSpeed = 0.75f;

    private void FixedUpdate()
    {
        //Checks if you're playing the game before moving the player.
        if (GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        direction = new Vector3(InputCapture.instance.ReturnHorizontalInput(),
            0, InputCapture.instance.ReturnVerticalInput());

        playerRB.AddForce(direction * playerSpeed, ForceMode.Impulse);
    }
}