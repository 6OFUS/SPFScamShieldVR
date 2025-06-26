/*
    Author: Kevin Heng
    Date: 05/06/2025
    Description: The JobScamManager class is used to handle all the functions related to the job scam scenarios
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using System;
using Ink.Parsed;

public class JobScamManager : InkManager
{
    public JobScamUIManager uiManager;

    private TextMeshProUGUI currentInputFieldText;
    private Button currentChoiceButton;

    [Header("Account creation")]
    public bool accountRegistered;
    public int inputCount;
    public float loadingTime;

    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "open_amail":
                uiManager.amailUI.SetActive(true);
                break;
            case "message_register_account":
                string selectedText = playerChoices[index].choiceName;
                sendMessage.PlayerNextMessage(selectedText);
                uiManager.websiteHomeUI.SetActive(true);
                break;
            case "Player:submit_scamshield":
                uiManager.whatsupReportUI.SetActive(true);
                break;
            default:
                base.PlayerAction(action, index); 
                break;
        }
    }

    public void SetCurrentInputField(TextMeshProUGUI inputField)
    {
        currentInputFieldText = inputField;
    }
    public void SetCurrentChoiceButton(Button choiceButton)
    {
        currentChoiceButton = choiceButton;
    }

    public void InputChoice(string inputName)
    {
        GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceContent);
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = inputName;

        buttonObj.GetComponent<Button>().onClick.AddListener(() => {
            if (currentChoiceButton != null)
            {
                currentChoiceButton.interactable = false;
            }
            currentInputFieldText.text = inputName;
            currentInputFieldText.color = Color.black;
            ClearChoices();
            inputCount++;
        });
    }

    public IEnumerator RegisterAccountCoroutine()
    {
        if(inputCount == 4)
        {
            uiManager.loadingScreenUI.SetActive(true);
            yield return new WaitForSeconds(loadingTime);
            uiManager.loadingScreenUI.SetActive(false);
            uiManager.websiteHomeLoggedInUI.SetActive(true);
            accountRegistered = true;
            knotName = "job_task_2_dialogue_1";
        }
    }

    public void RegisterAccount()
    {
        StartCoroutine(RegisterAccountCoroutine());
    }


    void Start()
    {
        RandomiseScenario();
    }
}
