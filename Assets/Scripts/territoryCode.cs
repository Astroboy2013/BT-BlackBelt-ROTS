using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class territoryCode : MonoBehaviour
{
    public float playerCapture;
    public float enemyCapture;
    public float totalCapture;
    public float maxCapture;
    public float percentage;

    private float percentageBuffer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalCapture = playerCapture - enemyCapture;
        percentageBuffer = totalCapture / maxCapture;
        percentage = percentageBuffer * 100;
        Debug.Log(percentage);
    }
}
