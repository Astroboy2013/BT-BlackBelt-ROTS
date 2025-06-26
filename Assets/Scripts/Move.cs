using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;

    public float yawForce;
    public float pitchForce;

    private float yawBuffer = 0f;
    private float pitchBuffer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(yawBuffer);
        //Tilt Left
        if (Input.GetKey(KeyCode.A))
        {
            yawBuffer -= yawForce;
        }
        //Tilt Right
        if (Input.GetKey(KeyCode.D))
        {
            yawBuffer += yawForce;
        }


        //Tilt Up
        if (Input.GetKey(KeyCode.S))
        {
            pitchBuffer += pitchForce;
        }
        //Tilt Down
        if (Input.GetKey(KeyCode.W))
        {
            pitchBuffer -= pitchForce;
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
            rb.AddForce(transform.forward * 30f);
        }

        pitchBuffer = pitchBuffer * 0.999f;
        transform.eulerAngles = new Vector3(pitchBuffer, yawBuffer, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Explode();
        }
    }

    void Explode()
    {

    }
}
