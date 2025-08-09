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
    public bool firstTaskCompleted;

    public override void DisplayChoices()
    {
        base.DisplayChoices();
        if (scamshieldButton == null && !isOnHomeScreen)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot(this);
                ClearChoices();
                Destroy(scamshieldButton);
                StartCoroutine(SpawnHomeButton(uIManager,1));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_tap_notification", _ => uIManager.TapNotification()},
            { "action_open_amail", _ => uIManager.OpenAmail()},
            { "action_open_jason_email", _ => uIManager.OpenJasonEmail()},
            { "message_register_account", index => uIManager.MessageAndRegisterAccount(index)},
            { "action_create_account" , _ => uIManager.CreateAccount()},
            { "action_enter_details", _ => uIManager.EnterAccountDetails()},
            { "action_submit_and_create", _ => StartCoroutine(uIManager.CreatingAccount())},
            { "message_complete_task", index => uIManager.MessageAndCompleteFirstTask(index)},
            { "action_select_silver_tier", _ => uIManager.SelectSilverTier()},
            { "action_select_task_1", _ => StartCoroutine(uIManager.HandleFirstTaskGroupSelection())},
            { "action_add_items", _ => uIManager.AddItemsToCart()},
            { "action_check_out", _ => StartCoroutine(uIManager.HandleCheckOut())},
            { "message_complete_task_2", index => uIManager.MessageAndCompleteSecondTask(index)},
            { "action_select_task_2", _ => StartCoroutine(uIManager.HandleSecondTaskGroupSelection())},
            { "message_withdraw", index => uIManager.MessageAndWithdraw(index)},
            { "action_withdraw", _ => StartCoroutine(uIManager.Withdraw())},
            { "error_message", _ => ErrorMessage()},
            { "lose_ending", _ => StartCoroutine(uIManager.HandleLoseEnding())},
            { "ignre_ending", _ => StartCoroutine(uIManager.HandleIgnoreOfferEnding())},
        };
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

    private void ErrorMessage()
    {
        messagingSystem.PlayerNextMessage("<color=grey>You can no longer send messages to this contact.</color>");
        StartCoroutine(WaitAndContinueStory(messageTime));
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
