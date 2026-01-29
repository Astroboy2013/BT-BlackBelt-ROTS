using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Missile Settings")]
    public Missile missilePrefab;
    public Transform missileSpawnLocation;
    public float time;

    [Header("Ammo Settings")]
    public int maxAmmo;
    public int currentAmmo;
    public TextMeshProUGUI ammoCounter;

    [Header("Other")]
    public float mouseFireThreshold;
    public float mouseY;

    private PlayerBehaviour parentCode;
    private float timer = 1f;

    Missile newMissile;

    // Start is called before the first frame update
    void Start()
    {
        parentCode = GetComponent<PlayerBehaviour>();
        currentAmmo = maxAmmo;
        UpdateAmmoCounter(currentAmmo);

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
            if (mouseY > mouseFireThreshold)
            {
                if (currentAmmo > 0)
                {
                    SphereRayTrace();
                    UpdateAmmoCounter(currentAmmo);
                }
            }
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

    void SphereRayTrace()
    {
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);
        Vector3 origin = Camera.main.ScreenToWorldPoint(viewportCenter);
        Vector3 direction = Camera.main.transform.forward;
        float radius = 50;
        float maxDistance = 80000f;
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
                currentAmmo--;

                //Set homing target
                newMissile.SetTarget(targetTransform);
            }
            else
            {
                newMissile = Instantiate(missilePrefab, missileSpawnPos, transform.rotation);
                currentAmmo--;
            }
        }
        else
        {
            newMissile = Instantiate(missilePrefab, missileSpawnPos, transform.rotation);
            currentAmmo--;
        }

        newMissile.initialForce = GetComponent<Rigidbody>().velocity.magnitude;
    }

    private void UpdateAmmoCounter(int count)
    {
        ammoCounter.text = count.ToString();
    }
   
}
