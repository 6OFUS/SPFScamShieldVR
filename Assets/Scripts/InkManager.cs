/*
    Author: Kevin Heng
    Date: 05/06/2025
    Description: The InkManager class is used to handle the functions for Ink
*/
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class InkManager : MonoBehaviour 
{
    [Header("Ink story")]
    /// <summary>
    /// The active Ink story instance used for dialogue control.
    /// </summary>
    public Story story;
    /// <summary>
    /// Tags extracted from the current point in the story
    /// </summary>v
    private List<string> storyTags = new List<string>();

    public bool stopStory;

    public bool isOnHomeScreen;

    [Header("Player controls")]
    public GameObject xrMove;
    public GameObject leftControllerNearFar;
    public GameObject rightControllerNearFar;

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
    protected Dictionary<string, Action<int>> actionHandlers;

    /// <summary>
    /// Prefab for dialogue choice button UI 
    /// </summary>
    public GameObject dialogueChoiceButtonPrefab;
    /// <summary>
    /// Parent transform where choice buttons will be instantiated
    /// </summary>
    public Transform choiceContainer;
    /// <summary>
    /// List of current choices presented to player
    /// </summary>
    public List<ChoiceData> playerChoices = new List<ChoiceData>();

    public GameObject actionChoiceButtonPrefab;

    public GameObject phone;

    [Header("Scamshield")]
    public GameObject scamshieldChoiceButtonPrefab;
    public GameObject scamshieldButton;
    public AnimationClip scamshieldLoading;

    [Header("Educational videos")]
    public VideoClip gameOverVideoClip;
    public VideoClip winVideoClip;
    public VideoClip whatHappenWinVideoClip;
    public VideoClip whatHappenLoseVideoClip;

    [Header("Script references")]
    public Fade fadeScript;
    public RecapVideo recapVideoScript;

    /// <summary>
    /// Function to start the story via messages
    /// </summary>
    public void StartStory(AudioManager audioManager)
    {
        StartCoroutine(ContinueStory(audioManager));
    }

    /// <summary>
    /// Coroutine to continue Ink story automatically
    /// </summary>
    /// <returns>Time taken for next message to send</returns>
    public IEnumerator ContinueStory(AudioManager audioManager)
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

        if (story.currentChoices.Count > 0 && !stopStory)
        {
            playerChoices.Clear();

            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                playerChoices.Add(new ChoiceData(i, story.currentChoices[i].text));
            }

            ShuffleChoices(playerChoices);
            DisplayChoices(audioManager);
        }
    }


    /// <summary>
    /// Coroutine to wait for next message to send
    /// </summary>
    /// <returns>Time taken for next message to send</returns>
    public IEnumerator WaitAndContinueStory(float replyTime, AudioManager audioManager)
    {
        yield return new WaitForSeconds(replyTime);
        yield return StartCoroutine(ContinueStory(audioManager));
    }

    /// <summary>
    /// Handles the player's choice selection from the current Ink choices
    /// </summary>
    /// <param name="index">Index of the selected choice from choices given</param>
    public void ChooseOption(int index, AudioManager audioManager)
    {
        string action = playerChoices[index].choiceAction;
        PlayerAction(action, index, audioManager);
        if (action.Contains("action"))
        {
            audioManager.PlayAudio(audioManager.tapActionButton);
        }
        story.ChooseChoiceIndex(playerChoices[index].choiceIndex);
    }

    /// <summary>
    /// Function to display the UI choice buttons
    /// </summary>
    public virtual void DisplayChoices(AudioManager audioManager)
    {
        for (int i = 0; i < playerChoices.Count; i++)
        {
            var choice = playerChoices[i];

            if (choice.choiceAction.Contains("action"))
            {
                CreateChoiceButton(actionChoiceButtonPrefab, choiceContainer, choice.choiceName, i, choiceContainer, audioManager);
            }
            else if (choice.choiceAction.Contains("message") || choice.choiceAction.Contains("ending"))
            {
                CreateChoiceButton(dialogueChoiceButtonPrefab, choiceContainer, choice.choiceName, i, choiceContainer, audioManager);
            }
        }
    }

    protected virtual void CreateChoiceButton(GameObject prefab, Transform container, string choiceName, int choiceIndex, Transform choiceContainer, AudioManager audioManager)
    {
        GameObject buttonObj = Instantiate(prefab, container);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = choiceName;

        buttonObj.GetComponent<Button>().onClick.AddListener(() => {
            ChooseOption(choiceIndex, audioManager);
            ClearChoices(choiceContainer);
        });
    }

    /// <summary>
    /// Remove all choices UI from the choice list
    /// </summary>
    public void ClearChoices(Transform choiceContainer)
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
            int randomIndex = UnityEngine.Random.Range(i, choices.Count);
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

    public void PlayerAction(string action,int index, AudioManager audioManager)
    {
        if (actionHandlers.TryGetValue(action, out var handler))
        {
            handler(index);
        }
        else if(action == "message")
        {
            string selectedText = playerChoices[index].choiceName;
            messagingSystem.PlayerNextMessage(selectedText);
            StartCoroutine(WaitAndContinueStory(messageTime, audioManager));                  
        }

    }

    protected IEnumerator SpawnActionButton(string buttonText, float delay, AudioManager audioManager, UnityEngine.Events.UnityAction onClickAction)
    {
        yield return new WaitForSeconds(delay);

        GameObject buttonObj = Instantiate(actionChoiceButtonPrefab, choiceContainer);
        TextMeshProUGUI textComponent = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = buttonText;

        buttonObj.GetComponent<Button>().onClick.AddListener(() => {
            onClickAction.Invoke();
            Destroy(buttonObj);
            audioManager.PlayAudio(audioManager.tapActionButton);
        });

        scamshieldButton.transform.SetAsLastSibling();
    }

    //--------------------------------------------------------------------------------------------------
    public IEnumerator SpawnHomeButton(UIManager uIManager, float time, AudioManager audioManager)
    {
        yield return SpawnActionButton("Go to home screen", time, audioManager, () => {
            isOnHomeScreen = true;
            uIManager.DisableAllCanvasChildren();

            uIManager.homeScreen.SetActive(true);

            if (scamshieldButton != null)
            {
                Destroy(scamshieldButton);
            }
            if (uIManager.screenshotTaken)
            {
                StartCoroutine(SpawnOpenScamshieldButton(uIManager, audioManager));
            }
        });
    }

    protected IEnumerator SpawnOpenScamshieldButton(UIManager uIManager, AudioManager audioManager)
    {
        yield return SpawnActionButton("Open Scamshield app", 1f, audioManager, () => {
            uIManager.scamshieldScreen.SetActive(true);
            isOnHomeScreen = false;
            StartCoroutine(SpawnReportButton(audioManager));
        });
    }

    //--------------------------------------------------------------------------------------------------------------

    protected IEnumerator SpawnReportButton(AudioManager audioManager)
    {
        yield return SpawnActionButton("Report", 1f, audioManager, () => {
            Report();
        });
    }

    public IEnumerator ReportToScamShield(UIManager uIManager, AudioClip endingAudioClip, GameObject endingScreen, VideoClip whatHappenVideoClip, VideoClip endingVideoClip, AudioManager audioManager)
    {
        uIManager.scamshieldLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(scamshieldLoading.length);
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        audioManager.PlayAudio(endingAudioClip);
        endingScreen.SetActive(true);
        uIManager.whatHappenButton.onClick.AddListener(() =>
        {
            recapVideoScript.PlayVideo(whatHappenVideoClip);
        });
        ProceedToVideo(endingVideoClip);
    }

    protected virtual void Report()
    {

    }

    public void ProceedToVideo(VideoClip videoClip)
    {
        Destroy(scamshieldButton);
        choiceContainer.gameObject.SetActive(true);
        GameObject buttonObj = Instantiate(dialogueChoiceButtonPrefab, choiceContainer);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Proceed to video recap";

        buttonObj.GetComponent<Button>().onClick.AddListener(() =>
        {
            ClearChoices(choiceContainer);
            StartCoroutine(fadeScript.FadeTeleport(videoClip));
            xrMove.SetActive(false);
            leftControllerNearFar.GetComponent<NearFarInteractor>().enableFarCasting = true;
            rightControllerNearFar.GetComponent<NearFarInteractor>().enableFarCasting = true;
            Destroy(phone);
        });
    }

    private void Start()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        StartStory(audioManager);
    }


}
