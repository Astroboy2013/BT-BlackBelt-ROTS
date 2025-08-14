using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class health: MonoBehaviour
{
    public setExplosionAt explosionManager;

    public int maxHealth = 20;
    public bool isEnemy;
    public Color defaultColour;
    public Color damagedColour;
    public spawner spawnScript;

    public int currentHealth = 0;
    private Material currentMaterial;


    private void Start()
    {
        currentHealth = maxHealth;

        if (isEnemy && gameObject.tag == "dummy")
        {
            currentMaterial = GetComponent<MeshRenderer>().material;
            SetDefaultColour();
        }

        if (isEnemy)
        {
            GameObject otherObject = GameObject.FindGameObjectWithTag("GameController");
            spawnScript = otherObject.GetComponent<spawner>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isEnemy)
        {
            if (collision.gameObject.tag == "player missile")
            {
                currentHealth--;
                SetDamageColour();
            }
        }
        else
        {
            if (collision.gameObject.tag == "missile" || collision.gameObject.tag == "enemy")
            {
                currentHealth += -5;
            }
        }
        if (currentHealth < 1)
        {
            Explode();

        }

        if (collision.gameObject.tag == "ground")
        {
            currentHealth = 0;
        }
    }

    private void Explode()
    {
        if (isEnemy)
        {
            spawnScript.curEnemyCount--;
        }
        gameObject.SetActive(false);
        explosionManager.explodeAt(gameObject.transform.position);
    }

    private void SetDefaultColour()
    {
        currentMaterial.color = defaultColour;
    }

    private void SetDamageColour()
    {
        currentMaterial.color = damagedColour;

        Invoke("SetDefaultColour", 0.5f);
    }

}
