using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setExplosionAt : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject debrisPrefab;
    private GameObject newObject;
    
    public void explodeAt(Vector3 position, Vector3 velocity, bool canSpawnParticles)
    {
        newObject = Instantiate(explosionPrefab, position, Quaternion.identity);
        if (canSpawnParticles)
        {
            Vector3 offset = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            GameObject debrisObj;
            for (int i = 0; i <= Random.Range(5, 10); i++)
            {
                offset = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                debrisObj = Instantiate(debrisPrefab, position, Quaternion.identity);
                debrisObj.GetComponent<Rigidbody>().velocity = velocity + offset;
            }
        }
    }
}
