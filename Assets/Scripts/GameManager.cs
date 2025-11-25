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
    public PlayerBehaviour playerScript;
    public Slider fuelBar;
    public GameObject deathScreen;
    public GameObject winScreen;
    public GameObject engineOffIndicator;
    public GameObject gameModeText;
    public string gameMode;

    [Header("Other Variables")]
    public int totalEnemyCount = 0;

    private int textBlinkCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdateFirstEnemyCount", 0.5f);

        //Game Mode text blinking animation
        AnimateGameModeText();
    }

    // Update is called once per frame
    void Update()
    {
        // Ends the game when there are no enemies
        if (totalEnemyCount <= 0)
        {
            if (gameMode == "Elimination")
            {
                winScreen.SetActive(true);
            }
        }

        if (playerHealth.currentHealth <= 0)
        {
            deathScreen.SetActive(true);
        }

        if(playerScript.isMoving)
        {
            engineOffIndicator.SetActive(false);
        }
        else
        {
            engineOffIndicator.SetActive(true);
        }

        fuelBar.value = playerScript.fuel;
    }
    void UpdateFirstEnemyCount()
    {
        updateText(totalEnemyCount, gameMode);
    }

    public void SetEnemyTotalCount(int value) { 
        totalEnemyCount = value;
    }
    
    public void ReduceEnemyTotalCount()
    {
        totalEnemyCount--;
        updateText(totalEnemyCount, gameMode);
    }

    public void updateText(int rawInt, string mode)
    {
        if (mode == "Elimination")
        {
            // Updates the enemy counter text
            enemyCountText.text = "Enemies Left: " + rawInt.ToString();
        }
    }
    
    private void AnimateGameModeText()
    {
        if (gameModeText != null)
        {
            Invoke("HideModeTextTemporary", 0.3f);
        }
    }
    private void ShowModeText()
    {
        gameModeText.SetActive(true);

        if (textBlinkCount <= 0)
        {
            Invoke("HideModeTextPermanent", 0.3f);
        }
        else
        {
            Invoke("HideModeTextTemporary", 0.3f);
            textBlinkCount--;
        }
    }

    private void HideModeTextTemporary()
    {
        gameModeText.SetActive(false);
        Invoke("ShowModeText", 0.3f);
    }

    private void HideModeTextPermanent()
    {
        gameModeText.SetActive(false);
    }

}
