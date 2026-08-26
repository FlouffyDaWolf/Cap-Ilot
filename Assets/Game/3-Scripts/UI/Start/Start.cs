using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] string _registerSceneName;

    public void BeginGame()
    {
        if (!SaveManager.LoadGameData())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_registerSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        else
        {
            // load main island
        }
    }
}
