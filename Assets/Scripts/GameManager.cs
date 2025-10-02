using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("External Game Objects")]
    public TMP_Text enemyCountText;
    public health playerHealth;
    public playerBehaviour playerScript;
    public Slider fuelBar;
    public GameObject deathScreen;

    [Header("Other Variables")]
    public int totalEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdateFirstEnemyCount", 0.5f);
        Invoke("SetRoundBool", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        // Ends the game when there are no enemies
        if (totalEnemyCount <= 0)
        {
            Debug.Log("Game shall end");
            SceneManager.LoadSceneAsync("Win");
        }

        if (playerHealth.currentHealth <= 0)
        {
            deathScreen.SetActive(true);
        }

        fuelBar.value = playerScript.fuel;
    }
    void UpdateFirstEnemyCount()
    {
        updateText(totalEnemyCount);
    }

    public void SetEnemyTotalCount(int value) { 
        totalEnemyCount = value;
    }
    
    public void ReduceEnemyTotalCount()
    {
        totalEnemyCount--;
        updateText(totalEnemyCount);
    }

    public void updateText(int rawInt)
    {
        // Updates the enemy counter text
        enemyCountText.text = "Enemies Left: " + rawInt.ToString();
    }

    private void SetRoundBool()
    {
        isStarted = true;
    }
    
}
