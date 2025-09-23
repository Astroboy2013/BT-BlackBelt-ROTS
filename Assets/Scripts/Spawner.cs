using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class spawner : MonoBehaviour
{
    public GameObject[] enemy;
    public List<Transform> spawnLocations;

    private int enemyCount;
    private Vector3 spawnLocation;
    private GameObject selectedGameobject;
    public int maxEnemyCount;
    public int minEnemyCount;
    public int offSetRange;

    
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

        Vector3 randomOffset = new Vector3(Random.Range(offSetRange * -1, offSetRange), Random.Range(offSetRange * -1, offSetRange), Random.Range(offSetRange * -1, offSetRange));

        for (int enemies = 0; enemies < enemyCount; enemies++)
        {
            if (enemyCount < spawnLocations.Count)
            {
                GameObject newEnemy = Instantiate(enemy[Random.Range(0, enemy.Length)], spawnLocations[enemies].position, Quaternion.identity);
                newEnemy.SetActive(true);
            }
            else
            {
                GameObject newEnemy = Instantiate(enemy[Random.Range(0, enemy.Length)], spawnLocations[Random.Range(0, spawnLocations.Count)].position + randomOffset, Quaternion.identity);
                newEnemy.SetActive(true);
                Debug.Log("Too Many Enemies. Spawn Method 2 Activating...");
            }
        }

        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (gm != null)
        {
            gm.totalEnemyCount = enemyCount;
        }
    } 
}