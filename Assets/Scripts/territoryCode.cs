using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class territoryCode : MonoBehaviour
{
    [Header("Percetage Calculation")]
    public float playerCapture;
    public float enemyCapture;
    public float totalCapture;
    public float maxCapture;
    public float percentage;

    [Header("UI Editing")]
    public Slider captureSlider;
    public Image fillBar;
    public TMP_Text captureText;
    public GameManager manager;
    public GameObject baseHeal;
    public Renderer baseIcon;

    [Header("Territory State Colours")]
    public Material enemyOccupiedMaterial;
    public Material nonOccupiedMaterial;
    public Material playerOccupiedMaterial;

    private bool isPlayerInTerritory;
    private bool isEnemyInTerritory;

    private float percentageBuffer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalCapture = playerCapture - enemyCapture;
        percentageBuffer = totalCapture / maxCapture;
        percentage = Mathf.Clamp(percentageBuffer * 100, -100, 100);
        captureSlider.value = Mathf.Abs(percentage);
        captureText.text = Mathf.Abs(Mathf.Round(percentage)).ToString() + "%";
        if(percentage > 0)
        {
            captureSlider.direction = Slider.Direction.LeftToRight;
            fillBar.color = Color.blue;
            baseIcon.material = playerOccupiedMaterial;
            baseHeal.SetActive(true);
        }
        else if (percentage < 0)
        {
            captureSlider.direction = Slider.Direction.RightToLeft;
            fillBar.color = Color.red;
            baseIcon.material.color = Color.red;
            baseIcon.material = enemyOccupiedMaterial;
            baseHeal.SetActive(false);
        }
        else
        {
            fillBar.color = Color.black;
            baseIcon.material = nonOccupiedMaterial;
        }
    }
}
