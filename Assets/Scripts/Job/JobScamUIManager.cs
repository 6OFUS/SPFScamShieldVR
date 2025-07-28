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
    [Header("WhatsUp UI")]
    public GameObject whatsupScreen;

    [Header("Amail UI")]
    public GameObject amailScreen;

    [Header("Home UI")]
    public GameObject homeScreen;

    [Header("Scam website UI")]
    public GameObject websiteHomeScreen;
    public GameObject websiteCreateAccountScreen;
    public GameObject websiteHomeLoggedInScreen;
    public GameObject websiteHomeSilverTierButton;
    public GameObject websiteSelectTaskScreen;
    public GameObject websiteHomeAfterFirstTaskScreen;
    public GameObject websiteHomeAfterFirstTaskSilverTierButton;
    public GameObject withdrawButton;
    public GameObject websiteWithdrawErrorScreen;

    [Header("Scam website task UI")]
    public GameObject taskScreen;
    public List<GameObject> itemNumUI = new List<GameObject>();

    [Header("Loading screen UI")]
    public GameObject loadingScreen;
    public GameObject loadingScreenHomeButton;
    public GameObject loadingBackToDashboardScreen;

    [Header("Images")]
    public Sprite scamPayoutImage;

    [Header("End screen UI")]
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject ignoreOfferScreen;
    public GameObject reportAfterScammedScreen;

    public override IEnumerator FlashEffect()
    {
        yield return base.FlashEffect();
    }

    private void Start()
    {

    }
}
