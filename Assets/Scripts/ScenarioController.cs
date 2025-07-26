/*
    Author: Kevin Heng
    Date: 01/07/2025
    Description: The ScenarioController class is used to pick a scenario within each scam after selecting it
*/
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    [Header("Scenario selection")]
    /// <summary>
    /// List of Ink JSON files representing the different possible scenarios
    /// </summary>
    public TextAsset[] inkJsonFiles;
    /// <summary>
    /// Corresponding UI canvases for each scenario
    /// </summary>
    public GameObject[] uiCanvas;

    public InkManager[] scenarioManagers;

    public UIManager[] uiManagers;

    public Transform[] messagesContent;

    public GameObject[] scamOrNotEndingUI;

    public Transform whatsupContent;

    public GameObject scenarioCanvas;


    /// <summary>
    /// Function to randomise the scam scenario player will go through
    /// </summary>
    public void RandomiseScenario()
    {

        int index;

        if (GameManager.Instance.isRestart)
        {
            index = GameManager.Instance.prevSceneIndexNum;
            GameManager.Instance.isRestart = false; // reset flag
        }
        else
        {
            int max = inkJsonFiles.Length;
            index = Random.Range(0, max);

            // Avoid repeating the last scenario
            while (index == GameManager.Instance.prevSceneIndexNum)
            {
                index = Random.Range(0, max);
            }

            GameManager.Instance.prevSceneIndexNum = index;
        }

        LoadScenario(index);
    }

    private void LoadScenario(int index)
    {
        TextAsset selectedInk = inkJsonFiles[index];
        scenarioCanvas = uiCanvas[index];
        scamOrNotEndingUI[index].SetActive(true);
        scenarioCanvas.SetActive(true);
        scenarioManagers[index].gameObject.SetActive(true);
        uiManagers[index].gameObject.SetActive(true);
        whatsupContent = messagesContent[index];
        scenarioManagers[index].story = new Story(selectedInk.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomiseScenario();
    }
}
