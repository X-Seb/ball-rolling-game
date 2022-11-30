using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This script counts how much time you spend in the level scene (while you're playing the game).
//Other scipts get information from the public variables in this script.
public class GameTimer : MonoBehaviour
{
    [Header("Variables relative to display the time: ")]
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _background;
    [SerializeField] private Level _level;
    [Header("Scripts: ")]
    public PlayerGameOver playerGameOver;
    [Header("This value is used by other scripts: ")]
    public float _totalElapsedTime = 0.0f;
    [Header("Other variables:")]
    [SerializeField] private float _timeLeft;

    private void Start()
    {
        _timeLeft = _level.timeToCompleteLevel;
        _totalElapsedTime = 0.0f;
    }

    private void Update()
    {
        //Only count the time if you're currently playing the game
        if (GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            _timeLeft -= Time.deltaTime;
            _totalElapsedTime += Time.deltaTime;
        }

        _timeText.text = Mathf.Floor(_timeLeft).ToString();

        if (_timeLeft >= 60)
        {
            _background.color = Color.green;
        }
        if (_timeLeft < 60 && _timeLeft > 30)
        {
            _background.color = Color.yellow;
        }
        if (_timeLeft < 30 && _timeLeft > 0)
        {
            _background.color = Color.red;  
        }
        if (_timeLeft <= 0)
        {
            playerGameOver.PlayerDied();
        }
    }
}