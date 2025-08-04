/*
    Author: Kevin Heng
    Date: 26/06/2025
    Description: The JobScamUIManager class is used to manage all UI related to the job scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobScamUIManager : UIManager
{
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject ignoreOfferScreen;
    public GameObject reportAfterScammedScreen;

    [Header("WhatsUp UI")]
    public GameObject whatsupScreen;

    [Header("Amail UI")]
    public GameObject amailScreen;
    public GameObject jasonEmailScreen;

    [Header("Home UI")]
    public GameObject homeScreen;

    [Header("Scam website UI")]
    public GameObject websiteHomeScreen;
    public GameObject websiteCreateAccountScreen;

    public TextMeshProUGUI[] detailsInputText;
    public string[] detailsTextContent;

    public GameObject websiteHomeLoggedInScreen;

    public GameObject websiteSelectTaskScreen;

    public GameObject websiteHomeAfterFirstTaskScreen;
    
    public GameObject websiteFirstTaskGroup;

    public GameObject websiteWithdrawErrorScreen;

    [Header("Scam website task UI")]
    public GameObject taskScreen;
    public GameObject itemNumThree;

    [Header("Loading screen UI")]
    public GameObject loadingScreen;
    public GameObject loadingBackToDashboardScreen;

    [Header("Images")]
    public Sprite scamPayoutImage;


    public override IEnumerator FlashEffect()
    {
        yield return base.FlashEffect();
    }
}
