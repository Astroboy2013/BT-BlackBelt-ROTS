using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    public static float fdr = 0.03f;
    public static int maxAm = 50;
    public static int eTu = 15;
    public static int eTh = 3;

    [Header("For Settings Display")]
    public bool paramsBelowEnabled;
    public TMP_Text fdrText;
    public TMP_Text maxAmText;
    public TMP_Text eTuText;
    public TMP_Text eThText;
    public TMP_Dropdown fdrDropdown;
    public Slider maxAmSlider;
    public Slider eTuSlider;
    public Slider eThSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paramsBelowEnabled)
        {
            float shownNumber;

            shownNumber = fdrDropdown.value * 0.5f;
            shownNumber += 1;
            fdrText.text = "Fuel Consumption Rate: " + shownNumber.ToString() + "/s";
            fdr = shownNumber / 50;

            shownNumber = (maxAmSlider.value / 2) * 50;
            maxAmText.text = "Maximum Ammo: " + shownNumber.ToString();
            maxAm = (int)shownNumber;

            shownNumber = eTuSlider.value * 15;
            eTuText.text = "Enemy Turn Angle: " + shownNumber.ToString() + "°";
            eTu = (int)shownNumber;

            shownNumber = (eThSlider.value / 2) * 3;
            eThText.text = "Enemy Think Delay: " + shownNumber.ToString() + "s";
            eTh = (int)shownNumber;
        }
    }
}
