using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteClone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyClone", 10);
    }
    private void DestroyClone()
    {
        Destroy(gameObject);
    }
}
