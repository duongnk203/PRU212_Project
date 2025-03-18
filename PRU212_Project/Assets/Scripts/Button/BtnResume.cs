using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Handles the functionality of the "Resume" button.
/// When clicked, it resumes the game by setting time scale to 1 and hides the pause panel.
/// </summary>
public class BtnResume : BaseButton
{
    /// <summary>
    /// The pause panel that will be deactivated when resuming the game.
    /// </summary>
    public GameObject gamePanel;

    /// <summary>
    /// Called when the button is clicked.
    /// Logs a debug message, resumes the game, and hides the pause menu.
    /// </summary>
    protected override void OnClick()
    {
        Debug.Log("Tiep tuc");
        Time.timeScale = 1;
        gamePanel.SetActive(false);
    }
}
