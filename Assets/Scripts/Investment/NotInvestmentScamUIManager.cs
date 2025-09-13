/*
    Author: Kevin Heng
    Date: 09/08/2025
    Description: The NotInvestmentScamUIManager class is used to manage all UI related functions to the non investment scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class NotInvestmentScamUIManager : UIManager
{
    /// <summary>
    /// Reference NotInvestmentScamManager script
    /// </summary>
    public NotInvestmentScamManager notScamManager;
    /// <summary>
    /// Reference InvestmentScenariosAudioManager script
    /// </summary>
    public InvestmentScenariosAudioManager audioManager;

    /// <summary>
    /// Kachagram chat screen
    /// </summary>
    [Header("Kachagram")]
    public GameObject kachagramScreen;
    /// <summary>
    /// Rechel's Kachagram profile picture
    /// </summary>
    public GameObject rachelKachagramPfp;

    /// <summary>
    /// Browze+ screen
    /// </summary>
    [Header("Browze+")]
    public GameObject browzePlusScreen;
    /// <summary>
    /// Browze+ searched screen
    /// </summary>
    public GameObject browzePlusSearchedScreen;

    /// <summary>
    /// MAS website home screen
    /// </summary>
    [Header("MAS Website")]
    public GameObject mASWebsiteScreen;
    /// <summary>
    /// Input representative number
    /// </summary>
    public GameObject representativeNumber;
    /// <summary>
    /// MAS website scroll rect
    /// </summary>
    public ScrollRect mASWebsiteScrollRect;
    /// <summary>
    /// MAS website auto scroll duration
    /// </summary>
    public float scrollDuration;
    /// <summary>
    /// Loading screen
    /// </summary>
    public GameObject mASLoadingScreen;
    /// <summary>
    /// Confirmed Rachel on MAS screen
    /// </summary>
    public GameObject mASRachelConfirmedScreen;

    /// <summary>
    /// Change UI to show Kachagram chat screen after selecting option to open phone notification
    /// </summary>
    public void TapNotification()
    {
        kachagramScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(0, audioManager));
    }

    /// <summary>
    /// Add Rechel's Kachgram profile picture to chat scroll view
    /// </summary>
    /// <param name="index"></param>
    public void AddProfilePicture(int index)
    {
        rachelKachagramPfp.transform.SetParent(scenarioController.messageContentParent);
        rachelKachagramPfp.transform.SetAsFirstSibling();
        string selectedText = notScamManager.playerChoices[index].choiceName;
        notScamManager.messagingSystem.PlayerNextMessage(selectedText);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Loading time for searching for Rachel on MAS
    /// </summary>
    /// <returns> Loading time </returns>
    public IEnumerator SearchRepresentative()
    {
        mASLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        mASRachelConfirmedScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Animation to check if Rachel is a scam or not
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckLegitimacy()
    {
        //OPEN BROWZE+
        browzePlusScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        //SEARCH BROWZE+
        browzePlusSearchedScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        //TAP FIRST LINK
        mASWebsiteScreen.SetActive(true);
        mASWebsiteScrollRect.normalizedPosition = new Vector2(0, 1);
        mASWebsiteScrollRect.DONormalizedPos(new Vector2(0, 0), scrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(scrollDuration);

        //ENTER REPRESENTATIVE NUMBER
        representativeNumber.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        //SEARCHED RESULT
        StartCoroutine(SearchRepresentative());

    }

    /// <summary>
    /// Return to Kachagram chat
    /// </summary>
    public void OpenKachagram()
    {
        DisableAllCanvasChildren();
        kachagramScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Change UI to win screen after selecting final option
    /// </summary>
    /// <returns></returns>
    public IEnumerator HandleWinEnding()
    {
        scenarioController.scenarioCanvas.SetActive(false);
        winScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.winClip);
        yield return new WaitForEndOfFrame();
        notScamManager.ClearChoices(notScamManager.choiceContainer);
        Destroy(notScamManager.scamshieldButton);
        whatHappenButton.onClick.AddListener(() =>
        {
            notScamManager.recapVideoScript.PlayVideo(notScamManager.whatHappenWinVideoClip);
        });
        notScamManager.ProceedToVideo(notScamManager.winVideoClip);
    }
}
