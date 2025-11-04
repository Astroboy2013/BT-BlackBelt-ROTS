using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setExplosionAt : MonoBehaviour
{
    public GameObject prefab;
    public GameObject debrisPrefab;
    private GameObject newObject;
    private GameObject debris;
    public void explodeAt(Vector3 position, Vector3 velocity, bool addDestructionParts)
    {
        newObject = Instantiate(prefab, position, Quaternion.identity);
        for (int i = Random.Range(0, 4); i < 4; i++)
        {
            debris = Instantiate(debrisPrefab, position, Quaternion.identity);
            if (velocity != null)
            {
                debris.GetComponent<Rigidbody>().velocity = velocity;
            }
        }
    }
}
