using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public GameManager manager;
    public GameObject[] territories;
    public float speed;
    public float percent;
    public enemyDetectTerrain terDetec;

    [Header("Dev Options")]
    public bool hasAI;

    private bool closeToPlayer = false;
    private int curTer;

    // AI timing
    private float aiTimer = 0f;
    private float aiInterval; // AI updates 3 times per second

    private float enemyThinkTime;
    private float enemyTurnAngle;
    private GameObject terDetecObj;

    // LayerMask for OverlapSphere
    private int detectionMask;

    void Start()
    {
        player = GameObject.Find("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        terDetecObj = GameObject.Find("Terrain Detector");
        terDetec = GetComponentInChildren<enemyDetectTerrain>();
        territories = manager.territories;

        enemyThinkTime = Settings.eTh;
        enemyTurnAngle = Settings.eTu;
        aiInterval = enemyThinkTime;

        percent = enemyTurnAngle / 360;

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

        if (scene == 3)
        {
            WanderBehaviour();
        }
        else if (scene == 4)
        {
            terDetecObj.gameObject.SetActive(true);
            TerritorialBehaviour();
        }
    }

    private void TerritorialBehaviour()
    {
        Vector3 flyForce = transform.forward;

        // Pick a territory
        if (Random.Range(0f, 1f) < 0.5f)
            curTer = Random.Range(0, manager.territories.Length);
        else
            curTer = manager.foughtTerritory;

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
            rb.velocity += Vector3.up * 20f;

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

            if (hit.CompareTag("Player"))
                closeToPlayer = true;

            if (hit.CompareTag("ground"))
                foundTerrain = true;
        }

        Vector3 rawDistance = Vector3.zero;
        float rotationRange = percent;

        if (closeToPlayer)
        {
            rawDistance = transform.position - player.transform.position;
            rb.MoveRotation(Quaternion.LookRotation(rawDistance.normalized));
            aiInterval = enemyThinkTime / 6;
        }
        else
        {
            rawDistance = new Vector3(Random.Range(-rotationRange, rotationRange) + rawDistance.x, 0f, Random.Range(-rotationRange, rotationRange) + rawDistance.z);
            rb.MoveRotation(Quaternion.LookRotation(rawDistance));
            aiInterval = enemyThinkTime;
        }

        // Terrain avoidance
        if (foundTerrain)
        {
            rawDistance = new Vector3(Random.Range(-1, 1), 0f, Random.Range(-1f, 1f));
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