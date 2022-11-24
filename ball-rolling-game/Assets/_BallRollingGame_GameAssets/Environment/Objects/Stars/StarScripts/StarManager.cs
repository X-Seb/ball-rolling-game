using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [Header("Level: ")]
    [SerializeField] private Level _level;
    [Header("StarGameObject scripts: ")]
    [SerializeField] private StarGameObject _starGameObject1;
    [SerializeField] private StarGameObject _starGameObject2;
    [SerializeField] private StarGameObject _starGameObject3;
    [Header("Star information: ")]
    [SerializeField] private bool _star1CollectedDuringGame = false;
    [SerializeField] private bool _star2CollectedDuringGame = false;
    [SerializeField] private bool _star3CollectedDuringGame = false;
    [SerializeField] private bool _star1Collected = false;
    [SerializeField] private bool _star2Collected = false;
    [SerializeField] private bool _star3Collected = false;

    private void Start()
    {
        //Change the appearance of the stars, based on whether they're alreayd collected.
        if (PlayerPrefs.HasKey("Level_" + _level.levelBuildIndex.ToString() + "_Star1"))
        {
            _starGameObject1.StarAlreadyCollected();
            _star1Collected = true;
        }
        else
        {
            _starGameObject1.StarNotAlreadyCollected();
            _star1Collected = false;
        }
        if (PlayerPrefs.HasKey("Level_" + _level.levelBuildIndex.ToString() + "_Star2"))
        {
            _starGameObject2.StarAlreadyCollected();
            _star2Collected = true;
        }
        else
        {
            _starGameObject2.StarNotAlreadyCollected();
            _star2Collected = false;
        }
        if (PlayerPrefs.HasKey("Level_" + _level.levelBuildIndex.ToString() + "_Star3"))
        {
            _starGameObject3.StarAlreadyCollected();
            _star3Collected = true;
        }
        else
        {
            _starGameObject3.StarNotAlreadyCollected();
            _star3Collected = false;
        }
    }

    public void StarCollected(int starIndex)
    {
        if (starIndex == 1)
        {
            _star1CollectedDuringGame = true;
        }
        if (starIndex == 2)
        {
            _star2CollectedDuringGame = true;
        }
        if (starIndex == 3)
        {
            _star3CollectedDuringGame = true;
        }
    }
    public void LevelComplete()
    {
        //Save the stars the player just collected in player prefs.
        if (_star1CollectedDuringGame)
        {
            PlayerPrefs.SetInt("Level_" + _level.levelBuildIndex.ToString() + "_Star1", 1);
        }
        if (_star2CollectedDuringGame)
        {
            PlayerPrefs.SetInt("Level_" + _level.levelBuildIndex.ToString() + "_Star2", 1);
        }
        if (_star3CollectedDuringGame)
        {
            PlayerPrefs.SetInt("Level_" + _level.levelBuildIndex.ToString() + "_Star3", 1);
        }
        PlayerPrefs.Save();
    }
}