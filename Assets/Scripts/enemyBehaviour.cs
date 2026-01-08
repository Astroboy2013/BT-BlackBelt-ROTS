using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public GameManager manager;
    public GameObject[] territories;
    public float speed;

    [Header("Dev Options")]
    public bool hasAI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        territories = manager.territories;
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
                if (hasAI)
                {
                    int curTer = manager.foughtTerritory;

                    Vector3 flyForce = transform.forward;



                    /*if(Vector3.Distance(transform.position, player.transform.position) < 50)
                    {
                        gameObject.transform.LookAt(player.transform);
                    }
                    else
                    {
                        gameObject.transform.LookAt(territories[curTer].transform);
                    }
                    */
                    gameObject.transform.LookAt(territories[curTer].transform);

                    if (Physics.CheckSphere(transform.position, 10f))
                    {
                        rb.velocity += Vector3.up * 5f;
                    }

                    rb.velocity = flyForce * speed;

                }
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "territory")
        {
            other.GetComponent<territoryCode>().enemyCapture += 0.05f;
        }
    }
}
