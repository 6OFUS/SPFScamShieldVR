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
    /// <summary>
    /// Reference PhishingScamManager script
    /// </summary>
    public PhishingScamManager scamManager;
    /// <summary>
    /// Reference PhishingScenariosAudioManager script
    /// </summary>
    public PhishingScenariosAudioManager audioManager;

    /// <summary>
    /// Scammer SMS screen
    /// </summary>
    [Header("Phone UIs")]
    public GameObject smsScammerScreen;
    /// <summary>
    /// Scam website home screen
    /// </summary>
    public GameObject websiteHomeScreen;
    /// <summary>
    /// User details group
    /// </summary>
    public GameObject userDetails;
    /// <summary>
    /// Loading screen
    /// </summary>
    public GameObject loadingScreen;
    /// <summary>
    /// Bank SMS notification
    /// </summary>
    public GameObject bankMessage;
    /// <summary>
    /// Bank SMS screen
    /// </summary>
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

    /// <summary>
    /// Change UI to show scammer SMS chat screen after selecting option to open phone notification
    /// </summary>
    public void ScammerNotification()
    {
        smsScammerScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));
    }

    /// <summary>
    /// Change UI to scam CDC voucher website home screen after selecting option to open link
    /// </summary>
    public void TapLink()
    {
        websiteHomeScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Show UI group of user details after selecting option to enter details
    /// </summary>
    public void FillUpDetails()
    {
        userDetails.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Change UI to loading screen after selecting option to claim CDC vouchers
    /// </summary>
    public void ClaimCDC()
    {
        loadingScreen.SetActive(true);
        scamManager.cdcClaimed = true;
        StartCoroutine(BankSMS());
    }

    /// <summary>
    /// Wait for bank SMS notification to appear
    /// </summary>
    /// <returns>Time taken for message to be received</returns>
    private IEnumerator BankSMS()
    {
        yield return new WaitForSeconds(loadingTime);
        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));
        audioManager.PlayAudio(audioManager.smsNotification);
        bankMessage.SetActive(true);
    }

    /// <summary>
    /// Change UI to bank SMS chat screen after selecting option to open bank SMS notification
    /// </summary>
    public void TapBankSMSNotification()
    {
        smsBankScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.cryingClip);
        Destroy(scamManager.scamshieldButton);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Take screenshot after bank sms chat screen shows
    /// </summary>
    public void TakeScreenshot()
    {
        Screenshot(scamManager, audioManager);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Function to open scamshield app
    /// Change UI to scamshield screen after selecting option to open Scamshield app
    /// </summary>
    public void OpenScamShield()
    {
        scamshieldScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Handle end result of reporting after losing
    /// </summary>
    public void ReportLose()
    {
        scamshieldLoadingScreen.SetActive(true);
        StartCoroutine(scamManager.ReportToScamShield(this, audioManager.loseClip, reportAfterScammedScreen, scamManager.whatHappenLoseVideoClip, scamManager.gameOverVideoClip, audioManager));
    }
}
