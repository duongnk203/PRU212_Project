using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the functionality of the "Restart" button.
/// When clicked, it resets the game time scale and reloads the current scene.
/// </summary>
public class BtnRestart : BaseButton
{
    /// <summary>
    /// Called when the button is clicked.
    /// Logs a debug message, resumes time scale, and reloads the current scene.
    /// </summary>
    protected override void OnClick()
    {
        Debug.Log("Restart kich hoat");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
