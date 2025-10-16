using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapFollowPlayer : MonoBehaviour
{
    public Transform objTransform;
    public float hoverHeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objTransform != null)
        {
            transform.position = new Vector3(objTransform.position.x, hoverHeight, objTransform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
