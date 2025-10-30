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
    public TMP_Text fuelNumber;
    public GameObject engineEffectPart;

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
    public float maxFuel = 100f;
    public float fuel = 100f;
    public bool isMoving = true;
    public float fuelConsumption;

    private float yawBuffer = 0f;
    private float pitchBuffer = 0f;
    private bool isHealing = false;



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
            transform.eulerAngles = new Vector3(pitchBuffer, yawBuffer, 0);
        }

        ///FORCE CODE
        //Boosts Forward
        totalForce = constantForwardForce;

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
        if (collision.gameObject.tag == "carrier")
        {
            transform.SetParent(collision.transform);
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
