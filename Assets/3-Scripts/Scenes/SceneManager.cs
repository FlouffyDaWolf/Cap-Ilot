using UnityEngine;
using System.Collections;
using TMPro;            
using UnityEngine.UI;     

public class SceneManager : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Variables ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Public Variables --------------------------- //
    // Scenes allowed to be loaded
    public enum SceneNames
    {

    }

    // --------------------------- Private Variables --------------------------- //
    // Instance
    static SceneManager instance;

    // Unity Editor Objects
    [SerializeField] private Canvas loadingScreenCanvas;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Slider loadingSlider;

    // Coroutine to load a scene asynchronously
    Coroutine loadAsyncScene;


    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Unity Methods ----------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // Create a singleton instance of the SceneManager
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        loadingScreenCanvas.enabled = false;
    }

    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Public Methods ----------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Getters / Setters --------------------------- //
    // Get the instance of the SceneManager
    static public SceneManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("SceneManager instance is null. Make sure there is a SceneManager in the scene and to call it after the Awake.");
        }

        return instance;
    }

    // --------------------------- Main Methods --------------------------- //
    // Load a scene asynchronously
    public void LoadScene(SceneNames sceneName, string loadingString, string unloadingString)
    {
        if (loadAsyncScene != null)
        {
            Debug.LogWarning("A scene is already being loaded. Please wait for it to finish before loading another scene.");
            return;
        }
        loadAsyncScene = StartCoroutine(LoadSceneAsync(sceneName, loadingString, unloadingString));
    }

    // Reload the current scene asynchronously
    public void ReloadScene(string loadingString, string unloadingString)
    {
        LoadScene((SceneNames)System.Enum.Parse(typeof(SceneNames), UnityEngine.SceneManagement.SceneManager.GetActiveScene().name), loadingString, unloadingString);
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Private Methods ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // Coroutine to load a scene asynchronously
    private IEnumerator LoadSceneAsync(SceneNames sceneName, string loadingString, string unloadingString)
    {
        // Get the current scene name
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // UI
        loadingScreenCanvas.enabled = true;
        float progress = 0f;
        loadingText.text = loadingString;
        loadingSlider.value = progress;

        // Load new scene
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName.ToString());
        while (!asyncLoad.isDone)
        {
            progress = asyncLoad.progress;
            loadingSlider.value = progress;
            yield return null;
        }

        // Unload previous scene
        if (currentSceneName != sceneName.ToString())
        {

            // UI
            progress = 0f;
            loadingText.text = unloadingString;
            loadingSlider.value = progress;


            AsyncOperation asyncUnload = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentSceneName);
            while (!asyncUnload.isDone)
            {
                progress = asyncUnload.progress;
                loadingSlider.value = progress;
                yield return null;
            }
        }


        loadingScreenCanvas.enabled = false;
        loadAsyncScene = null;
    }


}
