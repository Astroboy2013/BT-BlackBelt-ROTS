using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFire : MonoBehaviour
{
    public GameObject missilePrefab;

    [Header("Dev Options")]
    public bool hasAI;

    public float time;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAI)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if (timer <= 0)
            {
                GameObject newMissile = Instantiate(missilePrefab, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), transform.rotation);
                //newMissile.transform.parent = null;

                timer = time;
            }
        }
    }
}
