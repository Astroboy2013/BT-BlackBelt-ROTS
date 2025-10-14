using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float hoverHeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, hoverHeight, playerTransform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
