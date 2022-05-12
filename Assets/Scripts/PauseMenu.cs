using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            } else
            {
                PauseMenuOpen();
            }
        }  
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }
    private void PauseMenuOpen()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(2);
    }
}
