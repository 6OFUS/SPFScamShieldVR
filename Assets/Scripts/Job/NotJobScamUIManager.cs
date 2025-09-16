/*
    Author: Kevin Heng
    Date: 01/07/2025
    Description: The ProfessionalJobUIManager class is used to manage all UI related functions to the professional job ad scenario
*/
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NotJobScamUIManager : UIManager
{
    public NotJobScamManager notScamManager;
    public JobScenariosAudioManager audioManager;

    public ScrollRect websiteScrollRect;
    public ScrollRect websiteCareersScrollRect;
    public ScrollRect luciaEmailScrollRect;
    public float websiteScrollDuration;
    public float luciaEmailScrollDuration;

    [Header("Stickers")]
    public Sprite[] stickers;
    public GameObject stickerChoicePrefab;

    [Header("Phone screens")]
    public GameObject whatsupScreen;
    public GameObject websiteHomeScreen;
    public GameObject amailScreen;
    public GameObject luciaEmailScreen;
    public GameObject websiteCareersScreen;

    public void TapNotification()
    {
        whatsupScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(0, audioManager));
    }

    public void SendSticker(int index)
    {
        notScamManager.messagingSystem.PlayerSendSticker(stickers[index]);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public IEnumerator CheckWebsite()
    {
        websiteHomeScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        websiteScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteScrollRect.DONormalizedPos(new Vector2(0, 0), websiteScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(websiteScrollDuration);

        websiteCareersScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        websiteCareersScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteCareersScrollRect.DONormalizedPos(new Vector2(0, 0), websiteScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(websiteScrollDuration);

        StartCoroutine(notScamManager.WaitAndContinueStory(0, audioManager));
    }


    public IEnumerator OpenAmail()
    {
        amailScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        luciaEmailScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        websiteCareersScrollRect.normalizedPosition = new Vector2(0, 1);
        websiteCareersScrollRect.DONormalizedPos(new Vector2(0, 0), luciaEmailScrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(luciaEmailScrollDuration);
        StartCoroutine(notScamManager.WaitAndContinueStory(0, audioManager));
    }


    public void ReturnToChat()
    {
        DisableAllCanvasChildren();
        whatsupScreen.SetActive(true);
        notScamManager.isOnHomeScreen = false;
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public IEnumerator HandleLoseEnding()
    {
        notScamManager.ClearChoices(notScamManager.choiceContainer);
        Destroy(notScamManager.scamshieldButton);
        scenarioController.scenarioCanvas.SetActive(false);
        audioManager.PlayAudio(audioManager.loseClip);
        loseScreen.SetActive(true);
        yield return new WaitForEndOfFrame();
        whatHappenButton.gameObject.SetActive(false);
        whatShouldYouDoButton.transform.position = whatShouldYouDoButtonPos.position;
        notScamManager.ProceedToVideo(notScamManager.gameOverVideoClip);
    }

    public IEnumerator HandleWinEnding()
    {
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
