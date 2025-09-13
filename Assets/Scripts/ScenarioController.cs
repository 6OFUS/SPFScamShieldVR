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
    /// <summary>
    /// List of Ink JSON files representing the different possible scenarios
    /// </summary>
    [Header("Scenario selection")]
    public TextAsset[] inkJsonFiles;
    /// <summary>
    /// Array for corresponding UI canvases for each scenario
    /// </summary>
    public GameObject[] uiCanvas;
    /// <summary>
    /// Array for corresponding ink managers for each scenario
    /// </summary>
    public InkManager[] inkManagers;
    /// <summary>
    /// Array for corresponding ui managers for each scenario
    /// </summary>
    public UIManager[] uiManagers;
    /// <summary>
    /// Array for corresponding messages content for each scenario
    /// </summary>
    public Transform[] messagesContent;
    /// <summary>
    /// Array for corresponding ending ui for each scenario
    /// </summary>
    public GameObject[] scamOrNotEndingUI;
    /// <summary>
    /// Transform of chosen message content
    /// </summary>
    public Transform messageContentParent;
    /// <summary>
    /// Chosen scenario canvas
    /// </summary>
    public GameObject scenarioCanvas;


    /// <summary>
    /// Function to randomise the scam scenario player will go through
    /// </summary>
    public void RandomiseScenario()
    {

        int index;

        if (GameManager.Instance.isRestart)
        {
            index = GameManager.Instance.prevScenario;
            GameManager.Instance.isRestart = false; // reset flag
        }
        else
        {
            int max = inkJsonFiles.Length;
            index = Random.Range(0, max);

            // Avoid repeating the last scenario
            while (index == GameManager.Instance.prevScenario)
            {
                index = Random.Range(0, max);
            }

            GameManager.Instance.prevScenario = index;
        }

        LoadScenario(index);
    }

    /// <summary>
    /// Load scenario with corresponding UI
    /// </summary>
    /// <param name="index">Scenario array index number</param>
    private void LoadScenario(int index)
    {
        TextAsset selectedInk = inkJsonFiles[index];
        scenarioCanvas = uiCanvas[index];
        scamOrNotEndingUI[index].SetActive(true);
        scenarioCanvas.SetActive(true);
        inkManagers[index].gameObject.SetActive(true);
        uiManagers[index].gameObject.SetActive(true);
        messageContentParent = messagesContent[index];
        inkManagers[index].story = new Story(selectedInk.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomiseScenario();
    }
}
