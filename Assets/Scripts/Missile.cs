using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Missile : MonoBehaviour
{
    public Rigidbody rb;
    public bool isEnemyMissile;
    public float initialForce;
    public float additionalForce;
    public float missileLifespan = 10f;

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

        Invoke("DestroyMissile", missileLifespan);
    }

    public void SetTarget(Transform target)
    {
        if (target != null)
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
        
        rb.velocity = (flyDirection * totalForce);
        transform.LookAt(flyDirection);
    }

    private void Explode()
    {
        gameObject.SetActive(false);
        explosionManager.explodeAt(gameObject.transform.position, Vector3.zero, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "territory")
        {
            if (!isEnemyMissile)
            {
                //If missile collided with enemies or dummies and do damage to them
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "dummy")
                {
                    collision.gameObject.GetComponent<health>().DoDamage(1); //Damage amount
                }

                //If missile collided with anything else then destroy missile
                if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "player missile")
                {
                    DestroyMissile();
                }

            }
            else
            {
                if (collision.gameObject.tag != "enemy" || collision.gameObject.tag == "dummy")
                {
                    DestroyMissile();
                }

                if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "player missile")
                {
                    collision.gameObject.GetComponent<health>().DoDamage(1); //Damage amount
                }
            }
        }
    }
    void DestroyMissile()
    {
        Explode();
        Destroy(gameObject);
    }
}
