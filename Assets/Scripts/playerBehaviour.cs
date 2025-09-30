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
    public float angularAcceleration = 1f;
    public float maxAngularAcceleration = 2f;

    [Header("Other")]
    public bool reverseTiltcontrol = false;
    public float fuel = 100f;
    public float fuelConsumption;

    private float yawBuffer = 0f;
    private float pitchBuffer = 0f;
    private bool isMoving = true;


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
        transform.eulerAngles = new Vector3(pitchBuffer, yawBuffer, 0);

        ///FORCE CODE
        //Boosts Forward
        totalForce = constantForwardForce;

        if (Input.GetKey(KeyCode.Space))
        {
            totalForce += boostForce;
            fuel += fuelConsumption * -1;
        }

        if (isMoving)
        {
            rb.velocity = transform.forward * totalForce;
            fuel += fuelConsumption * -1;
        } 

        if (fuel <= 0)
        {
            isMoving = false;
            rb.useGravity = true;
        }

        if (transform.position.y > 500)
        {
            pitchBuffer = 90f;
        }

        healthNumber.text = healthScript.currentHealth.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "fueling")
        {
            fuel++;
        }
    }
}
