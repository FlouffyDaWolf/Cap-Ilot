using UnityEngine;
using UnityEngine.InputSystem;

public class TestResultsMenu : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            ResultsMenu.Instance.HideResults();
            ResultsMenu.Instance.ShowResults(0);
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            ResultsMenu.Instance.HideResults();
            ResultsMenu.Instance.ShowResults(1);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            ResultsMenu.Instance.HideResults();
            ResultsMenu.Instance.ShowResults(2);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            ResultsMenu.Instance.HideResults();
            ResultsMenu.Instance.ShowResults(3);
        }
    }
}
