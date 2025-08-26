using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class spawner : MonoBehaviour
{
    public GameObject[] enemy;
    public TMP_Text enemyCountText;
    public List<Transform> spawnLocations;

    private int enemyCount;
    private Vector3 spawnLocation;
    private GameObject selectedGameobject;
    public int maxEnemyCount;
    public int minEnemyCount;
    public int curEnemyCount;

    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] foundSpawnLocations;
        foundSpawnLocations = GameObject.FindGameObjectsWithTag("enemySpawn");

        foreach (GameObject obj in foundSpawnLocations)
        {
            spawnLocations.Add(obj.transform);
        }



        enemyCount = Random.Range(minEnemyCount, maxEnemyCount);
        curEnemyCount = enemyCount;

        for (int enemies = 0; enemies < enemyCount; enemies++)
        {
            selectedGameobject = enemy[Random.Range(0, enemy.Length)];
            Instantiate(selectedGameobject, spawnLocations[enemies].position, Quaternion.identity);
        }
    }
    void Update()
    {
        enemyCountText.text = "Enemies Left: " + curEnemyCount.ToString();

        if (curEnemyCount <= 0)
        {
            Debug.Log("Game shall end");
            SceneManager.LoadSceneAsync("Win");
        }
    }

}