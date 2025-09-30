using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class fire : MonoBehaviour
{
    public missile missilePrefab;
    public float time;
    public int maxAmmo;
    public TextMeshProUGUI ammoCounter;

    private playerBehaviour parentCode;
    private float timer = 0f;
    private int currentAmmo;

    missile newMissile;

    // Start is called before the first frame update
    void Start()
    {
        parentCode = GetComponent<playerBehaviour>();
        currentAmmo = maxAmmo;
        UpdateAmmoCounter(currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && timer <= 0)
        {
            if (currentAmmo > 0)
            {
                SphereRayTrace();
                UpdateAmmoCounter(currentAmmo);
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
        newMissile.additionalForce = parentCode.totalForce;

        if (hit.transform != null)
        {
            newMissile.setTarget(hit.transform);
        }

        timer = time;
    }

    void SphereRayTrace()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        float radius = 50f;
        float maxDistance = 50000f;
        RaycastHit hit;

        Transform targetTransform = null;
        Quaternion targetRot = Quaternion.identity;
        Vector3 unLookRotationed = Vector3.zero;

        if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);

            targetTransform = hit.transform;
            unLookRotationed = targetTransform.position - origin; 
            targetRot = Quaternion.LookRotation(unLookRotationed);
        }
        else
        {
            Debug.Log("NONE");
        }

        if (targetTransform != null)
        {
            if (hit.collider.gameObject.tag == "enemy" || hit.collider.gameObject.tag == "dummy")
            {
                newMissile = Instantiate(missilePrefab, origin, targetRot);
                currentAmmo--;
                newMissile.setTarget(targetTransform);
            }
            else
            {
                newMissile = Instantiate(missilePrefab, origin, transform.rotation);
            }
        }
        else
        {
            newMissile = Instantiate(missilePrefab, origin, transform.rotation);
        }
    }

    private void UpdateAmmoCounter(int count)
    {
        ammoCounter.text = count.ToString();
    }
   
}
