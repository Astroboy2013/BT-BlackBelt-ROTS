using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyBehaviour : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float speed;

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
            gameObject.transform.LookAt(player.transform);
            if (hasAI)
            {
                rb.velocity = transform.forward * speed;
            } else {
                rb.isKinematic = true;
            }
        }
    }
}
