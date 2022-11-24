using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartingScreenDisplay : MonoBehaviour
{
    [Header("Text: ")]
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _levelDescriptionText;
    [SerializeField] private TextMeshProUGUI _timeText;

    [Header("The difficulty bars: ")]
    [SerializeField] private Image _bar1;
    [SerializeField] private Image _bar2;
    [SerializeField] private Image _bar3;
    [SerializeField] private Image _bar4;
    [SerializeField] private Image _bar5;

    [Header("Difficulty bar colors: ")]
    [SerializeField] private Color _colorBar1 = new Color(153, 255, 51);
    [SerializeField] private Color _colorBar2 = new Color(255, 255, 0);
    [SerializeField] private Color _colorBar3 = new Color(255, 153, 0);
    [SerializeField] private Color _colorBar4 = new Color(255, 51, 0);
    [SerializeField] private Color _colorBar5 = new Color(255, 51, 0);

    [Header("Stars: ")]
    [SerializeField] private Image _star1;
    [SerializeField] private Image _star2;
    [SerializeField] private Image _star3;
    [Header("Other colors: ")]
    [SerializeField] private Color _starCollected = new Color(255, 221, 97);
    [SerializeField] private Color _starUncollected = new Color(100, 110, 135);
    [SerializeField] private Color _barDeactivated = new Color(60, 71, 73);
    [Header("Others: ")]
    [SerializeField] private Image _timeBackground;
    [SerializeField] private Level _level;


    private void Start()
    {
        SetText();
        SetDifficultyColors();
        SetStarsColor();
        SetTimeColor();
    }

    private void SetText()
    {
        //Set the level name, description, and the time to complete it
        _levelNameText.text = _level.levelName;
        _levelDescriptionText.text = _level.levelDescription;
        _timeText.text = (_level.timeToCompleteLevel).ToString() + " seconds";
    }

    private void SetDifficultyColors()
    {
        //Set the difficulty bars to the right color
        switch (_level.levelDifficulty)
        {
            case 5:
                _bar1.color = _colorBar1;
                _bar2.color = _colorBar2;
                _bar3.color = _colorBar3;
                _bar4.color = _colorBar4;
                _bar5.color = _colorBar5;
                break;
            case 4:
                _bar1.color = _colorBar1;
                _bar2.color = _colorBar2;
                _bar3.color = _colorBar3;
                _bar4.color = _colorBar4;
                _bar5.color = _barDeactivated;
                break;
            case 3:
                _bar1.color = _colorBar1;
                _bar2.color = _colorBar2;
                _bar3.color = _colorBar3;
                _bar4.color = _barDeactivated;
                _bar5.color = _barDeactivated;
                break;
            case 2:
                _bar1.color = _colorBar1;
                _bar2.color = _colorBar2;
                _bar3.color = _barDeactivated;
                _bar4.color = _barDeactivated;
                _bar5.color = _barDeactivated;
                break;
            case 1:
                _bar1.color = _colorBar1;
                _bar2.color = _barDeactivated;
                _bar3.color = _barDeactivated;
                _bar4.color = _barDeactivated;
                _bar5.color = _barDeactivated;
                break;
            default:
                _bar1.color = _barDeactivated;
                _bar2.color = _barDeactivated;
                _bar3.color = _barDeactivated;
                _bar4.color = _barDeactivated;
                _bar5.color = _barDeactivated;
                break;
        }
    }

    private void SetStarsColor()
    {
        if (_level.star1)
        {
            _star1.color = _starCollected;
        }
        else
        {
            _star1.color = _starUncollected;
        }
        if (_level.star2)
        {
            _star2.color = _starCollected;
        }
        else
        {
            _star2.color = _starUncollected;
        }
        if (_level.star3)
        {
            _star3.color = _starCollected;
        }
        else
        {
            _star3.color = _starUncollected;
        }
    }

    private void SetTimeColor()
    {
        //Set the color of the time background, depending on how long you have to complete it
        if (_level.timeToCompleteLevel > 60)
        {
            _timeBackground.color = Color.green;
        }
        if (_level.timeToCompleteLevel > 30 && _level.timeToCompleteLevel <= 60)
        {
            _timeBackground.color = Color.yellow;
        }
        if (_level.timeToCompleteLevel > 0 && _level.timeToCompleteLevel <= 30)
        {
            _timeBackground.color = Color.red;
        }
    }
}