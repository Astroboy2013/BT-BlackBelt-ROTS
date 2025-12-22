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
            Vector3 newPos;
            rawDir = enemyTransform.position - playerTransform.position;
            direction = rawDir.normalized;
            newPos = playerTransform.position + direction * 20f;
            newPos.y = 1000f;
            transform.position = newPos;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
