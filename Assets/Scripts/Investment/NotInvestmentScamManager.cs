/*
    Author: Kevin Heng
    Date: 09/08/2025
    Description: The NotInvestmentScamManager class is used to handle all the dialogue choice options related to the non investment scam scenario
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NotInvestmentScamManager : InkManager
{
    public NotInvestmentScamUIManager uIManager;

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_tap_notification", _ => uIManager.TapNotification()},
            { "message_add_pfp", index => uIManager.AddProfilePicture(index)},
            { "action_home_screen", _ => uIManager.HomeScreen()},
            { "action_browze_plus", _ => uIManager.OpenBrowzePlus()},
            { "action_search", _ => uIManager.SearchBrowzePlus()},
            { "action_tap_link", _ => uIManager.TapFirstLink()},
            { "action_key_representative_number", _ => uIManager.EnterRepresentativeNumber()},
            { "action_search_representative", _ => StartCoroutine(uIManager.SearchRepresentative())},
            { "action_open_kachagram", _ => uIManager.OpenKachagram()},
            { "win_ending", _ => StartCoroutine(uIManager.HandleWinEnding())},

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
                StartCoroutine(SpawnHomeButton(uIManager, 1));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }
    protected override void Report()
    {
        StartCoroutine(ReportToScamShield(uIManager, uIManager.loseClip, uIManager.loseScreen, whatHappenLoseVideoClip, gameOverVideoClip));
    }
}
