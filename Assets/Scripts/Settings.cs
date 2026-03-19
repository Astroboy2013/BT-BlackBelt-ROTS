using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    public static float fdr;
    public static int maxAm;
    public static int eTu;
    public static int eTh;

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

            shownNumber = fdrDropdown.value * 5f;
            shownNumber = shownNumber * 0.1f;
            shownNumber += 1;
            fdrText.text = "Fuel Conspumtion Rate: " + shownNumber.ToString() + "/s";
            fdr = shownNumber / 50;

            shownNumber = (maxAmSlider.value / 2) * 50;
            maxAmText.text = "Maxmimum Ammo: " + shownNumber.ToString();
            maxAm = (int)shownNumber;

            shownNumber = eTuSlider.value * 15;
            eTuText.text = "Enemy Turn Angle: " + shownNumber.ToString() + "°";
            eTu = (int)shownNumber;

            shownNumber = (eThSlider.value / 2) * 50;
            eThText.text = "Enemy Think Delay: " + shownNumber.ToString() + "s";
            eTh = (int)shownNumber;
        }
    }
}
