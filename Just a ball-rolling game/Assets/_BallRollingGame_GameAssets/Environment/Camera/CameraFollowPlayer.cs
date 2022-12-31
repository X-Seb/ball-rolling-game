using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script moves the camera to follow the player's x and y position with an offset.
public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Transform currentPos;
    private Vector3 offset;
    private Vector3 newtrans;
    [SerializeField] private bool _testing = false;

    void Start()
    {
        if (_testing == true)
        {
            //This always places the camera at the same pos relative to the player
            offset.x = 0f;
            offset.z = -8.5f;
            newtrans.y = player.transform.position.y + 6.0f;
        }

        if (_testing == false)
        {
            offset.x = transform.position.x - player.transform.position.x;
            offset.z = transform.position.z - player.transform.position.z;
            newtrans = transform.position;
            //not taking y as we won't update y position. 
        }
    }

    void LateUpdate()
    {
        //Create a new position by knowing where the player is
        newtrans.x = player.transform.position.x + offset.x;
        newtrans.z = player.transform.position.z + offset.z;
        //Move the camera to that position
        transform.position = newtrans;
    }
}