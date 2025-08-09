/*
    Author: Kevin Heng
    Date: 06/08/2025
    Description: The InvestmentScamManager class is used to handle all the functions related to the investment scam scenario
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
    public InvestmentScamUIManager uIManager;

    public bool eZProfitAccountCreated;
    public bool isInvestAmountEntered;
    public bool isMoneyInvested;

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_scammer_notification", _ => uIManager.ScammerNotification() },
            { "message_add_pfp", index => uIManager.AddProfilePicture(index) },
            { "action_home_screen", _ => uIManager.HomeScreen() },
            { "action_open_appshop", _ => uIManager.OpenAppShop()},
            { "action_appshop_ezprofit", _ => uIManager.OpenEZProfitInAppShop()},
            { "action_download_ezprofit", _ => StartCoroutine(uIManager.DownloadingEZProfit()) },
            { "action_open_ezprofit", _ => uIManager.OpenEZProfitApp()},
            { "action_fill_details_ezprofit", _ => uIManager.FillUpAccountDetails()},
            { "action_signup_ezprofit", _ => uIManager.SignUpEZProfitAccount()},
            { "action_open_kachagram", _ => uIManager.OpenKachagram()},
            { "action_tap_bei_bei", _ => uIManager.TapOnBeiBei()},
            { "action_tap_on_invest", _ => uIManager.InvestInBaoBei()},
            { "action_invest_$300", _ => uIManager.InvestAmount()},
            { "action_add_payment_method", _ => uIManager.AddNewPaymentMethod()},
            { "action_scan_credit_card", _ => StartCoroutine(uIManager.ScanCreditCard())},
            { "action_add_credit_card", _ => uIManager.AddCreditCard()},
            { "action_pick_card", _ => StartCoroutine(uIManager.PickCard())},
            { "action_close_investment_confirmation", _ => uIManager.EarningsScreen()},
            { "action_withdraw_money", _ => StartCoroutine(uIManager.WithdrawEarnings())},
            { "action_withdraw_error", _ => uIManager.AskRachel()},
            { "lose_ending", _ => uIManager.kachagramAccountMissingScreen.SetActive(true)},
        };
    }

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
    protected override void Report()
    {
        if (!isMoneyInvested)
        {
            StartCoroutine(ReportToScamShield(uIManager, uIManager.winClip, uIManager.winScreen, whatHappenWinVideoClip, winVideoClip));
        }
        else
        {
            StartCoroutine(ReportToScamShield(uIManager, uIManager.loseClip, uIManager.reportAfterScammedScreen, whatHappenLoseVideoClip, gameOverVideoClip));
        }

    }   
}
