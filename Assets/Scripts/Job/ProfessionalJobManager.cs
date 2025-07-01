using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfessionalJobManager : InkManager
{
    public ProfessionalJobUIManager uIManager;

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
                knotName = "return_from_website";
                break;
            case "open_amail":
                uIManager.amailScreen.SetActive(true);
                knotName = "return_from_email";
                break;
            case "win_ending":
                uIManager.winScreen.SetActive(true);
                break;
            case "jobless_lose_ending":
                uIManager.loseJoblessScreen.SetActive(true);
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
        choiceContainer.gameObject.SetActive(true);
        uIManager.mightNotBeScamScreen.SetActive(true);
        GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceContainer);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Proceed to video recap";

        buttonObj.GetComponent<Button>().onClick.AddListener(() =>
        {
            ClearChoices();
        });
    }
}
