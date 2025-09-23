using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setExplosionAt : MonoBehaviour
{
    public GameObject prefab;
    private GameObject newObject;
    public void explodeAt(Vector3 position)
    {
        newObject = Instantiate(prefab, position, Quaternion.identity);
        Invoke("DestroyObj", 3f);
    }
    private void DestroyObj()
    {
        Destroy(newObject);
    }
}
