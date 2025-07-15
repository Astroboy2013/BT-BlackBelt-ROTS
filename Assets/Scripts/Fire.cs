using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Missile missilePrefab;
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

            if (Physics.Raycast(ray, out hit, 100000f))
            {
                Debug.Log("Hit: " + hit.collider.name);
                Debug.DrawLine(ray.origin, hit.point, Color.red); // Visible in Scene view
            }


            Vector3 missileSpawnPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
            Vector3 targetDirection = (hit.point - missileSpawnPos).normalized;


            Quaternion targetDirectionRot = Quaternion.LookRotation(targetDirection);


            Missile newMissile = Instantiate(missilePrefab, missileSpawnPos, targetDirectionRot); ; 
            newMissile.additionalForce = parentCode.totalForce;

            timer = time;
        }
    }

    //https://www.youtube.com/watch?v=adgeiUNlajY&embeds_referring_euri=https%3A%2F%2Fwww.bing.com%2F&embeds_referring_origin=https%3A%2F%2Fwww.bing.com&source_ve_path=MTM5MTE3LDEzOTExNywxMzkxMTcsMTM5MTE3LDEzOTExNywyODY2Ng
}
