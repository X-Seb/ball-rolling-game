using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPManager : MonoBehaviour
{
    private void Start()
    {
        //Unlocks the first level if you never played before
        if (!PlayerPrefs.HasKey("Level_1_Unlocked"))
        {
            PlayerPrefs.SetInt("Level_1_Unlocked", 1);
            PlayerPrefs.Save();
        }
    }
}