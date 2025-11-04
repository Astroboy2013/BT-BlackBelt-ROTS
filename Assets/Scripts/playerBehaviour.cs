using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("External Scripts")]
    public Rigidbody rb;
    public setExplosionAt explosionManager;
    public TMP_Text healthNumber;
    public health healthScript;
    public TMP_Text fuelNumber;
    public GameObject engineEffectPart;
<<<<<<< HEAD
    public CinemachineVirtualCamera cam;
    public AnimationCurve camOffsetCurve;
=======
>>>>>>> 921bd350012689a13a3c376e9fc559e5ea98077c

    [Header("Force Strengths")]
    public float yawForce;
    public float pitchForce;
    public float pitchRotationLimit = 35f;
    public float rollRotationLimit = 30f;
    public float constantForwardForce = 30f;
    public float boostForce = 60f;
    public float angularAcceleration = 1f;
    public float maxAngularAcceleration = 2f;
    public float camOffsetUpperLimitY = 12f;
    public float camOffsetLowerLimitY = -2.5f;

    [Header("Other")]
    public bool reversePitchControl = false;
    public float maxFuel = 100f;
    public float fuel = 100f;
    public bool isMoving = true;
    public float fuelConsumption;

    private float totalForce;
    private float horizontalInput;
    private float verticalInput;
    private float yawBuffer = 0f;
    private float pitchBuffer = 0f;
    private float rollBuffer = 0f;
    private bool isHealing = false;
    private CinemachineTransposer transposer;
    private Vector3 camOffsetBuffer;


    // Start is called before the first frame update
    void Start()
    {
        transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        camOffsetBuffer = transposer.m_FollowOffset;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ///ROTATION CODE
        
        //Get Inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Tilt Left-Right
        yawBuffer += horizontalInput * yawForce;
        rollBuffer = -horizontalInput * rollRotationLimit;
        rollBuffer = Math.Clamp(rollBuffer, -rollRotationLimit, rollRotationLimit);

        //Tilt Up-Down
        if (reversePitchControl) 
        {
            pitchBuffer += verticalInput * pitchForce;
        }
        else
        {
            pitchBuffer -= verticalInput * pitchForce;
        }

        //Limit pitch rotation
        pitchBuffer = Math.Clamp(pitchBuffer, -pitchRotationLimit, pitchRotationLimit);

        ////Tilt LEFT
        //if (Input.GetKey(KeyCode.A))
        //{
        //    yawBuffer -= yawForce;
        //}

        ////Tilt RIGHT
        //if (Input.GetKey(KeyCode.D))
        //{
        //    yawBuffer += yawForce;
        //}

<<<<<<< HEAD

        ////Tilt DOWN
        //if (Input.GetKey(KeyCode.S))
        //{
        //    if (reverseTiltcontrol)
        //    {
        //        pitchBuffer -= pitchForce;
        //    }
        //    else
        //    {
        //        pitchBuffer += pitchForce;
        //    }
        //}

        ////Tilt UP
        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (reverseTiltcontrol)
        //    {
        //        pitchBuffer += pitchForce;
        //    }
        //    else
        //    {
        //        pitchBuffer -= pitchForce;
        //    }
        //}

=======
>>>>>>> 921bd350012689a13a3c376e9fc559e5ea98077c
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isMoving == true && fuel > 0)
            {
                isMoving = false;
                rb.useGravity = true;
            }
            else
            {
                isMoving = true;
                rb.useGravity = false;
            }
        }

        if (isMoving)
        {
            //Apply input rotation
            transform.eulerAngles = new Vector3(pitchBuffer, yawBuffer, rollBuffer);
        }

        ///FORCE CODE
        //Boosts Forward
        totalForce = constantForwardForce;

<<<<<<< HEAD
        //Adjusts Camera
        camOffsetBuffer.y = camOffsetCurve.Evaluate(-pitchBuffer);
        camOffsetBuffer.y = Math.Clamp(camOffsetBuffer.y, camOffsetLowerLimitY, camOffsetUpperLimitY);
        transposer.m_FollowOffset = camOffsetBuffer;

        //Boosts
=======
>>>>>>> 921bd350012689a13a3c376e9fc559e5ea98077c
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalForce += boostForce;
            fuel += fuelConsumption * -1;
            engineEffectPart.SetActive(true);
        }
        else
        {
            engineEffectPart.SetActive(false);
        }

        if (isMoving)
        {
            rb.velocity = transform.forward * totalForce;
            fuel += fuelConsumption * -1;
            fuelNumber.text = Mathf.Round(fuel).ToString();
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

        healthNumber.text = Mathf.Round(healthScript.currentHealth).ToString();

        if (healthScript.currentHealth < healthScript.maxHealth && isHealing)
        {
            healthScript.currentHealth += 0.1f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "carrier":
                transform.SetParent(collision.transform);
                break;
            case "enemy":
                //Collide with enemies deal damage to them and self
                healthScript.DoDamage(5); //Damage to self
                collision.gameObject.GetComponent<health>().DoDamage(5); //Damage to enemy
                break;
            case "dummy":
                healthScript.DoDamage(1); //Damage to self
                collision.gameObject.GetComponent<health>().DoDamage(5); //Damage to dummy
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "carrier")
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "fueling")
        {
            fuel++;
            isHealing = true;
        }

        if (fuel > maxFuel)
        {
            fuel = maxFuel;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fueling")
        {
            isHealing = false;
        }
    }
}
