using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LimitRigidbodySpeed : MonoBehaviour
{
    public float maxSpeed = 10f;
    public bool isInformationTextActive = false;
    public Rigidbody rb;

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
        if (isInformationTextActive)
        {
            GUI.Label(new Rect(20, 20, 200, 200), "rigidbody velocity: " + rb.velocity);
        }
    }
}