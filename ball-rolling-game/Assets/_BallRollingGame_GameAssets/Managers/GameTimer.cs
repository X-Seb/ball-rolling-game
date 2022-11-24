using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public PlayerGameOver playerGameOver;
    [Header("Variables relative to display the time: ")]
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _background;
    [SerializeField] private Level _level;
    [Header("For reference only: ")]
    [SerializeField] float _totalTime;
    public float _totalElapsedTime = 0.0f;

    private void Start()
    {
        _totalTime = _level.timeToCompleteLevel;
        _totalElapsedTime = 0.0f;
    }

    private void Update()
    {
        if (GameManager.instance.ReturnCurrentGameState() == GameManager.GameState.PLAYING_GAME)
        {
            _totalTime -= Time.deltaTime;
            _totalElapsedTime += Time.deltaTime;
        }

        _timeText.text = Mathf.Floor(_totalTime).ToString();

        if (_totalTime >= 60)
        {
            _background.color = Color.green;
        }
        if (_totalTime < 60 && _totalTime > 30)
        {
            _background.color = Color.yellow;
        }
        if (_totalTime < 30 && _totalTime > 0)
        {
            _background.color = Color.red;  
        }
        if (_totalTime <= 0)
        {
            playerGameOver.PlayerDied();
        }
    }
}