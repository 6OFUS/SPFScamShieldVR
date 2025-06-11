/*
    Author: Kevin Heng
    Date: 05/06/2025
    Description: The JobScamManager class is used to handle the functions for job scam scenarios
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class JobScamManager : InkManager
{
    public TextAsset[] inkJsonFiles;

    void Start()
    {
        int index = Random.Range(0, inkJsonFiles.Length);
        TextAsset selectedInk = inkJsonFiles[index];
        // Load the story
        story = new Story(selectedInk.text);
        //StartCoroutine(ContinueStory());
    }
}
