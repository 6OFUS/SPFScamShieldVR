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

    public override void PlayerAction(string action)
    {
        switch (action)
        {
            case "open_amail":
                amailUI.SetActive(true);
                Debug.Log("opening amail");
                break;
            default:
                base.PlayerAction(action); 
                break;
        }
    }

    void Start()
    {
        RandomiseScenario();

        //StartCoroutine(ContinueStory());
    }
}
