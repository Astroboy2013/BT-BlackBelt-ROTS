using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;

    public float UpForce = 5000f;
    public float DownForce = 5000f;
    public float rotationalForce = 1f;

    private float rotationBuffer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, planeRotation);
        Debug.Log(transform.rotation.y);
        //Tilt Left
        if (Input.GetKey(KeyCode.A))
        {
            rotationBuffer += rotationalForce;
            transform.Rotate(new Vector3(0f, -rotationBuffer, 0f) * Time.deltaTime);
        }
        //Tilt Right
        if (Input.GetKey(KeyCode.D))
        {
            rotationBuffer += rotationalForce;
            transform.Rotate(new Vector3(0f, rotationBuffer, 0f) * Time.deltaTime);
        }


        //Tilt Up
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.left * 10f * Time.deltaTime);
            rb.AddForce(transform.up * UpForce * Time.deltaTime);
        }
        //Tilt Down
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(-Vector3.left * 10f * Time.deltaTime);
            rb.AddForce(-transform.up * DownForce * Time.deltaTime);
        }
        //Roll Counter clockwise
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime);
        }
        //Roll Clockwise
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 0, -10) * Time.deltaTime);
        }
        //Boosts Forward
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.forward * 50f);
        }
    }
}
