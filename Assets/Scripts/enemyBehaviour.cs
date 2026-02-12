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
    public enemyDetectTerrain terDetec;

    [Header("Dev Options")]
    public bool hasAI;

    private bool thinking = true;
    private bool closeToPlayer = false;
    private int curTer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        terDetec = GetComponentInChildren<enemyDetectTerrain>();
        territories = manager.territories;

        if (Random.Range(0f, 1f) < 0.5f)
        {
            curTer = Random.Range(0, manager.territories.Length);
        }
        else
        {
            curTer = manager.foughtTerritory;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                Vector3 rawDistance = transform.forward;

                if (hasAI)
                {
                    if (thinking)
                    {
                        if (closeToPlayer)
                        {
                            rawDistance = transform.position - player.transform.position;
                            rb.MoveRotation(Quaternion.LookRotation(rawDistance.normalized));
                        }
                        else
                        {
                            rawDistance = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                            rb.MoveRotation(Quaternion.LookRotation(rawDistance.normalized));
                        }
                        rb.velocity = rawDistance.normalized * 30f;

                        thinking = false;
                        Invoke("SetThinking", 1);

                    }

                    Collider[] hits = Physics.OverlapSphere(transform.position, 20f);
                    
                    for(int i = 1; i < hits.Length; i++)
                    {
                        if(hits[i].gameObject.tag == gameObject.tag)
                        {
                            continue;
                        }

                        if (hits[i].gameObject.tag == "player")
                        {
                            closeToPlayer = true;
                        }
                        else
                        {
                            closeToPlayer = false;
                        }

                        if (hits[i].gameObject.tag == "terrain")
                        {
                            rawDistance = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                        }
                    }

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

                    Vector3 flyForce = transform.forward;

                    if (Vector3.Distance(transform.position, territories[curTer].transform.position) > 200)
                    {
                        gameObject.transform.LookAt(territories[curTer].transform);
                    }
                    else
                    {
                        if (thinking)
                        {
                            Vector3 lookPosition;
                            lookPosition = territories[curTer].transform.position + new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
                            rb.MoveRotation(Quaternion.LookRotation((lookPosition - transform.position).normalized));
                            thinking = false;
                            Invoke("SetThinking", 1);
                        }
                    }

                    if (terDetec.isTerrainDetected)
                    {
                        rb.velocity += Vector3.up * 10f;
                    }

                    rb.velocity = flyForce * speed;

                }
            }

        }
    }

    private void SetThinking()
    {
        thinking = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "territory")
        {
            other.GetComponent<territoryCode>().enemyCapture += 0.01f;
        }
    }
}
