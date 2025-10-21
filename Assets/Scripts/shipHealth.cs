using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject fuelingBox;
    public GameObject fireEffect;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Vector3 offSet = new Vector3(Random.Range(-0.015f, 0.015f), 15, Random.Range(-0.015f, 0.015f));

            rb.useGravity = true;
            Destroy(fuelingBox);
            fireEffect.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "missile")
        {
            health--;
        }
    }
}
