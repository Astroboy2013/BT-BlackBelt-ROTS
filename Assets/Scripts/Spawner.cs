using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    private Vector3 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        for (int enemies = 0; enemies < Random.Range(10, 20); enemies++)
        {
            spawnLocation = new Vector3(Random.Range(0, 1000), 100, Random.Range(0, 1000));
            Instantiate(enemy, spawnLocation, Quaternion.identity);
        }
    }
}
