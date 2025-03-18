using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the functionality of the "Back to Menu" button.
/// When clicked, it logs a message and loads the main menu scene (Scene index 0).
/// </summary>
public class BtnBackMenu : BaseButton
{

    /// <summary>
    /// Called when the button is clicked.
    /// Logs a debug message and loads the main menu scene.
    /// </summary>
    protected override void OnClick()
    {
        Debug.Log("Btn back menu is active");
        SceneManager.LoadScene(0);
    }
}
