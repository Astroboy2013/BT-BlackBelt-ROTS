using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;

    [Header("Dev Options")]
    public bool hasAI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (hasAI)
            {
                gameObject.transform.LookAt(player.transform);
                rb.velocity = transform.forward * 30f;
            } else {
                rb.isKinematic = true;
            }
        }
    }
}
