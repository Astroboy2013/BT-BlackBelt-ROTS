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
    public Fire shootScript;
    public Transform playerTransform;
    public GameObject tooFarScreen;
    public GameObject deathScreen;
    public GameObject winScreen;
    public GameObject gameModeText;
    public GameObject lowFuelSign;

    [Header("Territorial Occupation Only")]
    public TMP_Text territorySign;
    public GameObject territorySignObj;
    public territoryCode[] captureValues;
    public GameObject[] territories;

    [Header("Other Variables")]
    public int totalEnemyCount = 0;
    public int foughtTerritory;
    public int distanceThreshold;
    public bool isPaused = false;

    [Header("Counters and Sliders")]
    public Slider fuelBar;
    public Slider healthBar;
    public TMP_Text healthNumber;
    public TMP_Text fuelNumber;
    public TextMeshProUGUI ammoCounter;

    private int textBlinkCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = playerHealth.maxHealth;

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

        if (playerTransform != null)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) > distanceThreshold)
            {
                tooFarScreen.SetActive(true);
            }
            else
            {
                tooFarScreen.SetActive(false);
            }
        }

        if (playerHealth.currentHealth <= 0.1f)
        {
            deathScreen.SetActive(true);
        }

        if (playerScript.fuel <= 40)
        {
            lowFuelSign.SetActive(true);
        }
        else
        {
            lowFuelSign.SetActive(false);
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
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

        healthBar.value = Mathf.Round(playerHealth.currentHealth);
        healthNumber.text = Mathf.Round(playerHealth.currentHealth).ToString();
        fuelBar.value = playerScript.fuel;
        fuelNumber.text = Mathf.Round(playerScript.fuel).ToString();
        ammoCounter.text = shootScript.currentAmmo.ToString();

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            CheckFoughtTerritories(captureValues);
        }
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
            if (territoriesInput[i].percentage >= -50)
            {
                foughtTerritory = i;
                break;
            }
        }

        if (territoriesInput[0].percentage < -99)
        {
             deathScreen.SetActive(true);
        }

        if(territoriesInput[territoriesInput.Length - 1].percentage > 0)
        {
            winScreen.SetActive(true);
        }
    }

    public void Reload()
    {
        if (playerScript.isFueling)
        {
            shootScript.currentAmmo = shootScript.maxAmmo;
        }
    }
}
