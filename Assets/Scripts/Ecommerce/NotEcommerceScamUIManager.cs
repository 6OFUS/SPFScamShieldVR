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

    [Header("Phone")]
    public GameObject pickUpUI;
    public XRGrabInteractable phoneInteractable;

    public GameObject phoneCanvas;
    public GameObject phoneHomeScreen;
    public GameObject aCTBankLogin;
    public GameObject aCTBankHome;
    public GameObject aCTBankTransfer;
    public GameObject aCTBankAmountInput;
    public GameObject aCTBankTransferSuccess;

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

    public void CheckImageOnBrowzePlus()
    {
        browzePlusSearch.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void UploadImageOnBrowzePlus()
    {
        browzePlusUploadImage.SetActive(true);
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

    public void TurnOnPhone()
    {
        pickUpUI.SetActive(true);
        phoneInteractable.enabled = true;
        phoneInteractable.selectEntered.AddListener((SelectEnterEventArgs args) =>
        {
            phoneCanvas.SetActive(true);
        });
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void UnlockPhone()
    {
        phoneHomeScreen.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public IEnumerator LoginACTBankApp()
    {
        aCTBankLogin.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        aCTBankHome.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void ActNow()
    {
        aCTBankTransfer.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void EnterTransferDetails()
    {
        aCTBankAmountInput.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void TransferSuccess()
    {
        aCTBankTransferSuccess.SetActive(true);
        StartCoroutine(notScamManager.WaitAndContinueStory(notScamManager.messageTime, audioManager));
    }

    public void ShareTransferredMessage()
    {
        notScamManager.messagingSystem.PlayerNextMessage("Hi! I just sent S$85 to your mobile number via ActNow.");
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
