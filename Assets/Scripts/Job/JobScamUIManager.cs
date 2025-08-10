/*
    Author: Kevin Heng
    Date: 26/06/2025
    Description: The JobScamUIManager class is used to manage all UI related functions to the job scam scenario
*/
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobScamUIManager : UIManager
{
    public GameObject ignoreOfferScreen;

    public JobScamManager scamManager;

    [Header("WhatsUp UI")]
    public GameObject whatsupScreen;

    [Header("Amail UI")]
    public GameObject amailScreen;
    public GameObject jasonEmailScreen;

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

    public void TapNotification()
    {
        whatsupScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(0));
    }

    public void OpenAmail()
    {
        amailScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void OpenJasonEmail()
    {
        jasonEmailScreen.SetActive(true);
        StartCoroutine(scamManager.SpawnHomeButton(this, 5));
    }

    public void MessageAndRegisterAccount(int index)
    {
        scamManager.messagingSystem.PlayerNextMessage(scamManager.playerChoices[index].choiceName);
        websiteHomeScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void CreateAccount()
    {
        websiteCreateAccountScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void EnterAccountDetails()
    {
        for (int i = 0; i < detailsInputText.Length; i++)
        {
            detailsInputText[i].color = Color.black;
            detailsInputText[i].text = detailsTextContent[i];
        }
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public IEnumerator CreatingAccount()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        websiteHomeLoggedInScreen.SetActive(true);
        StartCoroutine(scamManager.SpawnHomeButton(this, 1));
    }

    public void MessageAndCompleteFirstTask(int index)
    {
        scamManager.messagingSystem.PlayerNextMessage(scamManager.playerChoices[index].choiceName);
        whatsupScreen.SetActive(false);
        websiteHomeLoggedInScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void SelectSilverTier()
    {
        websiteSelectTaskScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public IEnumerator HandleFirstTaskGroupSelection()
    {
        websiteHomeLoggedInScreen.SetActive(false);
        websiteSelectTaskScreen.SetActive(false);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        loadingScreen.SetActive(false);
        taskScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void AddItemsToCart()
    {
        itemNumThree.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public IEnumerator HandleCheckOut()
    {
        taskScreen.SetActive(false);
        loadingBackToDashboardScreen.SetActive(true);
        scamManager.firstTaskCompleted = true;
        websiteFirstTaskGroup.SetActive(false);
        //CHECK OUT AUDIO PUT HERE
        yield return new WaitForSeconds(loadingTime);
        websiteHomeAfterFirstTaskScreen.SetActive(true);
        StartCoroutine(scamManager.SpawnHomeButton(this, 1));
    }

    public void MessageAndCompleteSecondTask(int index)
    {
        websiteHomeAfterFirstTaskScreen.SetActive(true);
        scamManager.messagingSystem.PlayerNextMessage(scamManager.playerChoices[index].choiceName);
        whatsupScreen.SetActive(false);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public IEnumerator HandleSecondTaskGroupSelection()
    {
        websiteHomeAfterFirstTaskScreen.SetActive(false);
        websiteSelectTaskScreen.SetActive(false);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime * 2);
        StartCoroutine(scamManager.SpawnHomeButton(this, 0));
    }

    public void MessageAndWithdraw(int index)
    {
        websiteHomeAfterFirstTaskScreen.SetActive(true);
        scamManager.messagingSystem.PlayerNextMessage(scamManager.playerChoices[index].choiceName);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public IEnumerator Withdraw()
    {
        websiteHomeAfterFirstTaskScreen.SetActive(false);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        loadingScreen.SetActive(false);
        websiteWithdrawErrorScreen.SetActive(true);
        audioSource.clip = scamManager.errorClip;
        audioSource.Play();
        StartCoroutine(scamManager.SpawnHomeButton(this, 1));
    }

    public IEnumerator HandleLoseEnding()
    {
        audioSource.clip = cryingClip;
        audioSource.Play();
        yield return new WaitForSeconds(cryingClip.length);
        scenarioController.scenarioCanvas.SetActive(false);
        loseScreen.SetActive(true);
        audioSource.clip = loseClip;
        audioSource.Play();
        scamManager.ClearChoices(scamManager.choiceContainer);
        Destroy(scamManager.scamshieldButton);
        yield return new WaitForSeconds(loseClip.length);
        whatHappenButton.onClick.AddListener(() =>
        {
            scamManager.recapVideoScript.PlayVideo(scamManager.whatHappenLoseVideoClip);
        });
        scamManager.ProceedToVideo(scamManager.gameOverVideoClip);
    }

    public IEnumerator HandleIgnoreOfferEnding()
    {
        scenarioController.scenarioCanvas.SetActive(false);
        audioSource.clip = winClip;
        audioSource.Play();
        ignoreOfferScreen.SetActive(true);
        scamManager.ClearChoices(scamManager.choiceContainer);
        Destroy(scamManager.scamshieldButton);
        yield return new WaitForSeconds(winClip.length);
        whatHappenButton.onClick.AddListener(() =>
        {
            scamManager.recapVideoScript.PlayVideo(scamManager.whatHappenWinVideoClip);
        });
        scamManager.ProceedToVideo(scamManager.winVideoClip);
    }
}
