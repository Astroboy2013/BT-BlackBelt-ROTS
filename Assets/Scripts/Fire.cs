using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using Unity.Mathematics;
using Unity.Burst.CompilerServices;

public class Fire : MonoBehaviour
{
    [Header("Missile Settings")]
    public Missile missilePrefab;
    public Transform missileSpawnLocation;
    public float time;

    [Header("Ammo Settings")]
    public int maxAmmo;
    public int currentAmmo;

    [Header("Other")]
    public float mouseY;

    private PlayerBehaviour parentCode;
    private float timer = 1f;

    Missile newMissile;

    // Start is called before the first frame update
    void Start()
    {
        parentCode = GetComponent<PlayerBehaviour>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        mouseY = Input.mousePosition.y;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && timer <= 0)
        {
            if (mouseY > 130 && mouseY < 860)
            {
                if (currentAmmo > 0)
                {
                    ClickToAim();
                }
            }
        }

        if(currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }


    void ShootStraight()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetDirection;
        Vector3 missileSpawnPos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        if (Physics.Raycast(ray, out hit, 100000f))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Debug.DrawLine(ray.origin, hit.point, Color.red, 2f); // Visible in Scene view

            targetDirection = (hit.point - missileSpawnPos).normalized;
        }
        else
        {
            targetDirection = transform.forward;
        }

        Quaternion targetDirectionRot = Quaternion.LookRotation(targetDirection);

        newMissile = Instantiate(missilePrefab, missileSpawnPos, targetDirectionRot);
        //newMissile.additionalForce = parentCode;

        if (hit.transform != null)
        {
            newMissile.SetTarget(hit.transform);
        }

        timer = time;
    }

    void ClickToAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float radius = 10f;
        float maxDistance = 500;

        RaycastHit[] hits = Physics.SphereCastAll(ray, radius, maxDistance);

        float largestDist = maxDistance;
        GameObject chosenHit = null;

        foreach (var hit in hits)
        {
            // Get the closest object to target
            for (int i = 0; i < hits.Length; i++)
            {
                Vector3 distanceBetweenHit = hit.collider.gameObject.transform.position - Input.mousePosition;
                if (distanceBetweenHit.magnitude < largestDist)
                {
                    largestDist = distanceBetweenHit.magnitude;
                    chosenHit = hit.collider.gameObject;
                }
            }
        }
        if (chosenHit != null)
        {
            if (chosenHit.tag == "enemy" || chosenHit.tag == "dummy")
            {
                Quaternion targetRot = Quaternion.identity;
                Vector3 unLookRotationed = Vector3.zero;

                if (missileSpawnLocation != null)
                {
                    unLookRotationed = chosenHit.transform.position - missileSpawnLocation.position;
                }
                else
                {
                    unLookRotationed = chosenHit.transform.position - transform.position;
                }
                targetRot = Quaternion.LookRotation(unLookRotationed);

                //Set homing target
                newMissile = Instantiate(missilePrefab, transform.position, targetRot);
                newMissile.SetTarget(chosenHit.transform);

            }
        else
        {

        }
    }

    void SphereRayTrace()
    {
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);
        Vector3 origin = Camera.main.ScreenToWorldPoint(viewportCenter);
        Vector3 direction = Camera.main.transform.forward;
        float radius = 200;
        float maxDistance = 999999f;
        RaycastHit hit;

        Transform targetTransform = null;
        Quaternion targetRot = Quaternion.identity;
        Vector3 unLookRotationed = Vector3.zero;

        if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance))
        {
            if (hit.collider.gameObject.tag == "enemy" || hit.collider.gameObject.tag == "dummy")
            {
                targetTransform = hit.transform;

                if (missileSpawnLocation != null)
                {
                    unLookRotationed = targetTransform.position - missileSpawnLocation.position; 
                }
                else
                {
                    unLookRotationed = targetTransform.position - transform.position;
                }
                targetRot = Quaternion.LookRotation(unLookRotationed);
            }
        }

        //For debugging
        //Debug.DrawLine(origin, hit.point, Color.red, 3f);

        Vector3 missileSpawnPos = transform.position;
        if (missileSpawnLocation != null)
        {
            missileSpawnPos = missileSpawnLocation.position;
        }

        if (targetTransform != null)
        {
            if (hit.collider.gameObject.tag == "enemy" || hit.collider.gameObject.tag == "dummy")
            {
                newMissile = Instantiate(missilePrefab, missileSpawnPos, targetRot);

                //Set homing target
                newMissile.SetTarget(targetTransform);
            }
            else
            {
                newMissile = Instantiate(missilePrefab, missileSpawnPos, transform.rotation);
            }
        }
        else
        {
            newMissile = Instantiate(missilePrefab, missileSpawnPos, transform.rotation);
        }

        newMissile.initialForce = GetComponent<Rigidbody>().velocity.magnitude;
        currentAmmo--;
    }
}
