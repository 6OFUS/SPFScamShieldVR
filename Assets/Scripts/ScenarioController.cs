using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

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

    /// <summary>
    /// Function to randomise the scam scenario player will go through
    /// </summary>
    public void RandomiseScenario()
    {
        int index = Random.Range(0, inkJsonFiles.Length);

        TextAsset selectedInk = inkJsonFiles[index];
        uiCanvas[index].SetActive(true);
        scenarioManagers[index].gameObject.SetActive(true);
        uiManagers[index].gameObject.SetActive(true);

        scenarioManagers[index].story = new Story(selectedInk.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomiseScenario();
    }
}
