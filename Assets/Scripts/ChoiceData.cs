using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceData
{
    public int choiceIndex;
    public string choiceName;

    public ChoiceData(int index, string name)
    {
        this.choiceIndex = index;
        this.choiceName = name;
    }
}
