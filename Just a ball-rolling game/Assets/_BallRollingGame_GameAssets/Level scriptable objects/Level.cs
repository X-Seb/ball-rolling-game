using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
///This script creates a scriptable object for all of the game's levels.
///From the "assets" section, you can create a new level and modify it's properties.
///You can then use that level as a value for variables in other scripts.
/// </summary>
[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    [Header("General level info: ")]
    public string levelName;
    public string levelDescription;
    public Image levelImage;

    [Header("Technical level data: ")]
    public int levelDifficulty;
    public int timeToCompleteLevel;
    public int levelBuildIndex;

    [Header("These values are changed when the game object is enabled to match the PlayerPrefs data")]
    public bool levelCompleted;
    public bool levelUnlocked;

    [Header("If fastestTime = -1, then the player never finished the level. ")]
    public float fastestTime;

    private void OnEnable()
    {
        levelCompleted = PlayerPrefs.HasKey("Level_" + levelBuildIndex.ToString() + "_Completed");
        levelUnlocked = PlayerPrefs.HasKey("Level_" + levelBuildIndex.ToString() + "_Unlocked");

        if (PlayerPrefs.HasKey("Level_" + levelBuildIndex.ToString() + "_FastestTime"))
        {
            fastestTime = PlayerPrefs.GetFloat("Level_" + levelBuildIndex.ToString() + "_FastestTime");
        }
        else
        {
            fastestTime = -1.0f;
        }
    }
}