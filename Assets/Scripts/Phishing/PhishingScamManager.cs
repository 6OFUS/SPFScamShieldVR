/*
    Author: Kevin Heng
    Date: 03/08/2025
    Description: The PhishingScamManager class is used to handle all the dialogue choice options related to the phishing scam scenario
*/
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PhishingScamManager : InkManager
{
    public PhishingScamUIManager uIManager;

    public PhishingScenariosAudioManager audioManager;

    public bool cdcClaimed;

    public override void DisplayChoices(AudioManager manager)
    {
        manager = audioManager;
        base.DisplayChoices(manager);
        if (scamshieldButton == null && !cdcClaimed)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot(this, audioManager);
                ClearChoices(choiceContainer);
                Destroy(scamshieldButton);
                StartCoroutine(SpawnHomeButton(uIManager, 1, audioManager));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_scammer_notification", _ => uIManager.ScammerNotification()},
            { "action_tap_link", _ => uIManager.TapLink()},
            { "action_fill_up_details", _ => uIManager.FillUpDetails()},
            { "action_claim", _ => uIManager.ClaimCDC()},
            { "action_tap_bank_notification", _ => uIManager.TapBankSMSNotification()},
            { "action_screenshot", _ => uIManager.TakeScreenshot()},
            { "action_home_screen", _ => uIManager.HomeScreen()},
            { "action_open_scamshield", _ => uIManager.OpenScamShield()},
            { "action_report_lose", _ => uIManager.ReportLose()},

        };
    }

    protected override void Report()
    {
        if (!cdcClaimed)
        {
            StartCoroutine(ReportToScamShield(uIManager, audioManager.winClip, uIManager.winScreen, whatHappenWinVideoClip, winVideoClip, audioManager));
        }
        else
        {
            StartCoroutine(ReportToScamShield(uIManager, audioManager.loseClip, uIManager.reportAfterScammedScreen, whatHappenLoseVideoClip, gameOverVideoClip, audioManager));
        }
    }
}
