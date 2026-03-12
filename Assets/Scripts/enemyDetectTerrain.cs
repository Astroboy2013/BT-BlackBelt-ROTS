using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyDetectTerrain : MonoBehaviour
{
    public bool isTerrainDetected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "terrain")
        {
            isTerrainDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "terrain")
        {
            isTerrainDetected = false;
        }
    }
}
