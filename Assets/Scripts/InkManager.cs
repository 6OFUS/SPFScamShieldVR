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
    public TextMeshProUGUI dialogueText; 
    public TextMeshProUGUI playerChoice;
    
    public float messageTime;

    protected Story story;
    public SendMessage sendMessage;

    void Start()
    {

    }
    public IEnumerator ContinueStory()
    {
        while (story.canContinue)
        {
            string dialogue = story.Continue();
            //Instantiate text message 
            sendMessage.SenderNextMessage(dialogue);
            dialogueText.text = dialogue;
            yield return new WaitForSeconds(messageTime);
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
            StartCoroutine(WaitForReply());
        }
    }

    IEnumerator WaitForReply()
    {
        yield return new WaitForSeconds(messageTime);
        yield return StartCoroutine(ContinueStory());
    }
}
