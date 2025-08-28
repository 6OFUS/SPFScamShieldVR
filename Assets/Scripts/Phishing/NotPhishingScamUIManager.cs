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
    /// <summary>
    /// Reference NotPhishingScamManager script
    /// </summary>
    public NotPhishingScamManager notScamManager;
    /// <summary>
    /// Reference PhishingScenariosAudioManager script
    /// </summary>
    public PhishingScenariosAudioManager audioManager;

    /// <summary>
    /// SMS screen UI
    /// </summary>
    [Header("Phone UIs")]
    public GameObject smsScreen;
    /// <summary>
    /// Website home screen UI
    /// </summary>
    public GameObject websiteHomeScreen;
    /// <summary>
    /// Redeem CDC voucher screen
    /// </summary>
    public GameObject redeemScreen;
    /// <summary>
    /// Singpass login screen
    /// </summary>
    public GameObject singPassLoginScreen;
    /// <summary>
    /// Singpass login success screen
    /// </summary>
    public GameObject singPassLoginSuccessScreen;
    /// <summary>
    /// Bank SMS message notification
    /// </summary>
    public GameObject bankMessage;
    /// <summary>
    /// Bank SMS screen
    /// </summary>
    public GameObject smsBankScreen;
    /// <summary>
    /// CDC vouchers claimed screen
    /// </summary>
    public GameObject vouchersClaimedScreen;

    /// <summary>
    /// Change UI to show SMS chat screen after selecting option to open phone notification
    /// </summary>
    public void RealNotification()
    {
        smsScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Change UI to CDC voucher website home screen after selecting option to open link
    /// </summary>
    public void TapLink()
    {
        websiteHomeScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Change UI to redeem CDC voucher screen after selecting option to redeem vouchers
    /// </summary>
    public void ClaimCDC()
    {
        redeemScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Change UI to Singpass login screen after selecting option to login
    /// </summary>
    public void LoginSingpass()
    {
        singPassLoginScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Change UI to Singpass login success screen after selecting option to enter passcode
    /// </summary>
    public void EnterSingpassPasscode()
    {
        singPassLoginSuccessScreen.SetActive(true);
        StartCoroutine(BankSMS());
    }

    /// <summary>
    /// Wait for bank SMS notification to appear
    /// </summary>
    /// <returns>Time taken for message to be received</returns>
    private IEnumerator BankSMS()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(notScamManager.WaitAndContinueStory(0, audioManager));
        audioManager.PlayAudio(audioManager.smsNotification);
        bankMessage.SetActive(true);
    }

    /// <summary>
    /// Change UI to bank SMS chat screen after selecting option to open bank SMS notification
    /// </summary>
    public void TapBankSMSNotification()
    {
        smsBankScreen.SetActive(true);
        Destroy(notScamManager.scamshieldButton);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Change UI to CDC vouchers claimed screen after selecting option to open link sent by the bank
    /// </summary>
    public void TapLinkBank()
    {
        vouchersClaimedScreen.SetActive(true);
        StartCoroutine(HandleWinEnding());
    }

    /// <summary>
    /// Change UI to win screen after selecting final option
    /// </summary>
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
