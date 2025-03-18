using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

/// <summary>
/// Handles the functionality of the "Exit" button.
/// When clicked, it logs a message and quits the application.
/// </summary>
public class BtnExitMenu : BaseButton
{
    /// <summary>
    /// Called when the button is clicked.
    /// Logs a debug message and exits the application.
    /// </summary>
    protected override void OnClick()
    {
        Debug.Log("Btn Exit is active");
        Application.Quit();
    }
}
