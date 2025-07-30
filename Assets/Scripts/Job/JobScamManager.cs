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

    private bool isOnHomeScreen;

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
                StartCoroutine(SpawnHomeButton(1));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }
    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "action_open_notification":
                uIManager.whatsupScreen.SetActive(true);
                StartCoroutine(WaitForReply(0));
                break;
            case "action_open_amail":
                uIManager.amailScreen.SetActive(true);
                StartCoroutine(WaitForReply(1));
                break;
            case "action_open_jason_email":
                uIManager.jasonEmailScreen.SetActive(true);
                StartCoroutine(SpawnHomeButton(5));
                break;
            case "message_register_account":
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                uIManager.websiteHomeScreen.SetActive(true);
                StartCoroutine(WaitForReply(1));
                break;

            case "action_create_account":
                uIManager.websiteCreateAccountScreen.SetActive(true);
                StartCoroutine(SpawnEnterDetailsButton());
                break;

            case "message_complete_task":
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                uIManager.whatsupScreen.SetActive(false);
                if (!firstTaskCompleted)
                {
                    uIManager.websiteHomeLoggedInScreen.SetActive(true);
                }
                else
                {
                    uIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
                }
                StartCoroutine(SpawnSelectSilverTierButton());
                break;
            case "message_withdraw":
                uIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                StartCoroutine(SpawnWithdrawButton());
                break;
            case "error_message":
                messagingSystem.PlayerNextMessage("<color=grey>You can no longer send messages to this contact.</color>");
                StartCoroutine(WaitForReply(messageTime));
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

    private IEnumerator SpawnHomeButton(float time)
    {
        yield return SpawnActionButton("Go to home screen", time, () => {
            isOnHomeScreen = true;
            uIManager.DisableAllCanvasChildren();
            uIManager.homeScreen.SetActive(true);
            if(scamshieldButton != null)
            {
                Destroy(scamshieldButton);
            }
            if (uIManager.screenshotTaken)
            {
                StartCoroutine(SpawnOpenScamshieldButton());
            }
            else
            {
                StartCoroutine(SpawnOpenWhatsUpButton(1));
            }
        });
    }

    private IEnumerator SpawnOpenWhatsUpButton(float time)
    {
        yield return SpawnActionButton("Open WhatsUp", time, () => {
            uIManager.whatsupScreen.SetActive(true);
            StartCoroutine(WaitForReply(0));
            isOnHomeScreen = false;
        });
    }

    private IEnumerator SpawnOpenScamshieldButton()
    {
        yield return SpawnActionButton("Open Scamshield", 1f, () => {
            uIManager.scamshieldScreen.SetActive(true);
            isOnHomeScreen = false;
            StartCoroutine(SpawnReportButton());
        });
    }

    private IEnumerator SpawnReportButton()
    {
        yield return SpawnActionButton("Report", 1f, () => {
            Report();
        });
    }

    protected override IEnumerator ReportToScamShield()
    {
        uIManager.scamshieldLoadingScreen.SetActive(true);
        yield return base.ReportToScamShield();
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        if (!firstTaskCompleted)
        {
            uIManager.audioSource.clip = uIManager.winClip;
            uIManager.audioSource.Play();
            uIManager.winScreen.SetActive(true);
            uIManager.whatHappenButton.onClick.AddListener(() =>
            {
                recapVideoScript.PlayVideo(whatHappenWinVideoClip);
            });
            ProceedToVideo(winVideoClip);
        }
        else
        {
            uIManager.audioSource.clip = uIManager.loseClip;
            uIManager.audioSource.Play();
            uIManager.reportAfterScammedScreen.SetActive(true);
            uIManager.whatHappenButton.onClick.AddListener(() =>
            {
                recapVideoScript.PlayVideo(whatHappenLoseVideoClip);
            });
            ProceedToVideo(gameOverVideoClip);
        }
    }

    private IEnumerator SpawnEnterDetailsButton()
    {
        yield return SpawnActionButton("Enter details", 1f, () => {
            for (int i = 0; i < uIManager.detailsInputText.Length; i++)
            {
                uIManager.detailsInputText[i].color = Color.black;
                uIManager.detailsInputText[i].text = uIManager.detailsTextContent[i];
            }
            StartCoroutine(SpawnCreateAccountButton());
        });
    }

    private IEnumerator SpawnCreateAccountButton()
    {
        yield return SpawnActionButton("Submit and create account", 1f, () => {
            StartCoroutine(CreatingAccount());
        });
    }

    private IEnumerator CreatingAccount()
    {
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.websiteHomeLoggedInScreen.SetActive(true);
        StartCoroutine(SpawnHomeButton(1));
    }

    private IEnumerator SpawnSelectSilverTierButton()
    {
        yield return SpawnActionButton("Select silver tier", 1f, () => {
            uIManager.websiteSelectTaskScreen.SetActive(true);
            if (!firstTaskCompleted)
            {
                StartCoroutine(LoadFirstTaskGroup());
            }
            else
            {
                uIManager.websiteHomeAfterFirstTaskScreen.SetActive(false);
                StartCoroutine(LoadingError());
            }
        });
    }

    private IEnumerator LoadFirstTaskGroup()
    {
        yield return SpawnActionButton("Select task 1", 1f, () => {
            StartCoroutine(HandleFirstTaskGroupSelection());
        });
    }

    private IEnumerator HandleFirstTaskGroupSelection()
    {
        uIManager.websiteHomeLoggedInScreen.SetActive(false);
        uIManager.websiteSelectTaskScreen.SetActive(false);
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.loadingScreen.SetActive(false);
        uIManager.taskScreen.SetActive(true);
        StartCoroutine(AddItems());
    }

    private IEnumerator HandleSecondTaskGroupSelection()
    {
        uIManager.websiteSelectTaskScreen.SetActive(false);
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime * 2);
        StartCoroutine(SpawnHomeButton(0));
        knotName = "job_task_2_loading_error";
    }

    private IEnumerator LoadingError()
    {
        yield return SpawnActionButton("Select task 2", 1f, () => {
            StartCoroutine(HandleSecondTaskGroupSelection());
        });
    }

    private IEnumerator AddItems()
    {
        yield return SpawnActionButton("Add items to cart", 1f, () => {
            uIManager.itemNumThree.SetActive(true);
            StartCoroutine(LoadCheckOut());
        });
    }
    private IEnumerator LoadCheckOut()
    {
        yield return SpawnActionButton("Check out", 1f, () => {
            StartCoroutine(HandleCheckOut());
        });
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
        knotName = "job_task_2_dialogue_2";
        StartCoroutine(SpawnHomeButton(1));
    }

    private IEnumerator SpawnWithdrawButton()
    {
        yield return SpawnActionButton("Withdraw", 1f, () => {
            StartCoroutine(Withdraw());
        });
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
        StartCoroutine(SpawnHomeButton(1));
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
        yield return new WaitForEndOfFrame(); // Wait for UI to fully update
        ClearChoices();
        Destroy(scamshieldButton);
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
