using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script sets a maximum speed for a rigidbody in all directions
public class LimitRigidbodySpeed : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] bool isInformationTextActive = false;
    [SerializeField] Rigidbody rb;

    void Update()
    {
        // Trying to Limit Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    void OnGUI()
    {
        //Displays the rigidbody's velocity if "isInformationTextActive" bool is true
        if (isInformationTextActive)
        {
            GUI.Label(new Rect(20, 20, 200, 200), "rigidbody velocity: " + rb.velocity);
        }
    }
}