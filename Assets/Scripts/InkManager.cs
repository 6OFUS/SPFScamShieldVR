/*
    Author: Kevin Heng
    Date: 05/06/2025
    Description: The InkManager class is used to handle the functions for Ink
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class InkManager
{
    public TextAsset inkJSON;
    public TextMeshProUGUI storyText; // narrative/guide text UI

    protected Story story;

    void Start()
    {

    }

    public void ContinueStory()
    {
        while (story.canContinue)
        {
            string text = story.Continue();
            //Instantiate text message
            //have typing animation and wait that duration
        }

        if (story.currentChoices.Count > 0)
        {
            //Instantiate choices
        }
        else
        {
            //no choices available 
        }
    }

    public void ChooseOption(int index)
    {
        if (story.currentChoices.Count > index)
        {
            string selectedText = story.currentChoices[index].text;
            Debug.Log("Player selected: " + selectedText);

            // Instantiate player selected text


            story.ChooseChoiceIndex(index);

            // Now continue the story and show the reply
            string reply = story.Continue();
            Debug.Log(reply);
            //Instantiate scammer text
        }
    }
}
