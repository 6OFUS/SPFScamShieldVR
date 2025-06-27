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
    public int inputCount;
    public float loadingTime;

    [Header("Website task")]
    [SerializeField] private bool firstTaskCompleted;
    [SerializeField] private int numItemsAdded;

    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "open_amail":
                uiManager.amailScreen.SetActive(true);
                break;
            case "message_register_account":
                string selectedText = playerChoices[index].choiceName;
                sendMessage.PlayerNextMessage(selectedText);
                uiManager.websiteHomeScreen.SetActive(true);
                break;
            case "message_complete_task":
                uiManager.websiteHomeLoggedInScreen.SetActive(true);
                uiManager.websiteReturnText.SetActive(false);
                uiManager.silverTierButton.SetActive(true);
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

    private IEnumerator RegisterAccountCoroutine()
    {
        if(inputCount == 4)
        {
            uiManager.loadingScreen.SetActive(true);
            yield return new WaitForSeconds(loadingTime);
            uiManager.websiteHomeLoggedInScreen.SetActive(true);

            uiManager.loadingScreen.SetActive(false);
            uiManager.websiteHomeScreen.SetActive(false);
            uiManager.whatsupScreen.SetActive(false);
            uiManager.websiteCreateAccountScreen.SetActive(false);

            knotName = "job_task_2_dialogue_1";
        }
        else
        {
            //ERROR AUDIO
        }
    }

    public void RegisterAccount()
    {
        StartCoroutine(RegisterAccountCoroutine());
    }

    public void FirstTaskGroup()
    {
        if (!firstTaskCompleted)
        {
            StartCoroutine(LoadFirstTaskGroup());
        }
    }

    private IEnumerator LoadFirstTaskGroup()
    {
        uiManager.websiteHomeLoggedInScreen.SetActive(false);
        uiManager.websiteSelectTaskScreen.SetActive(false);
        uiManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uiManager.loadingScreen.SetActive(false);
        uiManager.taskScreen.SetActive(true);
    }

    public void FollowingTaskGroups()
    {
        if (firstTaskCompleted)
        {
            StartCoroutine(LoadingError());
        }
    }

    private IEnumerator LoadingError()
    {
        uiManager.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime * 2);
        uiManager.loadingScreenReturnText.SetActive(true);
        uiManager.loadingScreenHomeButton.SetActive(true);
        knotName = "job_task_2_loading_error";
    }

    public void AddItemsToCart()
    {
        if(numItemsAdded < 3)
        {
            uiManager.itemNumUI[numItemsAdded].SetActive(true);
            numItemsAdded++;
        }
    }

    private IEnumerator LoadCheckOut()
    {
        uiManager.taskScreen.SetActive(false);
        uiManager.loadingBackToDashboardScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        uiManager.websiteHomeAfterFirstTaskScreen.SetActive(true);
        knotName = "job_task_2_dialogue_2";
    }

    public void CheckOut()
    {
        if(numItemsAdded == 3)
        {
            StartCoroutine(LoadCheckOut());
        }
    }

    void Start()
    {
        RandomiseScenario();
    }
}
