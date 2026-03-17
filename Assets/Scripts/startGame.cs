using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public void go()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void goButLevelTwo()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }

    public void exitMainMenu()
    {
        SceneManager.LoadSceneAsync("Level Select");
    }
    public void goodBAAH()
    {
        Application.Quit();
    }
}
