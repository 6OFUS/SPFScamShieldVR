/*
    Author: Kevin Heng
    Date: 26/06/2025
    Description: The JobScamUIManager class is used to manage all UI related functions to the job scam scenario
*/
using DG.Tweening;
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
    public JobScenariosAudioManager audioManager;

    [Header("WhatsUp UI")]
    public GameObject whatsupScreen;

    [Header("Amail UI")]
    public GameObject amailScreen;
    public GameObject jasonEmailScreen;
    public ScrollRect emailScrollRect;
    public float emailScrollDuration;

    [Header("Scam website UI")]
    public GameObject websiteHomeScreen;
    public ScrollRect websiteScrollRect;
    public float websiteScrollDuration;
    public GameObject websiteCreateAccountScreen;

    public TextMeshProUGUI[] detailsInputText;
    public string[] detailsTextContent;

    public GameObject websiteHomeLoggedInScreen;
    public ScrollRect websiteHomeLoggedInScrollRect;
    public float websiteHomeLoggedInScrollDuration;

    public GameObject websiteSelectTaskScreen;
    public ScrollRect websiteSelectTaskScrollRect;
    public float websiteSelectTaskScrollDuration;

    public GameObject websiteHomeAfterFirstTaskScreen;
    public ScrollRect websiteHomeAfterFirstTaskScrollRect;
    public float websiteHomeAfterFirstTaskScrollDuration;

    public GameObject websiteFirstTaskGroup;

    public GameObject websiteWithdrawErrorScreen;

    [Header("Scam website task UI")]
    public GameObject taskScreen;
    public ScrollRect websiteFirstTaskScrollRect;
    public float websiteFirstTaskScrollDuration;

    public GameObject itemNumThree;

    [Header("Loading screen UI")]
    public GameObject loadingScreen;
    public GameObject loadingBackToDashboardScreen;

    [Header("Images")]
    public Sprite scamPayoutImage;

    public void TapNotification()
    {
        whatsupScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));
    }

    public IEnumerator OpenAmail()
    {
        amailScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        jasonEmailScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        emailScrollRect.normalizedPosition = new Vector2(0, 1);
        emailScrollRect.DONormalizedPos(new Vector2(0, 0), emailScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(emailScrollDuration);

        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));

    }


    public IEnumerator MessageAndRegisterAccount(int index)
    {
        scamManager.messagingSystem.PlayerNextMessage(scamManager.playerChoices[index].choiceName);
        websiteHomeScreen.SetActive(true);
        websiteScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteScrollRect.DONormalizedPos(new Vector2(0, 0), websiteScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(websiteScrollDuration);
        websiteCreateAccountScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        for (int i = 0; i < detailsInputText.Length; i++)
        {
            detailsInputText[i].color = Color.black;
            detailsInputText[i].text = detailsTextContent[i];
        }
        yield return new WaitForSeconds(loadingTime);
        StartCoroutine(CreatingAccount());
    }

    public IEnumerator CreatingAccount()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        websiteHomeLoggedInScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(1, audioManager));
    }


    public IEnumerator MessageAndCompleteFirstTask(int index)
    {
        scamManager.messagingSystem.PlayerNextMessage(scamManager.playerChoices[index].choiceName);
        whatsupScreen.SetActive(false);
        websiteHomeLoggedInScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        websiteHomeLoggedInScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteHomeLoggedInScrollRect.DONormalizedPos(new Vector2(0, 0), websiteHomeLoggedInScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(websiteHomeLoggedInScrollDuration);

        websiteSelectTaskScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        websiteSelectTaskScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteSelectTaskScrollRect.DONormalizedPos(new Vector2(0, 0), websiteSelectTaskScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(websiteSelectTaskScrollDuration);

        yield return StartCoroutine(HandleFirstTaskGroupSelection());

        yield return new WaitForSeconds(loadingTime);
        websiteFirstTaskScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteFirstTaskScrollRect.DONormalizedPos(new Vector2(0, 0), websiteFirstTaskScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(websiteFirstTaskScrollDuration);
        itemNumThree.SetActive(true);

        StartCoroutine(HandleCheckOut());
    }


    public IEnumerator HandleFirstTaskGroupSelection()
    {
        websiteHomeLoggedInScreen.SetActive(false);
        websiteSelectTaskScreen.SetActive(false);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        loadingScreen.SetActive(false);
        taskScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public IEnumerator HandleCheckOut()
    {
        yield return new WaitForSeconds(loadingTime);
        taskScreen.SetActive(false);
        loadingBackToDashboardScreen.SetActive(true);
        scamManager.firstTaskCompleted = true;
        websiteFirstTaskGroup.SetActive(false);
        yield return new WaitForSeconds(loadingTime);
        audioManager.PlayAudio(audioManager.checkOutClip);
        websiteHomeAfterFirstTaskScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(1, audioManager));
    }

    public IEnumerator MessageAndCompleteSecondTask(int index)
    {
        websiteHomeAfterFirstTaskScreen.SetActive(true);
        scamManager.messagingSystem.PlayerNextMessage(scamManager.playerChoices[index].choiceName);
        whatsupScreen.SetActive(false);

        websiteHomeAfterFirstTaskScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteHomeAfterFirstTaskScrollRect.DONormalizedPos(new Vector2(0, 0), websiteHomeAfterFirstTaskScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(websiteHomeAfterFirstTaskScrollDuration);

        websiteHomeAfterFirstTaskScreen.SetActive(false);
        websiteSelectTaskScreen.SetActive(true);
        websiteSelectTaskScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteSelectTaskScrollRect.DONormalizedPos(new Vector2(0, 0), websiteSelectTaskScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(websiteSelectTaskScrollDuration);

        StartCoroutine(HandleSecondTaskGroupSelection());

    }

    public IEnumerator HandleSecondTaskGroupSelection()
    {
        websiteHomeAfterFirstTaskScreen.SetActive(false);
        websiteSelectTaskScreen.SetActive(false);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime * 2);
        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));
    }

    public void MessageAndWithdraw(int index)
    {
        websiteHomeAfterFirstTaskScreen.SetActive(true);
        scamManager.messagingSystem.PlayerNextMessage(scamManager.playerChoices[index].choiceName);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public IEnumerator Withdraw()
    {
        websiteHomeAfterFirstTaskScreen.SetActive(false);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        loadingScreen.SetActive(false);
        websiteWithdrawErrorScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.errorClip);
        StartCoroutine(scamManager.WaitAndContinueStory(1, audioManager));
    }

    public IEnumerator HandleLoseEnding()
    {
        audioManager.PlayAudio(audioManager.cryingClip);
        yield return new WaitForSeconds(2);
        scenarioController.scenarioCanvas.SetActive(false);
        loseScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.loseClip);
        scamManager.ClearChoices(scamManager.choiceContainer);
        Destroy(scamManager.scamshieldButton);
        yield return new WaitForEndOfFrame();
        whatHappenButton.onClick.AddListener(() =>
        {
            scamManager.recapVideoScript.PlayVideo(scamManager.whatHappenLoseVideoClip);
        });
        scamManager.ProceedToVideo(scamManager.gameOverVideoClip);
    }

    public IEnumerator HandleIgnoreOfferEnding()
    {
        scenarioController.scenarioCanvas.SetActive(false);
        audioManager.PlayAudio(audioManager.winClip);
        ignoreOfferScreen.SetActive(true);
        scamManager.ClearChoices(scamManager.choiceContainer);
        Destroy(scamManager.scamshieldButton);
        yield return new WaitForEndOfFrame();
        whatHappenButton.onClick.AddListener(() =>
        {
            scamManager.recapVideoScript.PlayVideo(scamManager.whatHappenWinVideoClip);
        });
        scamManager.ProceedToVideo(scamManager.winVideoClip);
    }

    public void OpenWhatsUp()
    {
        DisableAllCanvasChildren();
        whatsupScreen.SetActive(true);
        scamManager.isOnHomeScreen = false;
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }
}
