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
using System.Linq;

public class InkManager : MonoBehaviour 
{
    protected Story story;

    public TextMeshProUGUI dialogueText; 
    public TextMeshProUGUI playerChoice;

    [Header("Messaging")]
    public float messageTime;
    public MessagingSystem sendMessage;

    [Header("Player choice")]
    public List<ChoiceData> playerChoices = new List<ChoiceData>();

    public IEnumerator ContinueStory()
    {
        while (story.canContinue)
        {
            string dialogue = story.Continue();
            //Instantiate text message 
            dialogueText.text = dialogue;
            sendMessage.SenderNextMessage(dialogue);
            yield return new WaitForSeconds(messageTime);
        }

        if (story.currentChoices.Count > 0)
        {
            playerChoices.Clear();

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                playerChoices.Add(new ChoiceData(i, story.currentChoices[i].text));
            }
            //Shuffle and Instantiate choices
            ShuffleChoices(playerChoices);
            DisplayChoices();
        }
    }

    public void ChooseOption(int index)
    {
        string selectedText = story.currentChoices[index].text;
        Debug.Log("Player selected: " + selectedText);

        playerChoice.text = selectedText;

        story.ChooseChoiceIndex(index);
        // Now continue the story and show the reply
        StartCoroutine(WaitForReply());
    }

    IEnumerator WaitForReply()
    {
        yield return new WaitForSeconds(messageTime);
        yield return StartCoroutine(ContinueStory());
    }

    void DisplayChoices()
    {
        foreach (var choice in playerChoices)
        {
            Debug.Log($"Choice: {choice.choiceName} (Original index: {choice.choiceIndex})");

            /*
            // Example using a button prefab:
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceParent);
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = choice.text;

            // Capture the correct index in a local variable to avoid closure issue
            int capturedIndex = choice.originalIndex;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => ChooseOption(capturedIndex));
            */
        }
    }

    void ShuffleChoices(List<ChoiceData> choices)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            int randomIndex = Random.Range(i, playerChoices.Count);
            ChoiceData temp = choices[i];
            choices[i] = choices[randomIndex];
            choices[randomIndex] = temp;
        }
    }
}
