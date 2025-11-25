using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class health: MonoBehaviour
{
    public setExplosionAt explosionManager;

    public float currentHealth = 0;
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

    }

    public void DoDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (!isEnemy)
        {
            healthBar.value = Mathf.Round(currentHealth);
        }

        if (isColourChanging)
        {
            SetDamageColour();
        }

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (isEnemy)
        //{
        //    if (collision.gameObject.tag == "player missile")
        //    {
        //        currentHealth--;
        //        SetDamageColour();
        //    }
        //}
        //else
        //{
        //    if (collision.gameObject.tag == "enemy")
        //    {
        //        currentHealth += -5;
        //    }
        //    if (collision.gameObject.tag == "missile")
        //    {
        //        currentHealth--;
        //    }
        //    UpdateHealthBar(currentHealth, maxHealth);
        //}
        if (collision.gameObject.tag == "ground")
        {
            currentHealth = 0;

            if (!isEnemy)
            {
                healthBar.value = Mathf.Round(currentHealth);
            }
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
            explosionManager.explodeAt(gameObject.transform.position, gameObject.GetComponent<Rigidbody>().velocity, true);
        }
        
        Destroy(gameObject);

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
