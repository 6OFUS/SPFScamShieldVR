/*
    Author: Kevin Heng
    Date: 11/08/2025
    Description: The NotEcommerceScamManager class is used to handle all the dialogue choice options related to the non ecommerce scam scenario
*/
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NotEcommerceScamManager : InkManager
{
    public NotEcommerceScamUIManager uIManager;
    public EcommerceScenariosAudioManager audioManager;

    public Transform phoneChoiceContainer;

    public bool isMoneyTransferred;
    public bool isOn6OfUsWebsite;

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_open_caretosell", _ => uIManager.OpenCareToSell()},
            { "action_chat_with_seller", _ => uIManager.StartChatting()},
            { "action_check_image", _ => StartCoroutine(uIManager.CheckImageOnBrowzePlus())}, 
            { "action_return_to_chat", _ => uIManager.ReturnToChat()},
            { "action_purchase", _ => uIManager.Purchase()},
            { "action_money_transferred", _ => uIManager.MoneyTransferred()}, 
            { "action_send_address", _ => StartCoroutine(uIManager.HandleWinEnding())},
            { "lose_ending", _ => StartCoroutine(uIManager.HandleLoseEnding())},

        };
    }

    public override void DisplayChoices(AudioManager manager)
    {
        for (int i = 0; i < playerChoices.Count; i++)
        {
            var choice = playerChoices[i];
            
            if (choice.choiceAction.Contains("action_phone"))
            {
                CreateChoiceButton(actionChoiceButtonPrefab, phoneChoiceContainer, choice.choiceName, i, phoneChoiceContainer, audioManager);
            }
            else if (choice.choiceAction.Contains("message") || choice.choiceAction.Contains("ending"))
            {
                CreateChoiceButton(dialogueChoiceButtonPrefab, choiceContainer, choice.choiceName, i, choiceContainer, audioManager);
            }
            else if (choice.choiceAction.Contains("action"))
            {
                CreateChoiceButton(actionChoiceButtonPrefab, choiceContainer, choice.choiceName, i, choiceContainer, audioManager);
            }
        }
        if (scamshieldButton == null && !isOnHomeScreen && !isOn6OfUsWebsite)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot(this, audioManager);
                ClearChoices(choiceContainer);
                Destroy(scamshieldButton);
                StartCoroutine(SpawnOpenScamshieldButton(uIManager, audioManager));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }

    protected override void Report()
    {
        StartCoroutine(ReportToScamShield(uIManager, audioManager.loseClip, uIManager.loseScreen, whatHappenLoseVideoClip, gameOverVideoClip, audioManager));
        uIManager.whatHappenButton.gameObject.SetActive(false);
        uIManager.whatShouldYouDoButton.transform.position = uIManager.whatShouldYouDoButtonPos.position;
    }

    public override void SenderAction(string action, string dialogue)
    {
        switch (action)
        {
            case "image":
                StartCoroutine(SendImage());
                break;
            case "video":
                StartCoroutine(SendVideo());
                break;
            default:
                base.SenderAction(action, dialogue);
                break;
        }
    }

    private IEnumerator SendImage()
    {
        yield return new WaitForSeconds(messageTime);
        messagingSystem.SenderImage(uIManager.proofImage);
    }

    private IEnumerator SendVideo()
    {
        yield return new WaitForSeconds(messageTime);
        GameObject newMessage = Instantiate(uIManager.senderVideoPrefab, uIManager.scenarioController.messageContentParent);

        messagingSystem.audioSource.clip = messagingSystem.messageReceived;
        messagingSystem.audioSource.Play();
    }
}
