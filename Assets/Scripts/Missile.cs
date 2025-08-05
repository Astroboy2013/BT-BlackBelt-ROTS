using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    public Rigidbody rb;
    public float initialForce;

    public float additionalForce;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyMissile", 10f);
        
        float totalForce = initialForce + additionalForce;
        rb.velocity = transform.forward * totalForce;
    }

    // Update is called once per frame
    void Update()
    {
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
