/*
    Author: Kevin Heng
    Date: 26/06/2025
    Description: The JobScamUIManager class is used to manage all UI and its functions related to the job scam scenario
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
    /// <summary>
    /// UI for the report section on Whatsup screen
    /// </summary>
    public GameObject whatsupReportUI;
    public GameObject whatsupHomeButton;
    //public GameObject whatsupReturnText;

    [Header("Amail UI")]
    public GameObject amailScreen;

    [Header("Scam website UI")]
    public GameObject websiteHomeScreen;
    public GameObject websiteCreateAccountScreen;
    public GameObject websiteHomeLoggedInScreen;
    //public GameObject websiteHomeLoggedInReturnText;
    public GameObject websiteHomeSilverTierButton;
    public GameObject websiteSelectTaskScreen;
    public GameObject websiteHomeAfterFirstTaskScreen;
    public GameObject websiteHomeAfterFirstTaskSilverTierButton;
    public GameObject withdrawButton;

    [Header("Scam website task UI")]
    public GameObject taskScreen;
    public List<GameObject> itemNumUI = new List<GameObject>();

    [Header("Loading screen UI")]
    public GameObject loadingScreen;
    //public GameObject loadingScreenReturnText;
    public GameObject loadingScreenHomeButton;
    public GameObject loadingBackToDashboardScreen;

    public override IEnumerator FlashEffect()
    {
        yield return base.FlashEffect();
        whatsupHomeButton.SetActive(true);
        //whatsupReturnText.SetActive(true);
        returnText.SetActive(true);
    }
}
