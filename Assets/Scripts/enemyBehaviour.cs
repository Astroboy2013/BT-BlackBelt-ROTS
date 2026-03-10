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
    public float speed;
    public enemyDetectTerrain terDetec;

    [Header("Dev Options")]
    public bool hasAI;

    private bool closeToPlayer = false;
    private int curTer;

    // AI timing
    private float aiTimer = 0f;
    private float aiInterval = 0.3f; // AI updates 3 times per second

    // LayerMask for OverlapSphere
    private int detectionMask;

    void Start()
    {
        player = GameObject.Find("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        terDetec = GetComponentInChildren<enemyDetectTerrain>();
        territories = manager.territories;

        // Pick a territory
        if (Random.Range(0f, 1f) < 0.5f)
            curTer = Random.Range(0, manager.territories.Length);
        else
            curTer = manager.foughtTerritory;

        // Only detect what we need
        detectionMask = LayerMask.GetMask("Player", "Terrain", "Enemy");
    }

    void Update()
    {
        if (player == null)
            return;

        int scene = SceneManager.GetActiveScene().buildIndex;

        // AI runs on a timer instead of every frame
        aiTimer -= Time.deltaTime;
        if (aiTimer <= 0f)
        {
            aiTimer = aiInterval;
            RunAI(scene);
        }
    }

    private void RunAI(int scene)
    {
        if (!hasAI)
        {
            rb.isKinematic = true;
            return;
        }

        if (scene == 2)
        {
            WanderBehaviour();
        }
        else if (scene == 3)
        {
            TerritorialBehaviour();
        }
    }

    private void TerritorialBehaviour()
    {
        Vector3 flyForce = transform.forward;

        // Move toward territory unless close
        if (Vector3.Distance(transform.position, territories[curTer].transform.position) > 200)
        {
            transform.LookAt(territories[curTer].transform);
        }
        else
        {
            WanderBehaviour();
        }

        // Terrain avoidance
        if (terDetec.isTerrainDetected)
            rb.velocity += Vector3.up * 10f;

        rb.velocity = flyForce * speed;
    }

    private void WanderBehaviour()
    {
        bool foundTerrain = false;
        closeToPlayer = false;

        // MUCH cheaper OverlapSphere
        Collider[] hits = Physics.OverlapSphere(transform.position, 20f, detectionMask);

        foreach (var hit in hits)
        {
            if (hit == null) continue;

            if (hit.CompareTag(gameObject.tag))
                continue;

            if (hit.CompareTag("player"))
                closeToPlayer = true;

            if (hit.CompareTag("terrain"))
                foundTerrain = true;
        }

        Vector3 rawDistance;

        if (closeToPlayer)
        {
            rawDistance = transform.position - player.transform.position;
            rb.MoveRotation(Quaternion.LookRotation(rawDistance.normalized));
            aiInterval = 0.5f; // Think faster near player
        }
        else
        {
            rawDistance = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            rb.MoveRotation(Quaternion.LookRotation(rawDistance.normalized));
            aiInterval = 3f; // Think slower when wandering
        }

        // Terrain avoidance
        if (foundTerrain)
        {
            rawDistance = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        }

        rb.velocity = rawDistance.normalized * 30f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("territory"))
        {
            other.GetComponent<territoryCode>().enemyCapture += 0.01f;
        }
    }
}