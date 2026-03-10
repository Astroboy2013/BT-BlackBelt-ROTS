using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RefreshGame : MonoBehaviour
{
    public GameManager Manager;
    public void Reload()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        if (!Manager.isPaused)
        {
            Manager.isPaused = true;
        }
        else
        {
            Manager.isPaused = false;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
