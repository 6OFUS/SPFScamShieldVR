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
using UnityEngine.Video;


public class InkManager : MonoBehaviour 
{
    /// <summary>
    /// The active Ink story instance used for dialogue control.
    /// </summary>
    public Story story;
    /// <summary>
    /// Tags extracted from the current point in the story
    /// </summary>v
    public List<string> storyTags = new List<string>();

    public string knotName;
    public bool stopStory;

    public AnimationClip scamshieldLoading;

    public Fade fadeScript;

    [Header("Messaging")]
    /// <summary>
    /// Time taken between player and sender message
    /// </summary>
    public float messageTime;
    /// <summary>
    /// Reference MessagingSystem class
    /// </summary>
    public MessagingSystem messagingSystem;

    [Header("Player choice")]
    /// <summary>
    /// Prefab for choice button UI 
    /// </summary>
    public GameObject choiceButtonPrefab;

    public GameObject scamshieldChoiceButtonPrefab;

    public GameObject scamshieldButton;

    /// <summary>
    /// Parent transform where choice buttons will be instantiated
    /// </summary>
    public Transform choiceContainer;
    /// <summary>
    /// List of current choices presented to player
    /// </summary>
    public List<ChoiceData> playerChoices = new List<ChoiceData>();



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
    public virtual IEnumerator ContinueStory()
    {
        while (story.canContinue)
        {
            if (stopStory)
            {
                yield break;
            }
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
                Debug.Log(playerChoices[i].choiceName);
                Debug.Log(playerChoices[i].choiceIndex);
            }

            ShuffleChoices(playerChoices);
            DisplayChoices();
        }
        else
        {
            LoadStoryPoint(knotName);
        }
    }

    public virtual void LoadStoryPoint(string knotName)
    {
        story.ChoosePathString(knotName);
        StartCoroutine(ContinueStory());
    }

    /// <summary>
    /// Coroutine to wait for next message to send
    /// </summary>
    /// <returns>Time taken for next message to send</returns>
    public IEnumerator WaitForReply()
    {
        yield return new WaitForSeconds(messageTime);
        yield return StartCoroutine(ContinueStory());
    }

    /// <summary>
    /// Handles the player's choice selection from the current Ink choices
    /// </summary>
    /// <param name="index">Index of the selected choice from choices given</param>
    public void ChooseOption(int index)
    {
        Debug.Log(index);
        string action = playerChoices[index].choiceAction;
        PlayerAction(action, index);
        story.ChooseChoiceIndex(playerChoices[index].choiceIndex);
    }

    /// <summary>
    /// Function to display the UI choice buttons
    /// </summary>
    public virtual void DisplayChoices()
    {
        foreach (var choice in playerChoices)
        {
            if(choice.choiceAction == "sticker")
            {
                continue;
            }

            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceContainer);
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = choice.choiceName;

            // Capture the correct index in a local variable to avoid closure issue
            int capturedIndex = playerChoices.IndexOf(choice);
            buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                ChooseOption(capturedIndex);
                ClearChoices();
                });
        }
    }

    /// <summary>
    /// Remove all choices UI from the choice list
    /// </summary>
    public void ClearChoices()
    {
        foreach (Transform button in choiceContainer)
        {
            if(button.gameObject != scamshieldButton)
            {
                Destroy(button.gameObject);
            }
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
            int randomIndex = Random.Range(i, choices.Count);
            ChoiceData temp = choices[i];
            choices[i] = choices[randomIndex];
            choices[randomIndex] = temp;
        }
        playerChoices = new List<ChoiceData>(choices);
    }

    /// <summary>
    /// To handle the different sender actions based on the current tags in Ink story
    /// </summary>
    /// <param name="dialogue"></param>
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

    /// <summary>
    /// Executes actions triggered by the player's choice selection
    /// </summary>
    /// <param name="action"> Action name </param>
    public virtual void PlayerAction(string action,int index)
    {
        switch (action)
        {
            case "message":
                string selectedText = playerChoices[index].choiceName;
                messagingSystem.PlayerNextMessage(selectedText);
                StartCoroutine(WaitForReply());
                break;
        }
    }

    /// <summary>
    /// Execute actions triggered by the sender based on the tag
    /// </summary>
    /// <param name="action"> Tag for sender actions </param>
    /// <param name="dialogue"> Dialogue content sent by sender </param>
    public virtual void SenderAction(string action, string dialogue)
    {
        switch (action)
        {
            case "message":
                messagingSystem.SenderNextMessage(dialogue);
                break;
        }
    }

    protected virtual IEnumerator ReportToScamShield()
    {
        yield return new WaitForSeconds(scamshieldLoading.length);
    }

    public void Report()
    {
        StartCoroutine(ReportToScamShield());
    }

    protected void ProceedToVideo(VideoClip videoClip)
    {
        choiceContainer.gameObject.SetActive(true);
        GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceContainer);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Proceed to video recap";

        buttonObj.GetComponent<Button>().onClick.AddListener(() =>
        {
            ClearChoices();
            StartCoroutine(fadeScript.FadeTeleport(videoClip));
        });
    }
}
