using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the functionality of the "Quit" button in the pause menu.
/// When clicked, it logs a message and returns to the main menu (Scene index 0).
/// </summary>
public class BtnQuit : BaseButton
{
    /// <summary>
    /// Called when the button is clicked.
    /// Logs a debug message and loads the main menu scene.
    /// </summary>
    protected override void OnClick()
    {
        Debug.Log("Quit duoc bat");
        SceneManager.LoadScene(0);
    }

}
