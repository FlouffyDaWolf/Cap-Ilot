using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] string _registerSceneName;
    [SerializeField] string _mainIslandScene;

    public void BeginGame()
    {
        if (!SaveManager.LoadGameData())
        {
            SceneSystem.Instance.LoadAdditive(_registerSceneName);
        }
        else
        {
            SceneSystem.Instance.LoadSingle(_mainIslandScene);
        }
    }
}
