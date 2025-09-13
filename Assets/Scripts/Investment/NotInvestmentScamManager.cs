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
    /// <summary>
    /// Reference NotInvestmentScamUIManager script
    /// </summary>
    public NotInvestmentScamUIManager uIManager;
    /// <summary>
    /// Reference InvestmentScenariosAudioManager script
    /// </summary>
    public InvestmentScenariosAudioManager audioManager;

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_tap_notification", _ => uIManager.TapNotification()},
            { "message_add_pfp", index => uIManager.AddProfilePicture(index)},
            { "action_check_legitimacy", _ => StartCoroutine(uIManager.CheckLegitimacy())},
            { "action_open_kachagram", _ => uIManager.OpenKachagram()},
            { "win_ending", _ => StartCoroutine(uIManager.HandleWinEnding())},

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
        StartCoroutine(ReportToScamShield(uIManager, audioManager.loseClip, uIManager.loseScreen, whatHappenLoseVideoClip, gameOverVideoClip, audioManager));
        uIManager.whatHappenButton.gameObject.SetActive(false);
        uIManager.whatShouldYouDoButton.transform.position = uIManager.whatShouldYouDoButtonPos.position;
    }
}
