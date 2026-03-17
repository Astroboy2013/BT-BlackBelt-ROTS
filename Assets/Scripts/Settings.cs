using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditFDR(int diff)
    {
        float shownNumber = diff * 5f;
        if (paramsBelowEnabled)
        {
            fdrText.text = shownNumber.ToString();
        }
        fdr = shownNumber;
    }
    public void EditMaxAm(int coEff)
    {
        int shownNumber = (coEff / 2) * 50;
        if (paramsBelowEnabled)
        {
            maxAmText.text = shownNumber.ToString();
        }
        maxAm = shownNumber;
    }
    public void EditETu(int coEff)
    {
        int shownNumber = (coEff / 2) * 50;
        if (paramsBelowEnabled)
        {
            maxAmText.text = shownNumber.ToString();
        }
        eTu = shownNumber;
    }
}
