/*
    Author: Kevin Heng
    Date: 01/07/2025
    Description: The ProfessionalJobUIManager class is used to manage all UI related to the professional job ad scenario
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NotJobScamUIManager : UIManager
{
    public NotJobScamManager notScamManager;

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
        StartCoroutine(notScamManager.WaitAndContinueStory(0));
    }

    public void SendSticker(int index)
    {
        notScamManager.messagingSystem.PlayerSendSticker(stickers[index]);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime));
    }

    public void CheckWebsite()
    {
        websiteHomeScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(5));
    }
    public void CheckWebsiteCareersSection()
    {
        websiteCareersScreen.SetActive(true);
        StartCoroutine(notScamManager.SpawnHomeButton(this,5));
    }

    public void OpenAmail()
    {
        amailScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime));
    }

    public void OpenLuciaEmail()
    {
        luciaEmailScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(5));
    }

    public IEnumerator HandleLoseEnding()
    {
        notScamManager.ClearChoices();
        Destroy(notScamManager.scamshieldButton);
        scenarioController.scenarioCanvas.SetActive(false);
        audioSource.clip = loseClip;
        audioSource.Play();
        loseScreen.SetActive(true);
        yield return new WaitForSeconds(loseClip.length);
        whatHappenButton.gameObject.SetActive(false);
        whatShouldYouDoButton.transform.position = whatShouldYouDoButtonPos.position;
        notScamManager.ProceedToVideo(notScamManager.gameOverVideoClip);
    }

    public IEnumerator HandleWinEnding()
    {
        notScamManager.ClearChoices();
        Destroy(notScamManager.scamshieldButton);
        scenarioController.scenarioCanvas.SetActive(false);
        audioSource.clip = winClip;
        audioSource.Play();
        winScreen.SetActive(true);
        yield return new WaitForSeconds(winClip.length);
        whatHappenButton.onClick.AddListener(() =>
        {
            notScamManager.recapVideoScript.PlayVideo(notScamManager.whatHappenWinVideoClip);
        });
        notScamManager.ProceedToVideo(notScamManager.winVideoClip);
    }
}
