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
        if (background == null) return;

        if (isOn)
        {
            background.color = onColor;
        }
        else
        {
            background.color = offColor;
        }
    }
}
