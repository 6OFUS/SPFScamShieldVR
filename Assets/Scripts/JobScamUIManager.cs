/*
    Author: Kevin Heng
    Date: 26/06/2025
    Description: The JobScamUIManager class is used to manage all UI and its functions related to the job scam scenarios
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobScamUIManager : UIManager
{
    [Header("UI")]
    /// <summary>
    /// UI for the report section on Whatsup screen
    /// </summary>
    public GameObject whatsupReportUI;
    public GameObject whatsupHomeButton;
    public GameObject whatsupReturnText;
    public GameObject amailUI;
    public GameObject websiteHomeUI;
    public GameObject loadingScreenUI;
    public GameObject websiteHomeLoggedInUI;

    public override IEnumerator FlashEffect()
    {
        yield return base.FlashEffect();
        whatsupHomeButton.SetActive(true);
        whatsupReturnText.SetActive(true);
    }
}
