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

public class InkManager : MonoBehaviour 
{
    public TextAsset inkJSON;
    public TextMeshProUGUI dialogue; // narrative/guide text UI
    public TextMeshProUGUI playerChoice;

    protected Story story;

    void Start()
    {

    }
    public IEnumerator ContinueStory()
    {
        while (story.canContinue)
        {
            string text = story.Continue();
            Debug.Log(text);
            //Instantiate text message
            //have typing animation and wait that duration
            dialogue.text = text;
            yield return new WaitForSeconds(5f);
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Debug.Log($"Choice {i}: '{story.currentChoices[i].text}'");
                //Instantiate choices
            }
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


            playerChoice.text = selectedText;

            story.ChooseChoiceIndex(index);

            // Now continue the story and show the reply
            string reply = story.Continue().Trim();
            Debug.Log(reply);
            dialogue.text = reply;
            NextLine();
        }
    }

    public void NextLine()
    {
        if (story.canContinue)
        {
            //Debug.Log(story.Continue());
            string dialogueTest = story.Continue();
            dialogue.text = dialogueTest;
            Debug.Log(dialogueTest);
        }
        else
        {
            if (story.currentChoices.Count > 0)
            {
                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    Debug.Log($"Choice {i}: '{story.currentChoices[i].text}'");
                }
            }
        }
    }
}
