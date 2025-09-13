/*
    Author: Kevin Heng
    Date: 06/08/2025
    Description: The InvestmentScamManager class is used to handle all the dialogue choice options related to the investment scam scenario
*/
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class InvestmentScamManager : InkManager
{
    /// <summary>
    /// Reference InvestmentScamUIManager script
    /// </summary>
    public InvestmentScamUIManager uIManager;
    /// <summary>
    /// Reference InvestmentScenariosAudioManager script
    /// </summary>
    public InvestmentScenariosAudioManager audioManager;

    /// <summary>
    /// Boolean if money is invested in Bei Bei
    /// </summary>
    [Header("Booleans")]
    public bool isMoneyInvested;

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_scammer_notification", _ => uIManager.ScammerNotification()},
            { "message_add_pfp", index => uIManager.AddProfilePicture(index)},
            { "action_download_ezprofit", _ => StartCoroutine(uIManager.DownloadEZProfitAnimation())},
            { "action_create_ezprofit_account", _ => StartCoroutine(uIManager.CreateEZProfitAccount())},
            { "action_open_ezprofit", _ => uIManager.OpenEZProfitApp()},
            { "action_invest_bei_bei", _ => StartCoroutine(uIManager.InvestAnimation())},
            { "action_close_investment_confirmation", _ => uIManager.EarningsScreen()},
            { "action_withdraw_money", _ => StartCoroutine(uIManager.WithdrawEarnings())},
            { "action_withdraw_error", _ => uIManager.AskRachel()},
            { "lose_ending", _ => StartCoroutine(uIManager.HandleLoseEnding())},
        };
    }

    public override void DisplayChoices(AudioManager manager)
    {
        base.DisplayChoices(manager);
        if (scamshieldButton == null && !isOnHomeScreen)
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
    protected override void Report()
    {
        if (!isMoneyInvested)
        {
            StartCoroutine(ReportToScamShield(uIManager, audioManager.winClip, uIManager.winScreen, whatHappenWinVideoClip, winVideoClip, audioManager));
        }
        else
        {
            StartCoroutine(ReportToScamShield(uIManager, audioManager.loseClip, uIManager.reportAfterScammedScreen, whatHappenLoseVideoClip, gameOverVideoClip, audioManager));
        }

    }   
}
