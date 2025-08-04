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

    public Transform whatShouldYouDoButtonPos;

    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "action_tap_notification":
                uIManager.whatsupScreen.SetActive(true);
                StartCoroutine(WaitForReply(0));
                break;
            case "sticker":
                messagingSystem.PlayerSendSticker(uIManager.stickers[index]);
                StartCoroutine(WaitForReply(messageTime));
                break;
            case "action_check_website":
                uIManager.websiteHomeScreen.SetActive(true);
                StartCoroutine(WaitForReply(5));
                break;
            case "action_check_website_careers":
                uIManager.websiteCareersScreen.SetActive(true);
                StartCoroutine(SpawnHomeButton(5));
                break;
            case "action_open_amail":
                uIManager.amailScreen.SetActive(true);
                StartCoroutine(WaitForReply(1));
                break;
            case "action_open_lucia_email":
                uIManager.luciaEmailScreen.SetActive(true);
                StartCoroutine(SpawnHomeButton(5));
                break;
            case "win_ending":
                StartCoroutine(HandleWinEnding());
                break;
            case "lose_ending":
                StartCoroutine(HandleLoseEnding());
                break;
            default:
                base.PlayerAction(action, index);
                break;
        }
    }

    private IEnumerator SpawnHomeButton(float time)
    {
        yield return SpawnActionButton("Go to home screen", time, () => {
            isOnHomeScreen = true;
            uIManager.DisableAllCanvasChildren();
            uIManager.homeScreen.SetActive(true);
            if (scamshieldButton != null)
            {
                Destroy(scamshieldButton);
            }
            if (uIManager.screenshotTaken)
            {
                StartCoroutine(SpawnOpenScamshieldButton());
            }
            else
            {
                StartCoroutine(SpawnOpenWhatsUpButton(1));
            }
        });
    }

    private IEnumerator SpawnOpenWhatsUpButton(float time)
    {
        yield return SpawnActionButton("Open WhatsUp app", time, () => {
            uIManager.whatsupScreen.SetActive(true);
            StartCoroutine(WaitForReply(0));
            isOnHomeScreen = false;
        });
    }

    private IEnumerator SpawnOpenScamshieldButton()
    {
        yield return SpawnActionButton("Open Scamshield app", 1f, () => {
            uIManager.scamshieldScreen.SetActive(true);
            isOnHomeScreen = false;
            StartCoroutine(SpawnReportButton());
        });
    }

    private IEnumerator SpawnReportButton()
    {
        yield return SpawnActionButton("Report", 1f, () => {
            Report();
        });
    }


    private IEnumerator HandleLoseEnding()
    {
        ClearChoices();
        Destroy(scamshieldButton);
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.audioSource.clip = uIManager.loseClip;
        uIManager.audioSource.Play();
        uIManager.loseScreen.SetActive(true);
        yield return new WaitForSeconds(uIManager.loseClip.length);
        uIManager.whatHappenButton.gameObject.SetActive(false);
        uIManager.whatShouldYouDoButton.transform.position = whatShouldYouDoButtonPos.position;
        ProceedToVideo(gameOverVideoClip);
    }
    private IEnumerator HandleWinEnding()
    {
        ClearChoices();
        Destroy(scamshieldButton);
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.audioSource.clip = uIManager.winClip;
        uIManager.audioSource.Play();
        uIManager.winScreen.SetActive(true);
        yield return new WaitForSeconds(uIManager.winClip.length);
        uIManager.whatHappenButton.onClick.AddListener(() =>
        {
            recapVideoScript.PlayVideo(whatHappenWinVideoClip);
        });
        ProceedToVideo(winVideoClip);
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
        if (scamshieldButton == null && !isOnHomeScreen)
        {
            scamshieldButton = Instantiate(scamshieldChoiceButtonPrefab, choiceContainer);
            scamshieldButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                uIManager.Screenshot();
                ClearChoices();
                Destroy(scamshieldButton);
                StartCoroutine(SpawnHomeButton(1));
            });
        }
        scamshieldButton.transform.SetAsLastSibling();
    }

    protected override IEnumerator ReportToScamShield()
    {
        uIManager.scamshieldLoadingScreen.SetActive(true);
        yield return base.ReportToScamShield();
        uIManager.scenarioController.scenarioCanvas.SetActive(false);
        uIManager.audioSource.clip = uIManager.loseClip;
        uIManager.audioSource.Play();
        uIManager.loseScreen.SetActive(true);
        yield return new WaitForSeconds(uIManager.loseClip.length);
        uIManager.whatHappenButton.gameObject.SetActive(false);
        uIManager.whatShouldYouDoButton.transform.position = whatShouldYouDoButtonPos.position;
        ProceedToVideo(gameOverVideoClip);
    }
}
