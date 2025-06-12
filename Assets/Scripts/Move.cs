using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;
    float planeRotation = 0;

    public float UpForce = 5000f;
    public float DownForce = 5000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        planeRotation = planeRotation * 0.9f;

        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, planeRotation);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * 100 * Time.deltaTime);
            //planeRotation = -50f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
            planeRotation = 50f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.left * 10f * Time.deltaTime);
            rb.AddForce(transform.up * UpForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(-Vector3.left * 10f * Time.deltaTime);
            rb.AddForce(-transform.up * DownForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.forward * 50f);
        }
    }
}
