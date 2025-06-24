/*
    Author: Kevin Heng
    Date: 05/06/2025
    Description: The JobScamManager class is used to handle the functions for job scam scenarios
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
    [SerializeField] private GameObject amailUI;
    [SerializeField] private GameObject websiteHomeUI;
    [SerializeField] private GameObject loadingScreenUI;
    [SerializeField] private GameObject loadingImageUI;
    [SerializeField] private GameObject websiteHomeLoggedInUI;


    [Header("Account creation")]
    [SerializeField] private bool accountRegistered;
    private TextMeshProUGUI currentInputFieldText;
    private Button currentChoiceButton;
    [SerializeField] private int inputCount;
    [SerializeField] private float loadingTime;


    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "open_amail":
                amailUI.SetActive(true);
                break;
            case "message_register_account":
                string selectedText = playerChoices[index].choiceName;
                sendMessage.PlayerNextMessage(selectedText);
                websiteHomeUI.SetActive(true);
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
            Debug.Log("registering");
            loadingScreenUI.SetActive(true);
            loadingImageUI.SetActive(true);
            yield return new WaitForSeconds(loadingTime);
            loadingScreenUI.SetActive(false);
            loadingImageUI.SetActive(false);
            websiteHomeLoggedInUI.SetActive(true);
        }
    }

    public void RegisterAccount()
    {
        StartCoroutine(RegisterAccountCoroutine());
    }
    void Start()
    {
        RandomiseScenario();

        //StartCoroutine(ContinueStory());
    }
}
