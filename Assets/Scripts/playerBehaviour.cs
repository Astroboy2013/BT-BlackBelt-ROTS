using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using UnityEditor.Build;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("External Scripts and GameObjects")]
    public Rigidbody rb;
    public setExplosionAt explosionManager;
    public Fire shootScript;
    public health healthScript;
    public AudioSource sound;
    public GameObject engineEffectPart;
    public GameObject[] damagedsmokeParts;
    public Image fuelingButton;
    public TMP_Text fuelingButtonText;
    public CinemachineVirtualCamera cam;
    public AnimationCurve camOffsetCurve;

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
    public bool isFueling;
    public bool isMoving = true;
    public float fuelConsumption;
    public string currentTerritory;
    public GameObject currentFuelingBox;
    public Color onColour;
    public Color offColour;

    private float totalForce;
    private float horizontalInput;
    private float verticalInput;
    private float yawBuffer = 0f;
    private float pitchBuffer = 0f;
    private float rollBuffer = 0f;
    private bool isHealing = false;
    private bool isBoost = false;
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
        /*
        REMAKE SOON
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (fuel > 0)
            {
                if (isMoving == true && isTouchingFuelBox == true)
                {
                    isMoving = false;
                    rb.useGravity = true;
                    pitchBuffer = 0;
                }
                else
                {
                    isMoving = true;
                    rb.useGravity = false;
                }
            }
        }
        */

        if (isMoving)
        {
            //Apply input rotation
            transform.eulerAngles = new Vector3(pitchBuffer, yawBuffer, rollBuffer);
        }

        ///FORCE CODE
        //Boosts Forward
        if (currentTerritory == null)
        {
            totalForce = constantForwardForce;
        }
        else
        {
            totalForce = constantForwardForce * 0.5f;
        }

        //Adjusts Camera
        camOffsetBuffer.y = camOffsetCurve.Evaluate(-pitchBuffer);
        camOffsetBuffer.y = Math.Clamp(camOffsetBuffer.y, camOffsetLowerLimitY, camOffsetUpperLimitY);
        transposer.m_FollowOffset = camOffsetBuffer;

        //Boosts
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentTerritory == null)
            {
                totalForce += boostForce;
                fuel += fuelConsumption * -1;
            }
            else
            {
                totalForce += boostForce * 0.5f;
                fuel += fuelConsumption * -0.5f;
            }
            engineEffectPart.SetActive(true);
            sound.pitch = 1.1f;
        }
        else
        {
            engineEffectPart.SetActive(false);
            sound.pitch = 1;
        }

        if (isMoving)
        {
            rb.velocity = transform.forward * totalForce;
            if (currentTerritory == null)
            {
                fuel += fuelConsumption * -1;
            }
            else
            {
                fuel += fuelConsumption * -0.5f;
            }
        }

        if (isFueling)
        {
            fuel++;
            isHealing = true;
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

        if(healthScript.currentHealth <= healthScript.maxHealth / 2)
        {
            damagedsmokeParts[0].SetActive(true);
        }
        else if (healthScript.currentHealth <= healthScript.maxHealth / 4)
        {
            damagedsmokeParts[1].SetActive(true);
        }
        else
        {
            damagedsmokeParts[0].SetActive(false);
            damagedsmokeParts[1].SetActive(false);
        }

        if (healthScript.currentHealth < healthScript.maxHealth && isHealing)
        {
            healthScript.currentHealth += 0.1f;
        }
    }

    public void ToggleFueling()
    {
        if (currentFuelingBox != null)
        {
            if (isFueling == true)
            {
                isFueling = false;
                fuelingButton.color = offColour;
                fuelingButtonText.text = "Start Fueling";
                pitchBuffer = transform.rotation.x;
                isMoving = true;
                rb.useGravity = false;
                StartCoroutine(FadeIn(0.5f));
            }
            else
            {
                isFueling = true;
                fuelingButton.color = onColour;
                fuelingButtonText.text = "Stop Fueling";
                isMoving = false;
                rb.useGravity = true;
                StartCoroutine(FadeOut(0.5f));
            }
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
                healthScript.DoDamage(2); //Damage to self
                collision.gameObject.GetComponent<health>().DoDamage(5); //Damage to enemy
                break;
            case "dummy":
                healthScript.DoDamage(1); //Damage to self
                collision.gameObject.GetComponent<health>().DoDamage(5); //Damage to dummy
                break;
            case "ground":
                healthScript.DoDamage(999);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "fueling")
        {
            currentFuelingBox = other.gameObject;
        }
        if (other.gameObject.tag == "territory")
        {
            currentTerritory = other.gameObject.name;
            other.GetComponent<territoryCode>().playerCapture++;
        }
        if(other.gameObject.tag == "ground")
        {
            healthScript.DoDamage(999);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (fuel > maxFuel)
        {
            fuel = maxFuel;
        }

        if (other.gameObject.tag == "territory")
        {
            other.GetComponent<territoryCode>().playerCapture += 0.1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fueling")
        {
            isHealing = false;
            currentFuelingBox = null;
        }

        if (other.gameObject.tag == "territory")
        {
            currentTerritory = null;
        }
    }
    public IEnumerator FadeOut(float duration)
    {
        float start = sound.volume;
        float end = 0f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;
            float eased = normalized * normalized * (3f - 2f * normalized); // SmoothStep curve
            float fadeValue = Mathf.Lerp(start, end, eased);
            sound.volume = fadeValue;
            sound.pitch = fadeValue * 5;
            yield return null;
        }

        sound.volume = 0f;
    }
    public IEnumerator FadeIn(float duration)
    {
        float start = sound.volume;
        float end = 0.2f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;
            float eased = normalized * normalized * (3f - 2f * normalized); // SmoothStep curve
            float fadeValue = Mathf.Lerp(start, end, eased);
            sound.volume = fadeValue;
            sound.pitch = fadeValue * 5;
            yield return null;
        }

        sound.volume = 0.2f;
        sound.pitch = 1f;
    }
    public IEnumerator PitchUp(float duration)
    {
        float start = sound.pitch;
        float end = 1.1f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;
            float eased = normalized * normalized * (3f - 2f * normalized); // SmoothStep curve
            float fadeValue = Mathf.Lerp(start, end, eased);
            sound.pitch = fadeValue;
            yield return null;
        }

        sound.pitch = 1.1f;
    }
    public IEnumerator PitchDown(float duration)
    {
        float start = sound.pitch;
        float end = 1f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;
            float eased = normalized * normalized * (3f - 2f * normalized); // SmoothStep curve
            float fadeValue = Mathf.Lerp(start, end, eased);
            sound.pitch = fadeValue;
            yield return null;
        }

        sound.pitch = 1f;
    }
}


