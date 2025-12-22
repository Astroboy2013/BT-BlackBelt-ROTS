using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectMenu : MonoBehaviour
{
    public void go()
    {
        SceneManager.LoadSceneAsync("Level Select");
        // Set to "Level Select" later
    }
}
