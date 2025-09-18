using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(speed, 0, 0) * Time.deltaTime);
    }
}
