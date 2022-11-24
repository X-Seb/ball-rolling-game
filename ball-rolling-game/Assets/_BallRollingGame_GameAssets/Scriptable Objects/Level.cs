using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public bool star1;
    public bool star2;
    public bool star3;
    [Header("If fastestTime = -1, then the player never finished the level. ")]
    public float fastestTime;

    private void OnEnable()
    {
        levelCompleted = PlayerPrefs.HasKey("Level_" + levelBuildIndex.ToString() + "_Completed");
        levelUnlocked = PlayerPrefs.HasKey("Level_" + levelBuildIndex.ToString() + "_Unlocked");
        star1 = PlayerPrefs.HasKey("Level_" + levelBuildIndex.ToString() + "_Star1");
        star1 = PlayerPrefs.HasKey("Level_" + levelBuildIndex.ToString() + "_Star1");
        star1 = PlayerPrefs.HasKey("Level_" + levelBuildIndex.ToString() + "_Star1");

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