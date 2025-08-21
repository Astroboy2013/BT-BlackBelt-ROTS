using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class tutorialSequenceManager : MonoBehaviour
{
    public TMP_Text text;
    public string[] tutorialDialogues;
    [Header("Buttons")]
    public GameObject nextButton;
    public GameObject skipButton;
    public GameObject finishButton;
    private int dialogueNum = 0;

    private void Start()
    {
        updateElements();
    }

    public void nextText()
    {
        dialogueNum++;
        updateElements();
    }

    private void updateElements()
    {
        text.text = tutorialDialogues[dialogueNum];
        if (dialogueNum + 1 == tutorialDialogues.Length)
        {
            nextButton.SetActive(false);
            skipButton.SetActive(false);
            finishButton.SetActive(true);
        }
    }

    public void skipText()
    {
        SceneManager.LoadScene("Tutorial Level", LoadSceneMode.Single);
        SceneManager.sceneLoaded += (scene, mode) => {
            SceneManager.SetActiveScene(scene);
        };
    }
}
