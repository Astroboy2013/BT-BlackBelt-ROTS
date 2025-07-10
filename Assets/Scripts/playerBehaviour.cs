using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    [Header("External Scripts")]
    public Rigidbody rb;
    public setExplosionAt explosionManager;

    [Header("Force Strengths")]
    public float yawForce;
    public float pitchForce;
    public float constantForwardForce = 30f;
    public float boostForce = 60f;
    public float totalForce;

    [Header("Other")]
    public bool reverseTiltcontrol = false;

    private float yawBuffer = 0f;
    private float pitchBuffer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ///ROTATION CODE
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
            if (reverseTiltcontrol)
            {
                pitchBuffer -= pitchForce;
            }
            else
            {
                pitchBuffer += pitchForce;
            }
        }
        //Tilt Down
        if (Input.GetKey(KeyCode.W))
        {
            if (reverseTiltcontrol)
            {
                pitchBuffer += pitchForce;
            }
            else
            {
                pitchBuffer -= pitchForce;
            }
        }
        
        pitchBuffer = pitchBuffer * 0.999f;
        transform.eulerAngles = new Vector3(pitchBuffer, yawBuffer, 0);


        ///FORCE CODE
        //Boosts Forward
        totalForce = constantForwardForce;
        
        if (Input.GetKey(KeyCode.Space))
        {
            totalForce += boostForce;
        }
        
        //rb.AddForce(transform.forward * totalForce);
        rb.velocity = transform.forward * totalForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "enemy")
        {
            Explode();
        }
        if (collision.gameObject.tag == "missile") //This is temporary
        {
            Explode();
        }
    }

    void Explode()
    {
        gameObject.SetActive(false);
        explosionManager.explodeAt(gameObject.transform.position);
    }
}
