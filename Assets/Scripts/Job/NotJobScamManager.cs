/*
    Author: Kevin Heng
    Date: 01/07/2025
    Description: The ProfessionalJobManager class is used to handle all the dialogue choice options related to the professional job ad scenario
*/
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NotJobScamManager : InkManager
{  
    public NotJobScamUIManager uIManager;
    public JobScenariosAudioManager audioManager;
    private void Awake()
    {
        actionHandlers = new Dictionary<string, Action<int>>
        {
            { "action_tap_notification", _ => uIManager.TapNotification()},
            { "sticker", index => uIManager.SendSticker(index)},
            { "action_check_website", _ => StartCoroutine(uIManager.CheckWebsite())},
            { "action_return_to_chat", _ => uIManager.ReturnToChat()},
            { "action_open_amail", _ => StartCoroutine(uIManager.OpenAmail())},
            { "win_ending", _ => StartCoroutine(uIManager.HandleWinEnding())},
            { "lose_ending", _ => StartCoroutine(uIManager.HandleLoseEnding())},

        };
    }
    public override void DisplayChoices(AudioManager manager)
    {
        //IMAGE OPTIONS HERE
        foreach (var choice in playerChoices)
        {
            if (choice.choiceAction == "sticker")
            {
                //choiceContainer.GetComponent<VerticalLayoutGroup>().spacing = 
                GameObject buttonObj = Instantiate(uIManager.stickerChoicePrefab, choiceContainer);

                // Capture the correct index in a local variable to avoid closure issue
                int capturedIndex = playerChoices.IndexOf(choice);
                Image image = buttonObj.GetComponent<Image>();
                image.sprite = uIManager.stickers[capturedIndex];
                buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                    ChooseOption(capturedIndex, audioManager);
                    ClearChoices(choiceContainer);
                });
            }
        }
        base.DisplayChoices(manager);
        if (scamshieldButton == null && !isOnHomeScreen)
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
}
