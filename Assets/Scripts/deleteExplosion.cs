using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class deleteExplosion : MonoBehaviour
{
    public float explosionLifespan = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObj", explosionLifespan);
    }
    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
