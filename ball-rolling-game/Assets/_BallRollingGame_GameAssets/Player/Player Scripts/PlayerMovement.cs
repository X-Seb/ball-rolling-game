using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;
    [SerializeField] Vector3 movement;
    [SerializeField] float playerSpeed = 0.75f;

    private void Start()
    {
        playerRB = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        movement = new Vector3(InputCapture.instance.ReturnHorizontalInput(),
            0, InputCapture.instance.ReturnVerticalInput());

        TryToMovePlayer(movement);
    }

    private void TryToMovePlayer(Vector3 movement)
    {
        if(GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            MovePlayer(movement);
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        playerRB.AddForce(direction * playerSpeed, ForceMode.Impulse);
    }
}