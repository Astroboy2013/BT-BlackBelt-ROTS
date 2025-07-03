using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyMissile", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * 30f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
   
    void destroyMissile()
    {
        Destroy(gameObject);
    }
}
