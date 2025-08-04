/*
    Author: Kevin Heng
    Date: 04/08/2025
    Description: The NotPhishingScamUIManager class is used to manage all UI related to the non phishing scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class NotPhishingScamManager : InkManager
{
    public NotPhishingScamUIManager uIManager;
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
                StartCoroutine(SpawnHomeButton(1));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }
    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "action_real_notification":
                uIManager.smsScreen.SetActive(true);
                StartCoroutine(WaitForReply(2));
                break;
            case "action_tap_link":
                uIManager.websiteHomeScreen.SetActive(true);
                StartCoroutine(WaitForReply(2));
                break;
            case "action_claim":
                uIManager.redeemScreen.SetActive(true);
                StartCoroutine(WaitForReply(2));
                break;
            case "action_login_singpass":             
                uIManager.singPassLoginScreen.SetActive(true);
                StartCoroutine(WaitForReply(2));
                break;
            case "action_passcode":
                uIManager.singPassLoginSuccessScreen.SetActive(true);
                cdcClaimed = true;
                StartCoroutine(BankSMS());
                break;
            case "action_tap_bank_notification":
                uIManager.smsBankScreen.SetActive(true);
                Destroy(scamshieldButton);
                StartCoroutine(WaitForReply(2));
                break;
            case "action_tap_link_bank":
                uIManager.vouchersClaimedScreen.SetActive(true);
                //win ending
                StartCoroutine(HandleWinEnding());
                break;
            default:
                base.PlayerAction(action, index);
                break;
        }
    }

    private IEnumerator BankSMS()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(WaitForReply(0));
        uIManager.bankMessage.SetActive(true);
    }

    private IEnumerator SpawnHomeButton(float time)
    {
        yield return SpawnActionButton("Go to home screen", time, () => {
            isOnHomeScreen = true;
            uIManager.DisableAllCanvasChildren();
            uIManager.homeScreen.SetActive(true);
            if (scamshieldButton != null)
            {
                Destroy(scamshieldButton);
            }
            if (uIManager.screenshotTaken)
            {
                StartCoroutine(SpawnOpenScamshieldButton());
            }
        });
    }

    private IEnumerator SpawnOpenScamshieldButton()
    {
        yield return SpawnActionButton("Open Scamshield app", 1f, () => {
            uIManager.scamshieldScreen.SetActive(true);
            isOnHomeScreen = false;
            StartCoroutine(SpawnReportButton());
        });
    }

    private IEnumerator SpawnReportButton()
    {
        yield return SpawnActionButton("Report", 1f, () => {
            Report();
        });
    }

    protected override IEnumerator ReportToScamShield()
    {
        uIManager.scamshieldLoadingScreen.SetActive(true);
        yield return base.ReportToScamShield();
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.audioSource.clip = uIManager.loseClip;
        uIManager.audioSource.Play();
        uIManager.loseScreen.SetActive(true);
        yield return new WaitForSeconds(uIManager.loseClip.length);
        uIManager.whatHappenButton.onClick.AddListener(() =>
        {
            recapVideoScript.PlayVideo(whatHappenLoseVideoClip);
        });
        ProceedToVideo(gameOverVideoClip);
    }

    private IEnumerator HandleWinEnding()
    {
        yield return new WaitForSeconds(2);
        ClearChoices();
        Destroy(scamshieldButton);
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.audioSource.clip = uIManager.winClip;
        uIManager.audioSource.Play();
        uIManager.winScreen.SetActive(true);
        yield return new WaitForSeconds(uIManager.winClip.length);
        uIManager.whatHappenButton.onClick.AddListener(() =>
        {
            recapVideoScript.PlayVideo(whatHappenWinVideoClip);
        });
        ProceedToVideo(winVideoClip);
    }
}
