using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void UnPauseGame()
    {
        isPaused = false; 
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        isPaused = true; 
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

public void quitGame()
    {
        Application.Quit();
        Debug.Log("gameQuit");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
