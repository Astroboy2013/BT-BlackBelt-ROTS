using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class lookAtEnemy : MonoBehaviour
{
    private Transform playerTransform;
    public Transform enemyTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTransform != null)
        {
            Vector3 rawDir;
            Vector3 direction;
            rawDir = enemyTransform.position - playerTransform.position;
            direction = rawDir.normalized;
            transform.position = playerTransform.position + direction * 20;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
