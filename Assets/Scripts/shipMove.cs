using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMove : MonoBehaviour
{
    public float shipSpeed;
    public GameObject[] shipPositions;
    public int currentPosId;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rawDistance;
        rawDistance = transform.position - shipPositions[currentPosId].transform.position;
        rb.velocity = rawDistance.normalized * shipSpeed;
        if(Vector3.Distance(shipPositions[currentPosId].transform.position, transform.position) < 5)
        {
            currentPosId++;
        }
    }
}
