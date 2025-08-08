/*
    Author: Kevin Heng
    Date: 03/08/2025
    Description: The PhishingScamUIManager class is used to manage all UI related to the phishing scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingScamUIManager : UIManager
{
    public PhishingScamManager scamManager;

    public GameObject smsScammerScreen;
    public GameObject websiteHomeScreen;
    public GameObject userDetails;
    public GameObject loadingScreen;
    public GameObject bankMessage;
    public GameObject smsBankScreen;

    public override void Screenshot()
    {
        if (!screenshotTaken)
        {
            screenshotTaken = true;
            audioSource.clip = screenshotClip;
            audioSource.Play();
            flashImage.gameObject.SetActive(true);
            StartCoroutine(FlashEffect());
        }
    }

    public void ScammerNotification()
    {
        smsScammerScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(0));
    }

    public void TapLink()
    {
        websiteHomeScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void FillUpDetails()
    {
        userDetails.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
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
        StartCoroutine(scamManager.WaitAndContinueStory(0));
        bankMessage.SetActive(true);
    }

    public void TapBankSMSNotification()
    {
        smsBankScreen.SetActive(true);
        Destroy(scamManager.scamshieldButton);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void TakeScreenshot()
    {
        Screenshot();
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void HomeScreen()
    {
        DisableAllCanvasChildren();
        homeScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void OpenScamShield()
    {
        scamshieldScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void ReportLose()
    {
        scamshieldLoadingScreen.SetActive(true);
        StartCoroutine(scamManager.ReportToScamShield(this, loseClip, reportAfterScammedScreen, scamManager.whatHappenLoseVideoClip, scamManager.gameOverVideoClip));
    }
}
