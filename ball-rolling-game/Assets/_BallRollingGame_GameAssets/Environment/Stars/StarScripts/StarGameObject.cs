using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGameObject : MonoBehaviour
{
    [Header("Scripts: ")]
    [SerializeField] private StarManager _starManager;
    [Header("Star index go from 1 to 3: ")]
    [SerializeField] private int _starIndex;
    [Header("Other information: ")]
    [SerializeField] private bool _isStarCollected = false;
    private void OnCollisionEnter(Collision collision)
    {
        //When the player collides with the star, do stuff.
        if (collision.gameObject.CompareTag("Player") &&
            GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            _starManager.StarCollected(_starIndex);
            StarCollected();
        }
    }

    public void StarCollected()
    {
        //Make the star spin around and dissapear, play sound, turn off the light
    }

    public void StarNotAlreadyCollected()
    {
        _isStarCollected = false;
    }
    public void StarAlreadyCollected()
    {
        //Make the star grey, change the lighting of the star, change noise
        _isStarCollected = true;
    }
}