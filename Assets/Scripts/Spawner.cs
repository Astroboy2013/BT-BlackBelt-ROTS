using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class spawner : MonoBehaviour
{
    public bool toggleConstantSpawn;

    public GameObject[] enemy;
    public GameObject dirIndicatorPrefab;
    public GameObject indicatorPrefab;
    public List<Transform> spawnLocations;

    private int enemyCount;
    private GameObject selectedGameobject;
    public int maxEnemyCount;
    public int minEnemyCount;

    private GameObject[] foundSpawnLocations;
    private int debugCounter = 0;

// Start is called before the first frame update
    void Start()
    {
            foundSpawnLocations = GameObject.FindGameObjectsWithTag("enemySpawn");

            foreach (GameObject obj in foundSpawnLocations)
            {
                spawnLocations.Add(obj.transform);
            }

            enemyCount = Random.Range(minEnemyCount, maxEnemyCount);

            Vector3 randomOffset = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(20, 50));

            for (int enemies = 0; enemies < enemyCount; enemies++)
            {
                GameObject enemyClone;
                GameObject indicator;
                GameObject dirInPre;
                if (enemyCount < spawnLocations.Count)
                {
                    selectedGameobject = enemy[Random.Range(0, enemy.Length)];
                    enemyClone = Instantiate(selectedGameobject, spawnLocations[enemies].position, Quaternion.identity);
                    indicator = Instantiate(indicatorPrefab);
                    indicator.GetComponent<mapFollowPlayer>().objTransform = enemyClone.transform;
                    dirInPre = Instantiate(selectedGameobject, spawnLocations[enemies].position, Quaternion.identity);
                    dirInPre.GetComponent<lookAtEnemy>().enemyTransform = enemyClone.transform;
                    debugCounter++;
                }
                else
                {
                    selectedGameobject = enemy[Random.Range(0, enemy.Length)];
                    enemyClone = Instantiate(selectedGameobject, spawnLocations[Random.Range(0, spawnLocations.Count)].position + randomOffset, Quaternion.identity);
                    indicator = Instantiate(indicatorPrefab);
                    indicator.GetComponent<mapFollowPlayer>().objTransform = enemyClone.transform;
                    dirInPre = Instantiate(selectedGameobject, spawnLocations[enemies].position, Quaternion.identity);
                    dirInPre.GetComponent<lookAtEnemy>().enemyTransform = enemyClone.transform;
                debugCounter++;
                }
            

                if (enemyClone.transform.position.y < 10)
                {
                    Vector3 newPosition = enemyClone.transform.position;
                    newPosition.y = Random.Range(20, 50);
                    enemyClone.transform.position = newPosition;
                }
            }

            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (gm != null)
            {
                gm.totalEnemyCount = enemyCount;
            }

            //Debug.Log(debugCounter.ToString() + " " + gm.totalEnemyCount.ToString());

    }
    void Update()
    {

    }

}