using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the functionality of the "Instructions" button.
/// When clicked, it logs a message and loads the instructions scene (Scene index 2).
/// </summary>
public class BtnInstructionMenu : BaseButton
{
    /// <summary>
    /// Called when the button is clicked.
    /// Logs a debug message and loads the instructions scene.
    /// </summary>
    protected override void OnClick()
    {
        Debug.Log("Btn instruction is active");
        SceneManager.LoadScene(2);
    }
}
