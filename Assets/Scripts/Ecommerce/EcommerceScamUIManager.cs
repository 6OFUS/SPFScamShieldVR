using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EcommerceScamUIManager : UIManager
{
    public EcommerceScamManager scamManager;

    [Header("IPad")]
    public GameObject careToSellWebsite;
    public GameObject careToSellChat;
    public GameObject browzePlusSearch;
    public GameObject browzePlusUploadImage;
    public GameObject errorMessage;
    public Sprite proofImage;

    [Header("Phone")]
    public GameObject pickUpUI;
    public XRGrabInteractable phoneInteractable;

    public GameObject phoneHomeScreen;
    public GameObject aCTBankLogin;
    public GameObject aCTBankHome;
    public GameObject aCTBankTransfer;
    public GameObject aCTBankAmountInput;
    public GameObject aCTBankTransferSuccess;

    public void OpenCareToSell()
    {
        careToSellWebsite.SetActive(true);
        scamManager.isOn6OfUsWebsite = false;
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void StartChatting()
    {
        careToSellChat.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void CheckImageOnBrowzePlus()
    {
        browzePlusSearch.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void UploadImageOnBrowzePlus()
    {
        browzePlusUploadImage.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void ReturnToChat()
    {
        DisableAllCanvasChildren();
        careToSellChat.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void Purchase()
    {
        scamManager.messagingSystem.PlayerNextMessage("Okay. What's your ActNow?");
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void TurnOnPhone()
    {
        pickUpUI.SetActive(true);
        phoneInteractable.enabled = true;
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void UnlockPhone()
    {
        phoneHomeScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public IEnumerator LoginACTBankApp()
    {
        aCTBankLogin.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        aCTBankHome.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void ActNow()
    {
        aCTBankTransfer.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void EnterTransferDetails()
    {
        aCTBankAmountInput.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void TransferSuccess()
    {
        aCTBankTransferSuccess.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void ShareTransferredMessage()
    {
        scamManager.messagingSystem.PlayerNextMessage("Hi! I just sent S$70 to your mobile number via ActNow.");
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public IEnumerator HandleWinEnding()
    {
        scenarioController.scenarioCanvas.SetActive(false);
        winScreen.SetActive(true);
        audioSource.clip = winClip;
        audioSource.Play();
        scamManager.ClearChoices(scamManager.choiceContainer);
        Destroy(scamManager.scamshieldButton);
        yield return new WaitForSeconds(winClip.length);
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
}
