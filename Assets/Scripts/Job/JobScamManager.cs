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

    private TextMeshProUGUI currentInputFieldText;
    private Button currentChoiceButton;

    public AudioClip cryingClip;
    [Header("Educational videos")]
    public VideoClip gameOverVideoClip;
    public VideoClip winVideoClip;

    [Header("Account creation")]
    public int inputCount;
    public float loadingTime;

    [Header("Website task")]
    [SerializeField] private bool firstTaskCompleted;
    [SerializeField] private int numItemsAdded;

    public override void DisplayChoices()
    {
        base.DisplayChoices();
        if (scamshieldButton == null)
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
            case "open_amail":
                uIManager.amailScreen.SetActive(true);
                break;
            case "message_register_account":
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                uIManager.websiteHomeScreen.SetActive(true);
                break;
            case "message_complete_task":
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                uIManager.whatsupScreen.SetActive(false);
                if (!firstTaskCompleted)
                {
                    uIManager.websiteHomeLoggedInScreen.SetActive(true);
                    uIManager.returnText.SetActive(false);
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
                StartCoroutine(WaitForReply());
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
        ProceedToVideo(gameOverVideoClip);
    }

    private IEnumerator HandleIgnoreOfferEnding()
    {
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.audioSource.clip = uIManager.winClip;
        uIManager.audioSource.Play();
        uIManager.ignoreOfferScreen.SetActive(true);
        yield return new WaitForEndOfFrame(); // Wait for UI to fully update
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
    public void SetCurrentInputField(TextMeshProUGUI inputField)
    {
        currentInputFieldText = inputField;
    }
    public void SetCurrentChoiceButton(Button choiceButton)
    {
        currentChoiceButton = choiceButton;
    }

    public void InputChoice(string inputName)
    {
        GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceContainer);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = inputName;

        buttonObj.GetComponent<Button>().onClick.AddListener(() => {
            if (currentChoiceButton != null)
            {
                currentChoiceButton.interactable = false;
            }
            currentInputFieldText.text = inputName;
            currentInputFieldText.color = Color.black;
            ClearChoices();
            inputCount++;
        });

        if (scamshieldButton != null && scamshieldButton.transform.IsChildOf(choiceContainer))
        {
            scamshieldButton.transform.SetAsLastSibling();
        }
    }

    private IEnumerator RegisterAccountCoroutine()
    {
        if(inputCount == 4)
        {
            uIManager.loadingScreen.SetActive(true);
            yield return new WaitForSeconds(loadingTime);
            uIManager.websiteHomeLoggedInScreen.SetActive(true);
            uIManager.returnText.SetActive(true);

            uIManager.loadingScreen.SetActive(false);
            uIManager.websiteHomeScreen.SetActive(false);
            uIManager.whatsupScreen.SetActive(false);
            uIManager.websiteCreateAccountScreen.SetActive(false);

            knotName = "job_task_2_dialogue_1";
        }
        else
        {
            //ERROR AUDIO
        }
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
        uIManager.returnText.SetActive(true);
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
        uIManager.returnText.SetActive(true);
        knotName = "job_task_2_dialogue_2";
    }

    public void CheckOut()
    {
        if(numItemsAdded == 3)
        {
            StartCoroutine(LoadCheckOut());
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
    }

    protected override IEnumerator ReportToScamShield()
    {
        yield return base.ReportToScamShield();
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        if (!firstTaskCompleted)
        {
            uIManager.audioSource.clip = uIManager.winClip;
            uIManager.audioSource.Play();
            uIManager.winScreen.SetActive(true);
            ProceedToVideo(winVideoClip);
        }
        else
        {
            uIManager.audioSource.clip = uIManager.loseClip;
            uIManager.audioSource.Play();
            uIManager.reportAfterScammedScreen.SetActive(true);

            ProceedToVideo(gameOverVideoClip);
        }
    }
}
