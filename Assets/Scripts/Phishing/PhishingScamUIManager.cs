/*
    Author: Kevin Heng
    Date: 03/08/2025
    Description: The PhishingScamUIManager class is used to manage all UI related functions to the phishing scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingScamUIManager : UIManager
{
    public PhishingScamManager scamManager;
    public PhishingScenariosAudioManager audioManager;

    public GameObject smsScammerScreen;
    public GameObject websiteHomeScreen;
    public GameObject userDetails;
    public GameObject loadingScreen;
    public GameObject bankMessage;
    public GameObject smsBankScreen;

    public override void Screenshot(InkManager inkManager, AudioManager audioManager)
    {
        if (!screenshotTaken)
        {
            screenshotTaken = true;
            audioManager.audioSource.clip = audioManager.screenshotClip;
            audioManager.audioSource.Play();
            flashImage.gameObject.SetActive(true);
            StartCoroutine(FlashEffect());
        }
    }

    public void ScammerNotification()
    {
        smsScammerScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));
    }

    public void TapLink()
    {
        websiteHomeScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void FillUpDetails()
    {
        userDetails.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void ClaimCDC()
    {
        loadingScreen.SetActive(true);
        scamManager.cdcClaimed = true;
        StartCoroutine(BankSMS());
    }

    private IEnumerator BankSMS()
    {
        yield return new WaitForSeconds(loadingTime);
        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));
        audioManager.PlayAudio(audioManager.smsNotification);
        bankMessage.SetActive(true);
    }

    public void TapBankSMSNotification()
    {
        smsBankScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.cryingClip);
        Destroy(scamManager.scamshieldButton);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void TakeScreenshot()
    {
        Screenshot(scamManager, audioManager);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void HomeScreen()
    {
        DisableAllCanvasChildren();
        homeScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void OpenScamShield()
    {
        scamshieldScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void ReportLose()
    {
        scamshieldLoadingScreen.SetActive(true);
        StartCoroutine(scamManager.ReportToScamShield(this, audioManager.loseClip, reportAfterScammedScreen, scamManager.whatHappenLoseVideoClip, scamManager.gameOverVideoClip, audioManager));
    }
}
