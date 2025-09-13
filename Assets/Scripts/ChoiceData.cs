/*
    Author: Kevin Heng
    Date: 09/06/2025
    Description: The ChoiceData class is used to handle the choices given to player
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceData
{
    /// <summary>
    /// Option choice index
    /// </summary>
    public int choiceIndex;
    /// <summary>
    /// Option choice name
    /// </summary>
    public string choiceName;
    /// <summary>
    /// Option choice action
    /// </summary>
    public string choiceAction;

    /// <summary>
    /// Store choice options data
    /// </summary>
    /// <param name="index">Choice index</param>
    /// <param name="fullText">Original choice naming in Ink</param>
    public ChoiceData(int index, string fullText)
    {
        choiceIndex = index;

        if (fullText.StartsWith("Player:"))
        {
            int spaceIndex = fullText.IndexOf(' ');
            if (spaceIndex > -1)
            {
                // Extract action by skipping the "Player:" part and slicing cleanly
                choiceAction = fullText.Substring("Player:".Length, spaceIndex - "Player:".Length);
                choiceName = fullText.Substring(spaceIndex + 1);
            }
            else
            {
                // If no space, default to treating the whole thing as the action
                choiceAction = fullText.Substring("Player:".Length);
                choiceName = "";
            }
        }
        else
        {
            choiceAction = "message"; // Default action
            choiceName = fullText;
        }
    }

}
