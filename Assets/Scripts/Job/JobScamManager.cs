/*
    Author: Kevin Heng
    Date: 05/06/2025
    Description: The JobScamManager class is used to handle all the functions related to the job scam scenario
*/
using Ink.Parsed;
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class JobScamManager : InkManager
{
    public JobScamUIManager uIManager;

    [Header("Audio")]
    public AudioClip cryingClip;
    public AudioClip checkOutClip;
    public AudioClip errorClip;

    [Header("Account creation")]
    public int inputCount;
    public float loadingTime;

    [Header("Website task")]
    [SerializeField] private bool firstTaskCompleted;
    [SerializeField] private int numItemsAdded;

    public override void DisplayChoices()
    {
        base.DisplayChoices();
        if (scamshieldButton == null && !isOnHomeScreen)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot();
                ClearChoices();
                Destroy(scamshieldButton);
                StartCoroutine(SpawnHomeButton(uIManager,1));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }
    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "action_tap_notification":
                uIManager.whatsupScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(0));
                break;
            case "action_open_amail":
                uIManager.amailScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_open_jason_email":
                uIManager.jasonEmailScreen.SetActive(true);
                StartCoroutine(SpawnHomeButton(uIManager, 5));
                break;
            case "message_register_account":
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                uIManager.websiteHomeScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;

            case "action_create_account":
                uIManager.websiteCreateAccountScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_enter_details":
                for (int i = 0; i < uIManager.detailsInputText.Length; i++)
                {
                    uIManager.detailsInputText[i].color = Color.black;
                    uIManager.detailsInputText[i].text = uIManager.detailsTextContent[i];
                }
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_submit_and_create":
                StartCoroutine(CreatingAccount());
                break;
            case "message_complete_task":
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                uIManager.whatsupScreen.SetActive(false);
                uIManager.websiteHomeLoggedInScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_select_silver_tier":
                uIManager.websiteSelectTaskScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_select_task_1":
                StartCoroutine(HandleFirstTaskGroupSelection());
                break;
            case "action_add_items":
                uIManager.itemNumThree.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_check_out":
                StartCoroutine(HandleCheckOut());
                break;
            case "message_complete_task_2":
                uIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                uIManager.whatsupScreen.SetActive(false);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_select_task_2":
                StartCoroutine(HandleSecondTaskGroupSelection());
                break;
            case "message_withdraw":
                uIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_withdraw":
                StartCoroutine(Withdraw());
                break;
            case "error_message":
                messagingSystem.PlayerNextMessage("<color=grey>You can no longer send messages to this contact.</color>");
                StartCoroutine(WaitAndContinueStory(messageTime));
                break;
            case "lose_ending":
                StartCoroutine(HandleLoseEnding());
                break;
            case "ignore_ending":
                StartCoroutine(HandleIgnoreOfferEnding());
                break;
            default:
                base.PlayerAction(action, index); 
                break;
        }
    }

    protected override void Report()
    {
        if (!firstTaskCompleted)
        {
            StartCoroutine(ReportToScamShield(uIManager, uIManager.winClip, uIManager.winScreen, whatHappenWinVideoClip, winVideoClip));
        }
        else
        {
            StartCoroutine(ReportToScamShield(uIManager, uIManager.loseClip, uIManager.loseScreen, whatHappenLoseVideoClip, gameOverVideoClip));
        }
    }


    private IEnumerator CreatingAccount()
    {
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.websiteHomeLoggedInScreen.SetActive(true);
        StartCoroutine(SpawnHomeButton(uIManager, 1));
    }

    private IEnumerator HandleFirstTaskGroupSelection()
    {
        uIManager.websiteHomeLoggedInScreen.SetActive(false);
        uIManager.websiteSelectTaskScreen.SetActive(false);
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.loadingScreen.SetActive(false);
        uIManager.taskScreen.SetActive(true);
        StartCoroutine(WaitAndContinueStory(1));
    }

    private IEnumerator HandleSecondTaskGroupSelection()
    {
        uIManager.websiteHomeAfterFirstTaskScreen.SetActive(false);
        uIManager.websiteSelectTaskScreen.SetActive(false);
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime * 2);
        StartCoroutine(SpawnHomeButton(uIManager, 0));
    }

    private IEnumerator HandleCheckOut()
    {
        uIManager.taskScreen.SetActive(false);
        uIManager.loadingBackToDashboardScreen.SetActive(true);
        firstTaskCompleted = true;
        uIManager.websiteFirstTaskGroup.SetActive(false);
        //CHECK OUT AUDIO PUT HERE
        yield return new WaitForSeconds(loadingTime);
        uIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
        StartCoroutine(SpawnHomeButton(uIManager, 1));
    }

    private IEnumerator Withdraw()
    {
        uIManager.websiteHomeAfterFirstTaskScreen.SetActive(false);
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.loadingScreen.SetActive(false);
        uIManager.websiteWithdrawErrorScreen.SetActive(true);
        uIManager.audioSource.clip = errorClip;
        uIManager.audioSource.Play();
        StartCoroutine(SpawnHomeButton(uIManager, 1));
    }

    private IEnumerator HandleLoseEnding()
    {
        uIManager.audioSource.clip = cryingClip;
        uIManager.audioSource.Play();
        yield return new WaitForSeconds(cryingClip.length);
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.loseScreen.SetActive(true);
        uIManager.audioSource.clip = uIManager.loseClip;
        uIManager.audioSource.Play();
        ClearChoices();
        Destroy(scamshieldButton);
        yield return new WaitForSeconds(uIManager.loseClip.length);
        uIManager.whatHappenButton.onClick.AddListener(() =>
        {
            recapVideoScript.PlayVideo(whatHappenLoseVideoClip);
        });
        ProceedToVideo(gameOverVideoClip);
    }

    private IEnumerator HandleIgnoreOfferEnding()
    {
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.audioSource.clip = uIManager.winClip;
        uIManager.audioSource.Play();
        uIManager.ignoreOfferScreen.SetActive(true);
        ClearChoices();
        Destroy(scamshieldButton);
        yield return new WaitForSeconds(uIManager.winClip.length);
        uIManager.whatHappenButton.onClick.AddListener(() =>
        {
            recapVideoScript.PlayVideo(whatHappenWinVideoClip);
        });
        ProceedToVideo(winVideoClip);
    }

    public override void SenderAction(string action, string dialogue)
    {
        switch(action)
        {
            case "image":
                messagingSystem.SenderImage(uIManager.scamPayoutImage);
                break;
            default:
                base.SenderAction(action, dialogue);
                break;
        }
    }
}
