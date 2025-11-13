using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class toggleGameObject : MonoBehaviour
{
    public GameObject obj;
    public void OnToggleChanged(bool isOn)
    {
        obj.SetActive(isOn);
    }
}
