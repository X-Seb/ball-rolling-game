using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;

public class LevelSelectionManager : MonoBehaviour
{
    [Header("UI elements: ")]
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Transform _allLevelInfoDisplays;

    [Header("Info relative to the movement: ")]
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    private int _positionIndex = 0;


    void Start()
    {
        //Deactivate the left button
        _leftButton.enabled = false;
        //Set the right button to be active if level 4 has been completed
        if (PlayerPrefs.HasKey("Level_4_Completed"))
        {
            _rightButton.enabled = true;
        }
        else
        {
            _rightButton.enabled = false;
        }
    }

    public void RightButton()
    {
        //Checks if you can move left based on your current position and the levels you've completed
        if (_positionIndex == 0 && PlayerPrefs.HasKey("Level-4_Completed"))
        {
            //Moves all the levels info displays to the left, so you can see 4 harder levels
            DeactivateButtons();
            Vector3 displacement = new Vector3(-800, 0, 0);
            _allLevelInfoDisplays.DOMove(displacement, _duration).SetEase(_ease).OnComplete(MovedLeft);
        }
    }

    public void LeftButton()
    {
        //Checks if you can move left based on your current position
        if (_positionIndex != 0)
        {
            //Moves all the level info displays to the right, so you can go back and see the easier levels
            DeactivateButtons();
            Vector3 displacement = new Vector3(800, 0, 0);
            _allLevelInfoDisplays.DOMove(displacement, _duration).SetEase(_ease).OnComplete(MovedRight);
        }
    }

    private void MovedLeft()
    {
        _positionIndex++;
        if (_positionIndex == 1)
        {
            _rightButton.enabled = false;
            _leftButton.enabled = true;
        }
    }

    private void MovedRight()
    {
        _positionIndex--;
        if (_positionIndex == 0)
        {
            _rightButton.enabled = true;
            _leftButton.enabled = false;
        }
    }

    private void DeactivateButtons()
    {
        _leftButton.enabled = false;
        _rightButton.enabled = false;
    }
}
