/*
    Author: Kevin Heng
    Date: 11/08/2025
    Description: The NotEcommerceScamUIManager class is used to manage all UI related functions to the non ecommerce scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NotEcommerceScamUIManager : UIManager
{
    public NotEcommerceScamManager notScamManager;
    public EcommerceScenariosAudioManager audioManager;

    [Header("IPad")]
    public GameObject careToSellWebsite;
    public GameObject careToSellChat;
    public GameObject browzePlusSearch;
    public GameObject browzePlusUploadImage;

    public Sprite proofImage;
    public GameObject senderVideoPrefab;

    public void OpenCareToSell()
    {
        careToSellWebsite.SetActive(true);
        notScamManager.isOn6OfUsWebsite = false;
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void StartChatting()
    {
        careToSellChat.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public IEnumerator CheckImageOnBrowzePlus()
    {
        browzePlusSearch.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        browzePlusUploadImage.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void ReturnToChat()
    {
        DisableAllCanvasChildren();
        careToSellChat.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void Purchase()
    {
        notScamManager.messagingSystem.PlayerNextMessage("Okay. What's your ActNow?");
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void MoneyTransferred()
    {
        notScamManager.messagingSystem.PlayerNextMessage("Okay, I have just transferred the money!");
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }
    
    public IEnumerator HandleWinEnding()
    {
        scenarioController.scenarioCanvas.SetActive(false);
        winScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.winClip);
        notScamManager.ClearChoices(notScamManager.choiceContainer);
        Destroy(notScamManager.scamshieldButton);
        yield return new WaitForEndOfFrame();
        whatHappenButton.onClick.AddListener(() =>
        {
            notScamManager.recapVideoScript.PlayVideo(notScamManager.whatHappenWinVideoClip);
        });
        notScamManager.ProceedToVideo(notScamManager.winVideoClip);
    }

    public IEnumerator HandleLoseEnding()
    {
        scenarioController.scenarioCanvas.SetActive(false);
        loseScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.loseClip);
        notScamManager.ClearChoices(notScamManager.choiceContainer);
        Destroy(notScamManager.scamshieldButton);
        yield return new WaitForEndOfFrame();
        whatHappenButton.gameObject.SetActive(false);
        whatShouldYouDoButton.transform.position = whatShouldYouDoButtonPos.position;
        whatHappenButton.onClick.AddListener(() =>
        {
            notScamManager.recapVideoScript.PlayVideo(notScamManager.whatHappenLoseVideoClip);
        });
        notScamManager.ProceedToVideo(notScamManager.gameOverVideoClip);
    }
}
