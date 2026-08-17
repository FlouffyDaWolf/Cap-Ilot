using UnityEngine;

public class SceneLoader : MonoBehaviour
{    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Variables ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Private Variables --------------------------- //
    // Reference to Managers
    private SceneManager sceneManager;

    [Header("Scene Loader Settings")]
    // Scene to load
    [Tooltip("The scene to load when the player enters the trigger.")]
        [SerializeField] private SceneManager.SceneNames sceneToLoad;

    [Header("Loading Screen Settings")]
    // Text on loading new scene
    [Tooltip("The text to display on the loading screen when loading the new scene.")]
        [SerializeField] private string loadingText = "Loading new zone...";
    // Text on unloading scene
    [Tooltip("The text to display on the loading screen when unloading the previous scene.")]
        [SerializeField] private string unloadingText = "Unloading previous zone...";

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Unity Methods ----------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    private void Start()
    {
        sceneManager = SceneManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Private Methods ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // Load the scene on Trigger
    private void LoadScene()
    {
        if (sceneManager != null)
        {
            sceneManager.LoadScene(sceneToLoad, loadingText, unloadingText);
        }
        else
        {
            Debug.LogError("SceneManager instance is not available.");
        }
    }

}
