/*
    Author: Kevin Heng
    Date: 03/08/2025
    Description: The PhishingScamManager class is used to handle all the functions related to the phishing scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class PhishingScamManager : InkManager
{
    public PhishingScamUIManager uIManager;

    private bool cdcClaimed;

    public override void DisplayChoices()
    {
        base.DisplayChoices();
        if (scamshieldButton == null && !cdcClaimed)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot();
                ClearChoices();
                Destroy(scamshieldButton);
                StartCoroutine(SpawnHomeButton(uIManager, 1));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }
    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "action_scammer_notification":
                uIManager.smsScammerScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(2));
                break;
            case "action_tap_link":
                uIManager.websiteHomeScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(2));
                break;
            case "action_fill_up_details":
                uIManager.userDetails.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_claim":
                uIManager.loadingScreen.SetActive(true);
                cdcClaimed = true;
                StartCoroutine(BankSMS());
                break;
            case "action_tap_bank_notification":
                uIManager.smsBankScreen.SetActive(true);
                Destroy(scamshieldButton);
                StartCoroutine(WaitAndContinueStory(2));
                break;
            case "action_screenshot":
                uIManager.Screenshot();
                StartCoroutine(WaitAndContinueStory(2));
                break;
            case "action_home_screen":
                uIManager.DisableAllCanvasChildren();
                uIManager.homeScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_open_scamshield":
                uIManager.scamshieldScreen.SetActive(true);
                StartCoroutine(WaitAndContinueStory(1));
                break;
            case "action_report_lose":
                uIManager.scamshieldLoadingScreen.SetActive(true);
                StartCoroutine(HandleLoseEnding());
                break;
            default:
                base.PlayerAction(action, index);
                break;
        }
    }

    private IEnumerator BankSMS()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(WaitAndContinueStory(0));
        uIManager.bankMessage.SetActive(true);
    }


    protected override void Report()
    {
        StartCoroutine(ReportToScamShield(uIManager, uIManager.winClip, uIManager.winScreen, whatHappenWinVideoClip, winVideoClip));
    }

    private IEnumerator HandleLoseEnding()
    {
        ClearChoices();
        Destroy(scamshieldButton);
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.audioSource.clip = uIManager.loseClip;
        uIManager.audioSource.Play();
        uIManager.reportAfterScammedScreen.SetActive(true);
        yield return new WaitForSeconds(uIManager.loseClip.length);
        uIManager.whatHappenButton.onClick.AddListener(() =>
        {
            recapVideoScript.PlayVideo(whatHappenLoseVideoClip);
        });
        ProceedToVideo(gameOverVideoClip);
    }
}
