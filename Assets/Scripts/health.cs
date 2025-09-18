using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health: MonoBehaviour
{
    public setExplosionAt explosionManager;

    public int maxHealth = 20;

    [Header("Enemy Specific Variables")]
    public bool isEnemy;
    public Color defaultColour;
    public Color damagedColour;
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
    public spawner spawnScript;
=======
>>>>>>> Stashed changes
    private GameManager gm;

>>>>>>> 425417744334a492b9ad6d928ea67ddea6ce1f09
    public int currentHealth = 0;
    private Material currentMaterial;
    public bool isColourChanging;

<<<<<<< Updated upstream
    void Start()
=======
<<<<<<< HEAD
    [Header("Player Specific Variables")]
    public GameObject[] healthBarParts;


    private void Start()
=======
    void Start()
>>>>>>> 425417744334a492b9ad6d928ea67ddea6ce1f09
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            explosionManager = GameObject.Find("GameManager").GetComponent<setExplosionAt>();
=======
<<<<<<< HEAD
            GameObject otherObject = GameObject.FindGameObjectWithTag("GameController");
            spawnScript = otherObject.GetComponent<spawner>();
            if (transform.position.y < 10)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (currentHealth < 1)
        {
            Explode();

=======
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            explosionManager = GameObject.Find("GameManager").GetComponent<setExplosionAt>();
>>>>>>> 425417744334a492b9ad6d928ea67ddea6ce1f09
>>>>>>> Stashed changes
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
                UpdateHealthBar(currentHealth);
            }
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
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
        Destroy(gameObject);

=======
>>>>>>> 425417744334a492b9ad6d928ea67ddea6ce1f09
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
=======
<<<<<<< HEAD
    private void UpdateHealthBar(int health)
    {
        for (int i = health; i < 10; i++)
        {
            healthBarParts[i].SetActive(false);
=======
>>>>>>> Stashed changes
    private void DeleteClone()
    {
        if (isEnemy)
        {
            Destroy(gameObject);
<<<<<<< Updated upstream
=======
>>>>>>> 425417744334a492b9ad6d928ea67ddea6ce1f09
>>>>>>> Stashed changes
        }
    }
}
