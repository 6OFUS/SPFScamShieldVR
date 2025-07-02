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
    public JobScamUIManager jobScamUIManager;

    private TextMeshProUGUI currentInputFieldText;
    private Button currentChoiceButton;

    public AudioClip cryingClip;
    public VideoClip loseVideoClip;
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
                jobScamUIManager.Screenshot();
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
                jobScamUIManager.amailScreen.SetActive(true);
                break;
            case "message_register_account":
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                jobScamUIManager.websiteHomeScreen.SetActive(true);
                break;
            case "message_complete_task":
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                jobScamUIManager.whatsupScreen.SetActive(false);
                if (!firstTaskCompleted)
                {
                    jobScamUIManager.websiteHomeLoggedInScreen.SetActive(true);
                    jobScamUIManager.returnText.SetActive(false);
                    jobScamUIManager.websiteHomeSilverTierButton.SetActive(true);
                }
                else
                {
                    jobScamUIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
                    jobScamUIManager.websiteHomeAfterFirstTaskSilverTierButton.SetActive(true);
                }
                    break;
            case "message_withdraw":
                jobScamUIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
                jobScamUIManager.withdrawButton.SetActive(true);
                messagingSystem.PlayerNextMessage(playerChoices[index].choiceName);
                break;
            case "error_message":
                messagingSystem.PlayerNextMessage("<color=grey>You can no longer send messages to this contact.</color>");
                StartCoroutine(WaitForReply());
                break;
            case "lose_ending":
                StartCoroutine(CryAudio());
                break;
            default:
                base.PlayerAction(action, index); 
                break;
        }
    }
    private IEnumerator CryAudio()
    {
        jobScamUIManager.audioSource.clip = cryingClip;
        jobScamUIManager.audioSource.Play();
        yield return new WaitForSeconds(cryingClip.length);
        jobScamUIManager.scenarioController.canvas.SetActive(false);
        jobScamUIManager.loseScreen.SetActive(true);
        jobScamUIManager.audioSource.clip = jobScamUIManager.loseClip;
        jobScamUIManager.audioSource.Play();
        ClearChoices();
        Destroy(scamshieldButton);
        ProceedToVideo(loseVideoClip);
    }
    public override void SenderAction(string action, string dialogue)
    {
        switch(action)
        {
            case "image":
                messagingSystem.SenderImage(jobScamUIManager.scamPayoutImage);
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
            jobScamUIManager.loadingScreen.SetActive(true);
            yield return new WaitForSeconds(loadingTime);
            jobScamUIManager.websiteHomeLoggedInScreen.SetActive(true);
            jobScamUIManager.returnText.SetActive(true);

            jobScamUIManager.loadingScreen.SetActive(false);
            jobScamUIManager.websiteHomeScreen.SetActive(false);
            jobScamUIManager.whatsupScreen.SetActive(false);
            jobScamUIManager.websiteCreateAccountScreen.SetActive(false);

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
        jobScamUIManager.websiteHomeLoggedInScreen.SetActive(false);
        jobScamUIManager.websiteSelectTaskScreen.SetActive(false);
        jobScamUIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        jobScamUIManager.loadingScreen.SetActive(false);
        jobScamUIManager.taskScreen.SetActive(true);
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
        jobScamUIManager.websiteSelectTaskScreen.SetActive(false);
        jobScamUIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime * 2);
        jobScamUIManager.returnText.SetActive(true);
        jobScamUIManager.loadingScreenHomeButton.SetActive(true);
        knotName = "job_task_2_loading_error";
    }

    public void AddItemsToCart()
    {
        if(numItemsAdded < 3)
        {
            jobScamUIManager.itemNumUI[numItemsAdded].SetActive(true);
            numItemsAdded++;
        }
    }

    private IEnumerator LoadCheckOut()
    {
        jobScamUIManager.taskScreen.SetActive(false);
        jobScamUIManager.loadingBackToDashboardScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        jobScamUIManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
        jobScamUIManager.returnText.SetActive(true);
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
        jobScamUIManager.websiteHomeAfterFirstTaskScreen.SetActive(false);
        jobScamUIManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        jobScamUIManager.loadingScreen.SetActive(false);
        jobScamUIManager.websiteWithdrawErrorScreen.SetActive(true);
    }

    protected override IEnumerator ReportToScamShield()
    {
        yield return base.ReportToScamShield();
        jobScamUIManager.audioSource.clip = jobScamUIManager.winClip;
        jobScamUIManager.audioSource.Play();
        jobScamUIManager.winScreen.SetActive(true);
        ProceedToVideo(winVideoClip);
    }
}
