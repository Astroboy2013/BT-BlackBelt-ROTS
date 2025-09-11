using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health: MonoBehaviour
{
    public setExplosionAt explosionManager;

    public int maxHealth = 20;
    public bool isEnemy;
    public Color defaultColour;
    public Color damagedColour;
    private GameManager gm;

    public int currentHealth = 0;
    private Material currentMaterial;
    public bool isColourChanging;

    void Start()
    {
        Debug.Log("I'm Working!");
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
        gameObject.SetActive(false);
        if (isEnemy)
        {
            if (gm != null)
            {
                gm.ReduceEnemyTotalCount();
            }
        }
        else
        {
            SceneManager.LoadScene("Tutorial Level");
        }
        if (explosionManager != null)
        {
            explosionManager.explodeAt(gameObject.transform.position);
        }
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

    private void DeleteClone()
    {
        if (isEnemy)
        {
            Destroy(gameObject);
        }
    }
}
