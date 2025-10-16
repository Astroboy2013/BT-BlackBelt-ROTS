using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class health: MonoBehaviour
{
    public setExplosionAt explosionManager;

    public int currentHealth = 0;
    public int maxHealth = 10;
    public bool isEnemy;
    public Color defaultColour;
    public Color damagedColour;
    private GameManager gm;
    public Slider healthBar;

    private Material currentMaterial;
    public bool isColourChanging;


    private void Start()
    {
        currentHealth = maxHealth;

        if (isEnemy && isColourChanging)
        {
            currentMaterial = GetComponent<MeshRenderer>().material;
            SetDefaultColour();
        }

        if (isEnemy)
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            explosionManager = GameObject.Find("GameManager").GetComponent<setExplosionAt>();
        }
        else
        {
            healthBar.maxValue = maxHealth;
        }
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Explode();
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
            UpdateHealthBar(currentHealth, maxHealth);
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
            if (gm != null)
            {
                gm.ReduceEnemyTotalCount();

            }
        }
        if (explosionManager != null)
        {
            explosionManager.explodeAt(gameObject.transform.position);
        }
        
        Destroy(gameObject);

    }

    private void SetDefaultColour()
    {
        if (isColourChanging)
        {
            currentMaterial.color = defaultColour;
        }
    }

    private void SetDamageColour()
    {
        if (isColourChanging)
        {
            currentMaterial.color = damagedColour;

            Invoke("SetDefaultColour", 0.5f);
        }
    }
    private void UpdateHealthBar(int health, int maxHealth)
    {
        /*float percent = health / maxHealth;
        int activeBars = (int)percent * healthBarParts.Length;

        Debug.Log(percent);

        foreach (GameObject bar in healthBarParts)
        {
            bar.SetActive(false);
        }

        for (int i = activeBars; i >= 0; i--)
        {
            healthBarParts[i].SetActive(true);
        }
        */

        healthBar.value = health;
        
    }
}
