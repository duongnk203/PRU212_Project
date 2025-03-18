using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Handles the functionality of the "Pause" button.
/// When clicked, it pauses the game and displays the pause panel.
/// </summary>
public class BtnPause : BaseButton
{
    /// <summary>
    /// The pause menu panel that will be displayed when the game is paused.
    /// </summary>
    public GameObject gamePausePanel;

    /// <summary>
    /// Called when the button is clicked.
    /// Pauses the game by setting Time.timeScale to 0 and activates the pause panel.
    /// </summary>
    protected override void OnClick()
    {
        Debug.Log("Pause duoc bat");
        Time.timeScale = 0;
        gamePausePanel.SetActive(true);
    }
}
