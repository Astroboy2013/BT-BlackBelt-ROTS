using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleMap : MonoBehaviour
{
    public GameObject map;
    public void OnToggleChanged(bool isOn)
    {
        if(isOn)
        {
            map.SetActive(true);
        }
        else
        {
            map.SetActive(false);
        }
    }
}
