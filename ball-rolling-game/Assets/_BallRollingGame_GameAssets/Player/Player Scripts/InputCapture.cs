using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputCapture : MonoBehaviour
{
    public static InputCapture instance;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] bool IsPlayerPressingP;
    [SerializeField] bool IsPlayerPressingR;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.P))
        {
            IsPlayerPressingP = true;
        }
        else
        {
            IsPlayerPressingP = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            IsPlayerPressingR = true;
        }
        else
        {
            IsPlayerPressingR = false;
        }
    }

    public bool ReturnIsPlayerPressingKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.P:
                return IsPlayerPressingP;
            case KeyCode.R:
                return IsPlayerPressingR;
            default: return false;
        }
    }
    public float ReturnHorizontalInput()
    {
        return horizontalInput;
    }

    public float ReturnVerticalInput()
    {
        return verticalInput;
    }
}