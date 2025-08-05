using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerBehaviour : MonoBehaviour
{
    [Header("External Scripts")]
    public Rigidbody rb;
    public setExplosionAt explosionManager;
    public TMP_Text healthNumber;
    public health healthScript;

    [Header("Force Strengths")]
    public float yawForce;
    public float pitchForce;
    public float constantForwardForce = 30f;
    public float boostForce = 60f;
    public float totalForce;
    public float angularAcceleration = 0.1f;
    public float maxAngularAcceleration = 2f;

    [Header("Other")]
    public bool reverseTiltcontrol = false;

    private float yawBuffer = 0f;
    private float pitchBuffer = 0f;

    private float yawPositiveTimeBuffer;
    private float yawNegativeTimeBuffer;
    private float pitchPositiveTimeBuffer;
    private float pitchNegativeTimeBuffer;



    // Start is called before the first frame update
    void Start()
    {
        yawPositiveTimeBuffer = 0.01f;
        yawNegativeTimeBuffer = 0.01f;
        pitchPositiveTimeBuffer = 0.01f;
        pitchNegativeTimeBuffer = 0.01f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ///ROTATION CODE
        //Tilt Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            yawBuffer -= yawForce * (yawNegativeTimeBuffer * yawNegativeTimeBuffer);
            yawNegativeTimeBuffer += angularAcceleration;
        }
        else if (Input.GetKeyUp(KeyCode.A)) 
        {
            yawNegativeTimeBuffer = 0f;
        }

        //Tilt Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            yawBuffer += yawForce * (yawPositiveTimeBuffer * yawPositiveTimeBuffer);
            yawPositiveTimeBuffer += angularAcceleration;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            yawPositiveTimeBuffer = 0f;
        }

        //Tilt Up
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (reverseTiltcontrol)
            {
                pitchBuffer -= pitchForce * (pitchPositiveTimeBuffer * pitchPositiveTimeBuffer);
                pitchNegativeTimeBuffer += angularAcceleration;
            }
            else
            {
                pitchBuffer += pitchForce * (pitchNegativeTimeBuffer * pitchNegativeTimeBuffer);
                pitchPositiveTimeBuffer += angularAcceleration;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (reverseTiltcontrol)
            {
                pitchNegativeTimeBuffer = 0f;
            }
            else
            {
                pitchPositiveTimeBuffer = 0f;
            }
        }
        //Tilt Down
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (reverseTiltcontrol)
            {
                pitchBuffer += pitchForce * (pitchNegativeTimeBuffer * pitchNegativeTimeBuffer);
                pitchNegativeTimeBuffer += angularAcceleration;
            }
            else
            {
                pitchBuffer -= pitchForce * (pitchPositiveTimeBuffer * pitchPositiveTimeBuffer);
                pitchPositiveTimeBuffer += angularAcceleration;
            }
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (reverseTiltcontrol)
            {
                pitchPositiveTimeBuffer = 0f;
            }
            else
            {
                pitchNegativeTimeBuffer = 0f;
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
        
        rb.velocity = transform.forward * totalForce;

        if (transform.position.y > 500)
        {
            pitchBuffer = 90f;
        }

        healthNumber.text = healthScript.currentHealth.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "enemy")
        {
            Explode();
        }
    }

    public void Explode()
    {
        gameObject.SetActive(false);
        explosionManager.explodeAt(gameObject.transform.position);
    }
}
