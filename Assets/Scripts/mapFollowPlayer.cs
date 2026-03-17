using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapFollowPlayer : MonoBehaviour
{
    public Transform objTransform;
    public float hoverHeight;
    // Update is called once per frame
    void Update()
    {
        if (objTransform != null)
        {
            transform.position = new Vector3(objTransform.position.x, hoverHeight, objTransform.position.z);
            Quaternion targetRotation = Quaternion.Euler(0, objTransform.eulerAngles.y, 0);
            transform.rotation = targetRotation;

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
