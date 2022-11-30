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

    [Header("This is the list of available string for the funnyText: ")]
    [SerializeField] private List<string> funnyTextOptions;

    [Header("The value of the progress bar: ")]
    [SerializeField] float targetSliderValue = 0.0f;

    private void Awake()
    {
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

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>();
        }
    }

    private void Update()
    {
        slider.value = Mathf.MoveTowards(slider.value, targetSliderValue, 100 * Time.deltaTime);
        progressText.text = Mathf.Round((Mathf.MoveTowards(slider.value, targetSliderValue, 100 * Time.deltaTime))).ToString() + "%";
    }

    public async void LoadSceneAsync(int sceneBuildIndex)
    {
        AudioSingleton.Instance.StopMusic();
        //Unpauses the game so the UI displays correctly.
        Time.timeScale = 1.0f;
        Debug.Log("Starting to load the next scene");
        
        //Checks if you're in a game Level to use the UIManager, which is only present in game Scenes
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuManager.LeavingScene();
        }
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //Deactivate the mainCanvas
            UIManager.instance.ChangingScene();
        }

        //Randomizes the text + resets the values to 0.
        RandomizeFunnyText();
        targetSliderValue = 0;

        var scene = SceneManager.LoadSceneAsync(sceneBuildIndex);

        //This makes it so the scene doesn't change on its own.
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);
        GameManager.instance.SetGameState(GameManager.GameState.LEAVING_SCENE);

        do
        {
            //Update the visual indicators to show the loading progress
            targetSliderValue = scene.progress * 110;
        } while (scene.progress < 0.9f);

        //Wait one and a half seconds
        await Task.Delay(1500);


        Debug.Log("Scene loaded!");
        //Allow the next scene to activate, which changes the scene
        scene.allowSceneActivation = true;
        loadingCanvas.SetActive(false);
        targetSliderValue = 0;
    }

    private void RandomizeFunnyText()
    {
        int randomIndex = Random.Range(0, funnyTextOptions.Count);
        funnyText.text = funnyTextOptions[randomIndex];
    }
}