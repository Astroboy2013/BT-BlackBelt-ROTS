using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setExplosionAt : MonoBehaviour
{
    public GameObject prefab;
    public void explodeAt(Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
