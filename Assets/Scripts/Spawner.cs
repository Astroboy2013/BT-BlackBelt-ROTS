using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy;

    private int enemyCount;
    private Vector3 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = Random.Range(10, 20);

        for (int enemies = 0; enemies < enemyCount; enemies++)
        {
            spawnLocation = new Vector3(Random.Range(0, 1000), 100, Random.Range(0, 1000));
            Instantiate(enemy, spawnLocation, Quaternion.identity);
        }
    }
}
