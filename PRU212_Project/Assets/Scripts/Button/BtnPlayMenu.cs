using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the functionality of the "Play" button in the main menu.
/// When clicked, it starts the game by loading the gameplay scene (Scene index 1).
/// </summary>
public class BtnPlayMenu : BaseButton
{
    /// <summary>
    /// Called when the button is clicked.
    /// Logs a debug message and loads the gameplay scene.
    /// </summary>
    protected override void OnClick()
    {
        Debug.Log("Btn play is active");
        SceneManager.LoadScene(1);
    }
}
