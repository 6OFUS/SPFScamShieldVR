/*
    Author: Kevin Heng
    Date: 04/08/2025
    Description: The NotPhishingScamManager class is used to handle all the dialogue choice options related to the non phishing scam scenario
*/
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NotPhishingScamManager : InkManager
{
    /// <summary>
    /// Reference NotPhishingScamUIManager script
    /// </summary>
    public NotPhishingScamUIManager uIManager;
    /// <summary>
    /// Reference PhishingScenariosAudioManager script
    /// </summary>
    public PhishingScenariosAudioManager audioManager;

    public override void DisplayChoices(AudioManager manager)
    {
        manager = audioManager;
        base.DisplayChoices(manager);
        if (scamshieldButton == null && !uIManager.screenshotTaken)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot(this, audioManager);
                ClearChoices(choiceContainer);
                Destroy(scamshieldButton);
                StartCoroutine(SpawnOpenScamshieldButton(uIManager, audioManager));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_real_notification", _ => uIManager.RealNotification()},
            { "action_tap_link", _ => uIManager.TapLink()},
            { "action_claim", _ => uIManager.ClaimCDC()},
            { "action_login_singpass", _ => uIManager.LoginSingpass()},
            { "action_passcode", _ => uIManager.EnterSingpassPasscode()},
            { "action_tap_bank_notification", _ => uIManager.TapBankSMSNotification()},
            { "action_tap_link_bank", _ => uIManager.TapLinkBank()},

        };
    }

    protected override void Report()
    {
        StartCoroutine(ReportToScamShield(uIManager, audioManager.loseClip, uIManager.loseScreen, whatHappenLoseVideoClip, gameOverVideoClip, audioManager));
        uIManager.whatHappenButton.gameObject.SetActive(false);
        uIManager.whatShouldYouDoButton.transform.position = uIManager.whatShouldYouDoButtonPos.position;
    }


}
