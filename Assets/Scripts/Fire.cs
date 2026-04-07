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

        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }

    bool AllowTags(string compTag)
    {
        bool canPass = true;

        string[] ignoreTags =
        {
            "Player",
            "player missile",
            "ground"
        };

        foreach (string tag in ignoreTags)
        {
            if (tag == compTag)
            {
                canPass = false;
            }
            else
            {
                canPass = true;
            }
        }
        return canPass;
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
        float radius = 20f;
        float maxDistance = 500;

        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = Camera.main.transform.forward;

        Ray ray = new Ray(origin, direction);


        RaycastHit[] hits = Physics.SphereCastAll(ray, radius, maxDistance);

        float largestDist = maxDistance;
        GameObject chosenHit = null;

        foreach (var hit in hits)
        {
            // Get the closest object to target
            for (int i = 0; i < hits.Length; i++)
            {
                GameObject hitObj = hit.collider.gameObject;
                Debug.Log(AllowTags(hitObj.tag));
                if(!AllowTags(hitObj.tag))
                {
                    continue;
                }
                
                
                Vector3 distanceBetweenHit = hit.collider.gameObject.transform.position - origin;
                if (distanceBetweenHit.magnitude < largestDist)
                {
                    largestDist = distanceBetweenHit.magnitude;
                    chosenHit = hitObj;
                    Debug.Log(hitObj.name);
                }
            }
        }
        if (chosenHit != null)
        {
            Quaternion targetRot = Quaternion.identity;
            Vector3 unLookRotationed = Vector3.zero;
            if (chosenHit.tag == "enemy" || chosenHit.tag == "dummy")
            {
                //Set homing target
                newMissile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
                newMissile.SetTarget(chosenHit.transform);

            }
            else
            {
                newMissile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
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
}
