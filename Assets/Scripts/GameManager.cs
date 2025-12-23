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
    public TMP_Text territorySign;
    public GameObject territorySignObj;
    public health playerHealth;
    public PlayerBehaviour playerScript;
    public Slider fuelBar;
    public GameObject deathScreen;
    public GameObject winScreen;
    public GameObject engineOffIndicator;
    public GameObject gameModeText;
    public territoryCode[] captureValues;
    public GameObject[] territories;

    [Header("Other Variables")]
    public int totalEnemyCount = 0;
    public string gameMode;
    public int foughtTerritory;

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
            if (SceneManager.GetActiveScene().buildIndex == 2)
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

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (playerScript.currentTerritory != null)
            {
                territorySignObj.SetActive(true);
                territorySign.text = playerScript.currentTerritory;
            }
            else
            {
                territorySignObj.SetActive(false);
            }
        }

        fuelBar.value = playerScript.fuel;
        CheckFoughtTerritories(captureValues);
    }
    void UpdateFirstEnemyCount()
    {
        updateText(totalEnemyCount, SceneManager.GetActiveScene().buildIndex);
    }

    public void SetEnemyTotalCount(int value) { 
        totalEnemyCount = value;
    }
    
    public void ReduceEnemyTotalCount()
    {
        totalEnemyCount--;
        updateText(totalEnemyCount, SceneManager.GetActiveScene().buildIndex);
    }

    public void updateText(int rawInt, int mode)
    {
        if (mode == 2)
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

    private void CheckFoughtTerritories(territoryCode[] territoriesInput)
    {
        for (int i = territoriesInput.Length - 1; i > 0; i--)
        {
            Debug.Log(territoriesInput[i].percentage);
            if (territoriesInput[i].percentage >= 0)
            {
                foughtTerritory = i;
                break;
            }
        }

        if(territoriesInput[0].percentage < 0)
        {
            deathScreen.SetActive(true);
        }

        if(territoriesInput[territoriesInput.Length - 1].percentage > 0)
        {
            winScreen.SetActive(true);
        }
    }
}
