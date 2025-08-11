/*
    Author: Kevin Heng
    Date: 04/08/2025
    Description: The NotPhishingScamUIManager class is used to manage all UI related functions to the non phishing scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotPhishingScamUIManager : UIManager
{
    public NotPhishingScamManager notScamManager;
    public PhishingScenariosAudioManager audioManager;

    public GameObject smsScreen;
    public GameObject websiteHomeScreen;
    public GameObject redeemScreen;
    public GameObject singPassLoginScreen;
    public GameObject singPassLoginSuccessScreen;
    public GameObject bankMessage;
    public GameObject smsBankScreen;
    public GameObject vouchersClaimedScreen;

    public void RealNotification()
    {
        smsScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void TapLink()
    {
        websiteHomeScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void ClaimCDC()
    {
        redeemScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void LoginSingpass()
    {
        singPassLoginScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void EnterSingpassPasscode()
    {
        singPassLoginSuccessScreen.SetActive(true);
        StartCoroutine(BankSMS());
    }

    private IEnumerator BankSMS()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(notScamManager.WaitAndContinueStory(0, audioManager));
        audioManager.PlayAudio(audioManager.smsNotification);
        bankMessage.SetActive(true);
    }

    public void TapBankSMSNotification()
    {
        smsBankScreen.SetActive(true);
        Destroy(notScamManager.scamshieldButton);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void TapLinkBank()
    {
        vouchersClaimedScreen.SetActive(true);
        StartCoroutine(HandleWinEnding());
    }

    public IEnumerator HandleWinEnding()
    {
        yield return new WaitForSeconds(2);
        notScamManager.ClearChoices(notScamManager.choiceContainer);
        Destroy(notScamManager.scamshieldButton);
        scenarioController.scenarioCanvas.SetActive(false);
        audioManager.PlayAudio(audioManager.winClip);
        winScreen.SetActive(true);
        yield return new WaitForEndOfFrame();
        whatHappenButton.onClick.AddListener(() =>
        {
            notScamManager.recapVideoScript.PlayVideo(notScamManager.whatHappenWinVideoClip);
        });
        notScamManager.ProceedToVideo(notScamManager.winVideoClip);
    }
}
