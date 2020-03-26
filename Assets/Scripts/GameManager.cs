using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //for pause
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        //for pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //Scenes

    //MainMenu
    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Cresits");
    }

    //General Buttons
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Score Stuff
}
