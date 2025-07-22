using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public missile missilePrefab;
    public float time;
    
    private playerBehaviour parentCode;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        parentCode = GetComponent<playerBehaviour>();   
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
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetDirection;
            Vector3 missileSpawnPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);

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


            missile newMissile = Instantiate(missilePrefab, missileSpawnPos, targetDirectionRot); ; 
            newMissile.additionalForce = parentCode.totalForce;

            timer = time;
        }
    }

   
}
