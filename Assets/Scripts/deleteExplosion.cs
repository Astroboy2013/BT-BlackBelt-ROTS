using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class deleteExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObj", 3f);
    }
    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
