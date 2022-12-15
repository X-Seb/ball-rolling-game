using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: This script manages interactions with the buttons at the bottom of the level selection screen.
//TODO: It allows you to switch between the level information shown.
public class LevelSelectionManager : MonoBehaviour
{
    [Header("The buttons that change the levels: ")]
    [SerializeField] private GameObject _button1;
    [SerializeField] private GameObject _button2;

    [Header("The index of the current level set: ")]
    [SerializeField] private int _currentLevelSet = 1;


    public void SetLevels(int buttonNumber)
    {

    }

    public int ReturnActiveLevelSet()
    {
        return _currentLevelSet;
    }
}