using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    public TMP_Text enemyCountText;
    public List<Transform> spawnLocations;

    private int enemyCount;
    private Vector3 spawnLocation;
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



        enemyCount = Random.Range(10, 20);
        curEnemyCount = enemyCount;

        for (int enemies = 0; enemies < spawnLocations.Count; enemies++)
        {
            //int randomIndex = Random.Range(0, spawnLocations.Count);
            //spawnLocation = new Vector3(Random.Range(0, 1000), 100, Random.Range(0, 1000));
            Instantiate(enemy, spawnLocations[enemies].position, Quaternion.identity);
        }
    }
    void Update()
    {
        enemyCountText.text = "Enemies Left: " + curEnemyCount.ToString();
    }

}