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
using UnityEngine.UI;

public class InkManager : MonoBehaviour 
{
    public Story story;
    public List<string> storyTags = new List<string>();

    public TextAsset[] inkJsonFiles;
    public GameObject[] uiCanvas;

    [Header("Messaging")]
    public float messageTime;
    public MessagingSystem sendMessage;

    [Header("Player choice")]
    public GameObject choiceButtonPrefab;
    public Transform choiceContent;
    public List<ChoiceData> playerChoices = new List<ChoiceData>();
    private int lastSelectedChoiceIndex;


    /// <summary>
    /// Function to randomise the scam scenario player will go through
    /// </summary>
    public void RandomiseScenario()
    {
        int index = Random.Range(0, inkJsonFiles.Length);
        TextAsset selectedInk = inkJsonFiles[index];
        uiCanvas[index].SetActive(true);
        story = new Story(selectedInk.text);
    }

    /// <summary>
    /// Function to start the story via messages
    /// </summary>
    public void StartStory()
    {
        StartCoroutine(ContinueStory());
    }

    /// <summary>
    /// Coroutine to continue Ink story automatically
    /// </summary>
    /// <returns>Time taken for next message to send</returns>
    public IEnumerator ContinueStory()
    {
        while (story.canContinue)
        {
            string dialogue = story.Continue();
            if(!string.IsNullOrWhiteSpace(dialogue))
            {
                HandleSenderActions(dialogue);

                yield return new WaitForSeconds(messageTime);
            }
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

    /// <summary>
    /// Handles the player's choice selection from the current Ink choices
    /// </summary>
    /// <param name="index">Index of the selected choice from choices given</param>
    public void ChooseOption(int index)
    {
        lastSelectedChoiceIndex = index;
        string action = playerChoices[lastSelectedChoiceIndex].choiceAction;
        PlayerAction(action);
        story.ChooseChoiceIndex(lastSelectedChoiceIndex);
        Debug.Log(lastSelectedChoiceIndex);
    }

    /// <summary>
    /// Coroutine to wait for next message to send
    /// </summary>
    /// <returns>Time taken for next message to send</returns>
    IEnumerator WaitForReply()
    {
        yield return new WaitForSeconds(messageTime);
        yield return StartCoroutine(ContinueStory());
    }

    /// <summary>
    /// Function to display the UI choice buttons
    /// </summary>
    void DisplayChoices()
    {
        foreach (var choice in playerChoices)
        {
            Debug.Log($"Choice: {choice.choiceName} (Original index: {choice.choiceIndex})");

            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceContent);
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = choice.choiceName;

            // Capture the correct index in a local variable to avoid closure issue
            int capturedIndex = choice.choiceIndex;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                ChooseOption(capturedIndex);
                ClearChoices();
                });
        }
    }

    /// <summary>
    /// Remove all choices UI from the choice list
    /// </summary>
    void ClearChoices()
    {
        foreach (Transform button in choiceContent)
        {
            Destroy(button.gameObject);
        }
    }

    /// <summary>
    /// Randomise how the options appear each time
    /// Each player will see it differently each time
    /// </summary>
    /// <param name="choices"></param>
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

    public virtual void HandleSenderActions(string dialogue)
    {
        storyTags = story.currentTags;

        foreach (string tag in storyTags)
        {
            if (tag.StartsWith("Sender:"))
            {
                string senderAction = tag.Substring("Sender:".Length);
                SenderAction(senderAction, dialogue);

            }
        }
    }
    

    public virtual void PlayerAction(string action)
    {
        switch (action)
        {
            case "message":
                string selectedText = playerChoices[lastSelectedChoiceIndex].choiceName;
                sendMessage.PlayerNextMessage(selectedText);
                StartCoroutine(WaitForReply());
                break;
            case "submit_scamshield":

                break;
        }
    }
    public void SenderAction(string action, string dialogue)
    {
        switch (action)
        {
            case "message":
                sendMessage.SenderNextMessage(dialogue);
                break;
        }
    }
}
