using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public GameManager manager;
    public GameObject[] territories;

    [Header("Dev Options")]
    public bool hasAI;

    // Start is called before the first frame update
    void Start()
    {
        territories = GameObject.FindGameObjectsWithTag("territory");
        player = GameObject.Find("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                gameObject.transform.LookAt(player.transform);
                if (hasAI)
                {
                    rb.velocity = transform.forward * 30f;
                }
                else
                {
                    rb.isKinematic = true;
                }
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                int curTer = manager.foughtTerritory;
                gameObject.transform.LookAt(territories[curTer].transform);
                rb.velocity = transform.forward * 30f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "territory")
        {
            other.GetComponent<territoryCode>().enemyCapture += 0.1f;
        }
    }
}
