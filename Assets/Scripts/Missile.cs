using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class missile : MonoBehaviour
{
    public Rigidbody rb;
    public float initialForce;
    public float additionalForce;

    Transform followTarget;
    Vector3 flyDirection;
    float totalForce;
    setExplosionAt explosionManager;


    // Start is called before the first frame update
    void Start()
    {
        GameObject otherObject = GameObject.FindGameObjectWithTag("GameController");
        explosionManager = otherObject.GetComponent<setExplosionAt>();

        rb = GetComponent<Rigidbody>();
        totalForce = initialForce + additionalForce;
        flyDirection = transform.forward;

        Invoke("destroyMissile", 10f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        destroyMissile();
    }
   
    void destroyMissile()
    {
        Explode();
        Destroy(gameObject);
    }

    public void setTarget(Transform target)
    {
        if (target != null || target.gameObject.tag != "enemy" || target.gameObject.tag != "dummy")
        {
            flyDirection = (target.position - transform.position).normalized;
            followTarget = target;
        }
    }

    void FixedUpdate()
    {
        if (followTarget != null)
        {
            flyDirection = (followTarget.position - transform.position).normalized;
        }

        if (followTarget = null)
        {
            flyDirection = transform.forward;
        }
        
        rb.AddForce(flyDirection * totalForce);
        transform.LookAt(flyDirection);
    }

    private void Explode()
    {
        gameObject.SetActive(false);
        explosionManager.explodeAt(gameObject.transform.position);
    }

}
