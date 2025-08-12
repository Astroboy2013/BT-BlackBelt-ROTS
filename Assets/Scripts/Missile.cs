using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    public Rigidbody rb;
    public float initialForce;
    public float additionalForce;

    Vector3 difference;
    float totalForce;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyMissile", 10f);

        totalForce = initialForce + additionalForce;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        destroyMissile();
    }
   
    void destroyMissile()
    {
        Destroy(gameObject);
    }

    public void setTarget(Transform target)
    {
        if (target != null)
        {
            difference = target.position - transform.position;
        }
        else
        {
            difference = transform.forward;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(difference.normalized * totalForce);
    }

}
