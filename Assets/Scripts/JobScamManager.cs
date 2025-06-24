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

public class JobScamManager : InkManager
{
    [SerializeField] private GameObject amailUI;
    [SerializeField] private GameObject websiteHomeUI;

    [SerializeField] private bool accountRegistered;

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

    void Start()
    {
        RandomiseScenario();

        //StartCoroutine(ContinueStory());
    }
}
