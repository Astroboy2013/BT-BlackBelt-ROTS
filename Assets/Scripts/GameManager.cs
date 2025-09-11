using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("External Game Objects")]
    public TMP_Text enemyCountText;

    public int totalEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdateFirstEnemyCount", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

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
        Debug.Log(totalEnemyCount);

        // Ends the game when there are no enemies
        if (totalEnemyCount <= 0)
        {
            Debug.Log("Game shall end");
            SceneManager.LoadSceneAsync("Win");
        }
    }

    public void updateText(int rawInt)
    {
        // Updates the enemy counter text
        enemyCountText.text = "Enemies Left: " + rawInt.ToString();
    }
    
}
