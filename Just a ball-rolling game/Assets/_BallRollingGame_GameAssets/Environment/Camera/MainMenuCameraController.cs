using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to rotate the camera in the main menu scene‰
public class MainMenuCameraController : MonoBehaviour
{
    [SerializeField] GameObject _gameObjectToRotate;
    [Header("Note: The angle has to be very small since the object is rotated by that amount in FixedUpdate.")]
    [SerializeField] private float xAngle, yAngle, zAngle;

    private void LateUpdate()
    {
        Vector3 rotation = new Vector3(0, 0, 0);
        _gameObjectToRotate.transform.Rotate(xAngle * Time.deltaTime,
            yAngle * Time.deltaTime,
            xAngle * Time.deltaTime,
            Space.Self);
    }
}