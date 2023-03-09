using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameEvent OnPauseGame;
    public GameEvent OnResumeGame;
    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnPauseGame.Raise();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        OnResumeGame.Raise();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
