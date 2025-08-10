using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EcommerceScamManager : InkManager
{
    public EcommerceScamUIManager uIManager;

    public Transform phoneChoiceContainer;

    public bool isMoneyTransferred;
    public bool isOn6OfUsWebsite;

    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_open_caretosell", _ => uIManager.OpenCareToSell()},
            { "action_chat_with_seller", _ => uIManager.StartChatting()},
            { "action_check_image", _ => uIManager.CheckImageOnBrowzePlus()},
            { "action_upload_image", _ => uIManager.UploadImageOnBrowzePlus()},
            { "action_return_to_chat", _ => uIManager.ReturnToChat()},
            { "action_purchase", _ => uIManager.Purchase()},
            { "action_turn_on_phone", _ => uIManager.TurnOnPhone()},
            { "action_phone_unlock", _ => uIManager.UnlockPhone()},
            { "action_phone_open_actbank", _ => StartCoroutine(uIManager.LoginACTBankApp())},
            { "action_phone_actnow", _ => uIManager.ActNow()},
            { "action_phone_enter_details", _ => uIManager.EnterTransferDetails()},
            { "action_phone_transfer", _ => uIManager.TransferSuccess()},
            { "action_phone_share", _ => uIManager.ShareTransferredMessage()},
            { "win_ending", _ => StartCoroutine(uIManager.HandleWinEnding())},
            { "lose_ending", _ => StartCoroutine(uIManager.HandleLoseEnding())},

        };
    }

    public override void DisplayChoices()
    {
        for (int i = 0; i < playerChoices.Count; i++)
        {
            var choice = playerChoices[i];
            
            if (choice.choiceAction.Contains("action_phone"))
            {
                CreateChoiceButton(actionChoiceButtonPrefab, phoneChoiceContainer, choice.choiceName, i, phoneChoiceContainer);
            }
            else if (choice.choiceAction.Contains("message") || choice.choiceAction.Contains("ending"))
            {
                CreateChoiceButton(dialogueChoiceButtonPrefab, choiceContainer, choice.choiceName, i, choiceContainer);
            }
            else if (choice.choiceAction.Contains("action"))
            {
                CreateChoiceButton(actionChoiceButtonPrefab, choiceContainer, choice.choiceName, i, choiceContainer);
            }
        }
        if (scamshieldButton == null && !isOnHomeScreen && !isOn6OfUsWebsite)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot(this);
                ClearChoices(choiceContainer);
                Destroy(scamshieldButton);
                StartCoroutine(SpawnHomeButton(uIManager, 1));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }

    protected override void Report()
    {
        if (!isMoneyTransferred)
        {
            StartCoroutine(ReportToScamShield(uIManager, uIManager.winClip, uIManager.winScreen, whatHappenWinVideoClip, winVideoClip));
        }
        else
        {
            StartCoroutine(ReportToScamShield(uIManager, uIManager.loseClip, uIManager.reportAfterScammedScreen, whatHappenLoseVideoClip, gameOverVideoClip));
        }
    }

    public override void SenderAction(string action, string dialogue)
    {
        switch (action)
        {
            case "image":
                StartCoroutine(SendImage());
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
}
