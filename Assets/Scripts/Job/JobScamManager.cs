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
                StartCoroutine(WaitForReply(2));
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
                    uIManager.websiteHomeSilverTierButton.SetActive(true);
                }
                else
                {
                    uIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
                    uIManager.websiteHomeAfterFirstTaskSilverTierButton.SetActive(true);
                }
                break;
            case "message_withdraw":
                uIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
                uIManager.withdrawButton.SetActive(true);
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
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
        yield return new WaitForSeconds(time);

        GameObject buttonObj = Instantiate(actionChoiceButtonPrefab, choiceContainer);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Go to home screen";

        buttonObj.GetComponent<Button>().onClick.AddListener(() => {
            isOnHomeScreen = true;
            uIManager.DisableAllCanvasChildren();
            uIManager.homeScreen.SetActive(true);
            Destroy(scamshieldButton);
            StartCoroutine(SpawnOpenWhatsUpButton(1));
            Destroy(buttonObj);
        });
        scamshieldButton.transform.SetAsLastSibling();
    }
    private IEnumerator SpawnOpenWhatsUpButton(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject buttonObj = Instantiate(actionChoiceButtonPrefab, choiceContainer);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Open WhatsUp";

        buttonObj.GetComponent<Button>().onClick.AddListener(() => {
            uIManager.whatsupScreen.SetActive(true);
            StartCoroutine(WaitForReply(0));
            isOnHomeScreen = false;
            Destroy(buttonObj);
        });
        scamshieldButton.transform.SetAsLastSibling();
    }

    private IEnumerator SpawnEnterDetailsButton()
    {
        yield return new WaitForSeconds(1);

        GameObject buttonObj = Instantiate(actionChoiceButtonPrefab, choiceContainer);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Enter details";

        buttonObj.GetComponent<Button>().onClick.AddListener(() => {
            for(int i = 0; i < uIManager.detailsInputText.Length; i++)
            {
                uIManager.detailsInputText[i].color = Color.black;
                uIManager.detailsInputText[i].text = uIManager.detailsTextContent[i];
            }
            Destroy(buttonObj);
            //spawn create account button
            StartCoroutine(SpawnCreateAccountButton());
        });
        scamshieldButton.transform.SetAsLastSibling();
    }

    private IEnumerator SpawnCreateAccountButton()
    {
        yield return new WaitForSeconds(1);

        GameObject buttonObj = Instantiate(actionChoiceButtonPrefab, choiceContainer);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Submit and create account";

        buttonObj.GetComponent<Button>().onClick.AddListener(() => {
            StartCoroutine(CreatingAccount());
            Destroy(buttonObj);

        });
        scamshieldButton.transform.SetAsLastSibling();
    }

    private IEnumerator CreatingAccount()
    {
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.websiteHomeLoggedInScreen.SetActive(true);
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

    private IEnumerator RegisterAccountCoroutine()
    {
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.websiteHomeLoggedInScreen.SetActive(true);


        knotName = "job_task_2_dialogue_1";
    }

    public void RegisterAccount()
    {
        StartCoroutine(RegisterAccountCoroutine());
    }

    public void FirstTaskGroup()
    {
        if (!firstTaskCompleted)
        {
            StartCoroutine(LoadFirstTaskGroup());
        }
    }

    private IEnumerator LoadFirstTaskGroup()
    {
        uIManager.websiteHomeLoggedInScreen.SetActive(false);
        uIManager.websiteSelectTaskScreen.SetActive(false);
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.loadingScreen.SetActive(false);
        uIManager.taskScreen.SetActive(true);
        firstTaskCompleted = true;
    }

    public void FollowingTaskGroups()
    {
        if (firstTaskCompleted)
        {
            StartCoroutine(LoadingError());
        }
    }

    private IEnumerator LoadingError()
    {
        uIManager.websiteSelectTaskScreen.SetActive(false);
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime * 2);
        uIManager.loadingScreenHomeButton.SetActive(true);
        knotName = "job_task_2_loading_error";
    }

    public void AddItemsToCart()
    {
        if(numItemsAdded < 3)
        {
            uIManager.itemNumUI[numItemsAdded].SetActive(true);
            numItemsAdded++;
        }
    }

    private IEnumerator LoadCheckOut()
    {
        uIManager.taskScreen.SetActive(false);
        uIManager.loadingBackToDashboardScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
        knotName = "job_task_2_dialogue_2";
    }

    public void CheckOut()
    {
        if(numItemsAdded == 3)
        {
            uIManager.audioSource.clip = checkOutClip;
            uIManager.audioSource.Play();
            StartCoroutine(LoadCheckOut());
        }
        else
        {
            uIManager.audioSource.clip = errorClip;
            uIManager.audioSource.Play();
        }
    }

    public void Withdraw()
    {
        StartCoroutine(WithdrawCoroutine());
    }

    private IEnumerator WithdrawCoroutine()
    {
        uIManager.websiteHomeAfterFirstTaskScreen.SetActive(false);
        uIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uIManager.loadingScreen.SetActive(false);
        uIManager.websiteWithdrawErrorScreen.SetActive(true);
        uIManager.audioSource.clip = errorClip;
        uIManager.audioSource.Play();
    }

    protected override IEnumerator ReportToScamShield()
    {
        yield return base.ReportToScamShield();
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        if (firstTaskCompleted)
        {
            uIManager.audioSource.clip = uIManager.winClip;
            uIManager.audioSource.Play();
            uIManager.winScreen.SetActive(true);
            uIManager.whatHappenButton.onClick.AddListener(() =>
            {
                recapVideoScript.PlayVideo(whatHappenLoseVideoClip);
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
                recapVideoScript.PlayVideo(whatHappenWinVideoClip);
            });
            ProceedToVideo(gameOverVideoClip);
        }
    }
}
