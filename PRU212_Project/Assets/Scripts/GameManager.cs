using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;  // Trạng thái pause
    public GameObject gamePausePanel;

    private void Start()
    {
        gamePausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                BackToMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            gamePausePanel.SetActive(true);
        }
        else
        {
            ResumeGame();
        }
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        gamePausePanel.SetActive(false);
    }

    void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); // Quay lại menu
    }

    void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1); // Load lại Scene 1

        // ✅ Reset Máu & Stamina
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.ResetHealth();
        }
        if (Stamina.Instance != null)
        {
            Stamina.Instance.ResetStamina();
        }

        // ✅ Reset Gold
        if (EconomyManager.Instance != null)
        {
            EconomyManager.Instance.ResetGold();
        }
    }

    void ReloadCurrentScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
