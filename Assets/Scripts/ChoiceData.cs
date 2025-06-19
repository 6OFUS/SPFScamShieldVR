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
    public int choiceIndex;
    public string choiceName;
    public string choiceAction;

    public ChoiceData(int index, string fullText)
    {
        choiceIndex = index;

        if (fullText.StartsWith("Player:"))
        {
            int spaceIndex = fullText.IndexOf(' ');
            if (spaceIndex > -1)
            {
                choiceAction = fullText.Substring(0, spaceIndex).Replace("Player:", "");
                choiceName = fullText.Substring(spaceIndex + 1); 
            }
            else
            {
                choiceAction = "";
                choiceName = fullText;
            }
        }
    }
}
