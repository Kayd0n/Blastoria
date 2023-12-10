using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Arena");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void EndOfGame()
    {
        SceneManager.LoadSceneAsync("EndOfGame");
    }
}
