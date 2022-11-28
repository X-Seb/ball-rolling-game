using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoDisplay : MonoBehaviour
{
    [Header("Text: ")]
    [SerializeField] private TextMeshProUGUI _levelNameText;
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

    [SerializeField] private Color _barDeactivated = new Color(60, 71, 73);
    [Header("Others: ")]
    [SerializeField] private Image _timeBackground;
    [SerializeField] private Button _playButton;
    [SerializeField] private Level _level;

    private void Start()
    {
        SetButtonActive();
        SetText();
        SetDifficultyColors();
        SetTimeColor();
    }

    public void PlayLevel()
    {
        AudioSingleton.Instance.PlayButtonSound();
        PlayerPrefs.SetInt("FirstPlay", 1);
        LevelLoader.instance.LoadSceneAsync(_level.levelBuildIndex);
    }

    private void SetButtonActive()
    {
        if (_level.levelUnlocked)
        {
            _playButton.interactable = true;
        }
        else
        {
            _playButton.interactable = false;
        }
        if (_level == null)
        {
            _playButton.interactable = false;
        }
    }
    private void SetText()
    {
        //Set the level name, description, and the time to complete it
        _levelNameText.text = (_level.levelBuildIndex).ToString() + ": "+ _level.levelName;
        _timeText.text = (_level.timeToCompleteLevel).ToString();
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