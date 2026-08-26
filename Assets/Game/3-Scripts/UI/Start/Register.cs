using TMPro;
using UnityEngine;

public class Register : MonoBehaviour
{
    [SerializeField] TMP_InputField _playerNameInput;
    [SerializeField] TMP_InputField _nurseEmailInput;

    public void FinishRengister()
    {
        // validation part
        string playerName = _playerNameInput.text;
        string nurseEmail = _nurseEmailInput.text;


        // end validation part

        PlayerInfo playerInfo = new() {PlayerName = playerName, NurseEmail = nurseEmail};
        SaveManager.InitPlayerInfo(playerInfo);

        // Load next scene 
        // SceneManager.GetInstance().LoadScene(SceneManager.SceneNames.);
    }
}
