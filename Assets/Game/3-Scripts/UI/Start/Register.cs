using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    [SerializeField] TMP_InputField _playerNameInput;
    [SerializeField] TMP_InputField _nurseEmailInput;

    [SerializeField] string _mainIslandScene;

    public void FinishRegister()
    {
        // validation part
        string playerName = _playerNameInput.text;
        string nurseEmail = _nurseEmailInput.text;


        // end validation part

        PlayerInfo playerInfo = new() {PlayerName = playerName, NurseEmail = nurseEmail};
        SaveManager.InitPlayerInfo(playerInfo);

        // Load next scene 
        SceneSystem.Instance.LoadSingle(_mainIslandScene);
    }
}
