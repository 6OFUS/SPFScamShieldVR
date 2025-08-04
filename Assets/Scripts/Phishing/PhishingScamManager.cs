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
                StartCoroutine(WaitForReply(2));
                break;
            case "action_tap_link":
                uIManager.websiteHomeScreen.SetActive(true);
                StartCoroutine(WaitForReply(2));
                break;
            case "action_fill_up_details":
                uIManager.userDetails.SetActive(true);
                StartCoroutine(WaitForReply(1));
                break;
            case "action_claim":
                uIManager.loadingScreen.SetActive(true);
                cdcClaimed = true;
                StartCoroutine(BankSMS());
                break;
            case "action_tap_bank_notification":
                uIManager.smsBankScreen.SetActive(true);
                Destroy(scamshieldButton);
                StartCoroutine(WaitForReply(2));
                break;
            case "action_home_screen":
                uIManager.DisableAllCanvasChildren();
                uIManager.homeScreen.SetActive(true);
                StartCoroutine(WaitForReply(1));
                break;
            case "action_open_scamshield":
                uIManager.scamshieldScreen.SetActive(true);
                StartCoroutine(WaitForReply(1));
                break;
            case "action_report":
                uIManager.scamshieldLoadingScreen.SetActive(true);
                //check if got scammed or not
                StartCoroutine(WaitForReply(1));
                break;
            default:
                base.PlayerAction(action, index);
                break;
        }
    }

    private IEnumerator BankSMS()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(WaitForReply(0));
        uIManager.bankMessage.SetActive(true);
    }
}
