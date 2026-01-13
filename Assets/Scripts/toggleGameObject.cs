using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using UnityEngine;

public class toggleGameObject : MonoBehaviour
{
    public GameObject obj;
    public bool isOn;
    public Image background;
    public Color onColor = Color.green;
    public Color offColor = Color.red;
    public void Toggle()
    {
        isOn = !isOn;
        obj.SetActive(isOn);
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if(isOn)
        {
            background.color = onColor;
        }
        else
        {
            background.color= offColor;
        }
    }
}
