/*
    Author: Kevin Heng
    Date: 01/07/2025
    Description: The ProfessionalJobManager class is used to handle all the functions related to the professional job ad scenario
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ProfessionalJobManager : InkManager
{
    public ProfessionalJobUIManager uIManager;

    public VideoClip loseVideoClip;
    public VideoClip winVideoClip;

    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "sticker":
                messagingSystem.PlayerSendSticker(uIManager.stickers[index]);
                StartCoroutine(WaitForReply());
                break;
            case "check_website":
                uIManager.websiteHomeScreen.SetActive(true);
                knotName = "job_verification_dialogue_1";
                break;
            case "open_amail":
                uIManager.amailScreen.SetActive(true);
                knotName = "return_from_email";
                break;
            case "win_ending":
                uIManager.audioSource.clip = uIManager.winClip;
                uIManager.audioSource.Play();
                uIManager.winScreen.SetActive(true);
                ProceedToVideo(winVideoClip);
                break;
            case "jobless_lose_ending":
                uIManager.scenarioController.canvas.SetActive(false);
                uIManager.audioSource.clip = uIManager.loseClip;
                uIManager.audioSource.Play();
                uIManager.loseJoblessScreen.SetActive(true);
                ProceedToVideo(loseVideoClip);
                break;
            default:
                base.PlayerAction(action, index);
                break;
        }
    }

    public override void DisplayChoices()
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
                    ChooseOption(capturedIndex);
                    ClearChoices();
                });
            }
        }
        base.DisplayChoices();
        if (scamshieldButton == null)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot();
                ClearChoices();
                Destroy(scamshieldButton);
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }

    protected override IEnumerator ReportToScamShield()
    {
        yield return base.ReportToScamShield();
        uIManager.scenarioController.canvas.SetActive(false);

        uIManager.audioSource.clip = uIManager.loseClip;
        uIManager.audioSource.Play();
        uIManager.mightNotBeScamScreen.SetActive(true);
        ProceedToVideo(loseVideoClip);
    }
}
