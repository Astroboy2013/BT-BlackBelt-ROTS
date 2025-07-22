using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class health: MonoBehaviour
{
    public setExplosionAt explosionManager;

    public int maxHealth = 20;
    public bool isEnemy;

    private int currentHealth = 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isEnemy)
        {
            if (collision.gameObject.tag == "player missile")
            {
                currentHealth--;

                if (currentHealth < 1)
                {
                    Explode();

                }
            }
        }
        else
        {
            if (collision.gameObject.tag == "missile")
            {
                currentHealth--;

                if (currentHealth < 1)
                {
                    Explode();
                }
            }
        }    
    }

    private void Explode()
    {
        gameObject.SetActive(false);
        explosionManager.explodeAt(gameObject.transform.position);
    }
}
