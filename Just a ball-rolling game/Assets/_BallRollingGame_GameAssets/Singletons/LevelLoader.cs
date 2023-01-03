using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.SearchService;

//This is a Singleton that allows you to switch between scenes.
//It also activates the loading bar canvas to display the progress.
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    [Header("UI stuff: ")]
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI funnyText;

    [Header("Other Scripts elements specific to certain scenes: ")]
    [SerializeField] MainMenuManager mainMenuManager;

    [Header("This value tells the UI manager if you start playing as soon as the scene loads.")]
    [SerializeField] private bool _quickStart = false;

    [Header("This is the list of available string for the funnyText: ")]
    [SerializeField] private List<string> funnyTextOptions;

    [Header("Progress bar variables: ")]
    [SerializeField] float targetSliderValue = 0.0f;
    [SerializeField] bool _isSliderVisible = false;

    private void Awake()
    {
        //This makes this script a singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            loadingCanvas.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        slider.value = 0.0f;
        targetSliderValue = 0.0f;

        GetMainMenuManager();
    }

    private void Update()
    {
        //If it's visible, change the slider's value and the text percentage
        if (_isSliderVisible == true)
        {
            slider.value = Mathf.MoveTowards(slider.value, targetSliderValue, 100 * Time.deltaTime);
            progressText.text = Mathf.Round((Mathf.MoveTowards(slider.value, targetSliderValue, 100 * Time.deltaTime))).ToString() + "%";
        }
    }

    //This functions loads the next scene
    public async void LoadSceneAsync(int sceneBuildIndex)
    {
        Debug.Log("Starting to load the next scene");

        AudioSingleton.Instance.SetVolumeGradually(0.0f, 1.5f);
        //Unpauses the game so the UI displays correctly.
        Time.timeScale = 1.0f;
        
        //Checks if you're in a game Level to use the UIManager, which is only present in the level scenes
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GetMainMenuManager();
            mainMenuManager.LeavingScene();
        }
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //Deactivate the mainCanvas
            UIManager.instance.ChangingScene();
        }

        //Randomizes the funny text + resets the slider's target value to 0.
        RandomizeFunnyText();
        targetSliderValue = 0;

        var scene = SceneManager.LoadSceneAsync(sceneBuildIndex);

        //This makes it so the scene doesn't change on its own.
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);
        _isSliderVisible = true;
        GameManager.instance.SetGameState(GameManager.GameState.LEAVING_SCENE);

        do
        {
            //Update the visual indicators to show the loading progress
            targetSliderValue = scene.progress * 111;
        } while (scene.progress < 0.9f);

        //Wait one and a half seconds before changing scenes and reseting everything
        await Task.Delay(1500);
        AudioSingleton.Instance.StopMusic();

        Debug.Log("Scene loaded!");
        //Allow the next scene to activate, which changes the scene
        scene.allowSceneActivation = true;

        //Turn off the loading screen and reset certain values
        loadingCanvas.SetActive(false);
        _isSliderVisible = false;
        targetSliderValue = 0;
        slider.value = 0;

        await Task.Delay(100);
        GetMainMenuManager();
    }

    private void RandomizeFunnyText()
    {
        //Randomly sets the funny text to one of the options in the array
        int randomIndex = Random.Range(0, funnyTextOptions.Count);
        funnyText.text = funnyTextOptions[randomIndex];
    }

    public void SetQuickStart(bool value)
    {
        _quickStart = value;
    }

    public bool ReturnQuickStart()
    {
        return _quickStart;
    }

    public void GetMainMenuManager()
    {
        //Assigns the MainMenuManager script to its varialbe if you're in the main menu
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>();
        }
    }
}