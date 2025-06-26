using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject missilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject newMissile = Instantiate(missilePrefab, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), transform.rotation);
            newMissile.transform.parent = null;

        }
    }
}
