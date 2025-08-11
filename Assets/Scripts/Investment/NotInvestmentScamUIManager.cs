/*
    Author: Kevin Heng
    Date: 09/08/2025
    Description: The NotInvestmentScamUIManager class is used to manage all UI related functions to the non investment scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotInvestmentScamUIManager : UIManager
{
    public NotInvestmentScamManager notScamManager;
    public InvestmentScenariosAudioManager audioManager;

    public GameObject kachagramScreen;
    public GameObject rachelKachagramPfp;

    public GameObject browzePlusScreen;
    public GameObject browzePlusSearchedScreen;
    public GameObject mASWebsiteScreen;
    public GameObject representativeNumber;
    public GameObject mASLoadingScreen;
    public GameObject mASRachelConfirmedScreen;

    public void TapNotification()
    {
        kachagramScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(0, audioManager));
    }

    public void AddProfilePicture(int index)
    {
        rachelKachagramPfp.transform.SetParent(scenarioController.messageContentParent);
        rachelKachagramPfp.transform.SetAsFirstSibling();
        string selectedText = notScamManager.playerChoices[index].choiceName;
        notScamManager.messagingSystem.PlayerNextMessage(selectedText);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void HomeScreen()
    {
        notScamManager.isOnHomeScreen = true;
        DisableAllCanvasChildren();
        homeScreen.SetActive(true);

        if (notScamManager.scamshieldButton != null)
        {
            Destroy(notScamManager.scamshieldButton);
        }
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void OpenBrowzePlus()
    {
        notScamManager.isOnHomeScreen = false;
        browzePlusScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void SearchBrowzePlus()
    {
        browzePlusSearchedScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void TapFirstLink()
    {
        mASWebsiteScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void EnterRepresentativeNumber()
    {
        representativeNumber.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public IEnumerator SearchRepresentative()
    {
        mASLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        mASRachelConfirmedScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void OpenKachagram()
    {
        DisableAllCanvasChildren();
        kachagramScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

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
