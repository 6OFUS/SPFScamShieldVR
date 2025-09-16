/*
    Author: Kevin Heng
    Date: 10/08/2025
    Description: The EcommerceScamUIManager class is used to manage all UI related functions to the ecommerce scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EcommerceScamUIManager : UIManager
{
    public EcommerceScamManager scamManager;
    public EcommerceScenariosAudioManager audioManager;

    [Header("IPad")]
    public GameObject careToSellWebsite;
    public GameObject careToSellChat;
    public GameObject browzePlusSearch;
    public GameObject browzePlusUploadImage;
    public GameObject errorMessage;
    public Sprite proofImage;


    public void OpenCareToSell()
    {
        careToSellWebsite.SetActive(true);
        scamManager.isOn6OfUsWebsite = false;
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void StartChatting()
    {
        careToSellChat.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public IEnumerator CheckImageOnBrowzePlus()
    {
        browzePlusSearch.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        browzePlusUploadImage.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void ReturnToChat()
    {
        DisableAllCanvasChildren();
        careToSellChat.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public void Purchase()
    {
        scamManager.messagingSystem.PlayerNextMessage("Okay. What's your ActNow?");
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /*

    public void ShareTransferredMessage()
    {
        scamManager.messagingSystem.PlayerNextMessage("Hi! I just sent S$70 to your mobile number via ActNow.");
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }
    */

    public void MoneyTransferred()
    {
        scamManager.messagingSystem.PlayerNextMessage("Hi! I just sent S$70 to your mobile number via ActNow.");
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    public IEnumerator HandleWinEnding()
    {
        scenarioController.scenarioCanvas.SetActive(false);
        winScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.winClip);
        scamManager.ClearChoices(scamManager.choiceContainer);
        Destroy(scamManager.scamshieldButton);
        yield return new WaitForEndOfFrame();
        whatHappenButton.onClick.AddListener(() =>
        {
            scamManager.recapVideoScript.PlayVideo(scamManager.whatHappenWinVideoClip);
        });
        scamManager.ProceedToVideo(scamManager.winVideoClip);
    }

    public IEnumerator HandleLoseEnding()
    {
        errorMessage.SetActive(true);
        errorMessage.transform.SetAsLastSibling();
        audioManager.PlayAudio(audioManager.cryingClip);
        yield return new WaitForSeconds(2);
        scenarioController.scenarioCanvas.SetActive(false);
        loseScreen.SetActive(true);
        audioManager.audioSource.clip = audioManager.loseClip;
        audioManager.audioSource.Play();
        scamManager.ClearChoices(scamManager.choiceContainer);
        Destroy(scamManager.scamshieldButton);
        yield return new WaitForEndOfFrame();
        whatHappenButton.onClick.AddListener(() =>
        {
            scamManager.recapVideoScript.PlayVideo(scamManager.whatHappenLoseVideoClip);
        });
        scamManager.ProceedToVideo(scamManager.gameOverVideoClip);
    }
}
