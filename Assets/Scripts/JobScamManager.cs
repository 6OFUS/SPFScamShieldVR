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

    public override void PlayerAction(string action, int index)
    {
        switch (action)
        {
            case "open_amail":
                amailUI.SetActive(true);
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
