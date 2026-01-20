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
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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

            if (player != null)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 20)
                {
                    if (timer <= 0)
                    {
                        timer = time;
                        GameObject newMissile = Instantiate(missilePrefab, new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), transform.rotation);
                        //newMissile.transform.parent = null;
                    }
                }
            }
        }
    }
}
