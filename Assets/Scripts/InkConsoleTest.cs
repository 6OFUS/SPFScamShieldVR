using Ink.Runtime;
using TMPro;
using UnityEngine;

public class InkConsoleTest : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI playerChoice;
    private Story story;
    public TextAsset inkJson;

    void Start()
    {
        if (inkJson == null)
        {
            Debug.LogError("Ink JSON file not assigned!");
            return;
        }

        story = new Story(inkJson.text);
        //Debug.Log(story.Continue());
        dialogue.text = story.Continue();
        //PlayStory();
    }
    public void NextLine()
    {
        if (story.canContinue)
        {
            //Debug.Log(story.Continue());
            string dialogueTest = story.Continue();
            dialogue.text = dialogueTest;
            Debug.Log(dialogueTest);
        }
        else
        {
            if (story.currentChoices.Count > 0)
            {
                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    Debug.Log($"Choice {i}: '{story.currentChoices[i].text}'");
                }
            }
        }
    }
    public void ChooseOption(int index)
    {
        if (story.currentChoices.Count > index)
        {
            string selectedText = story.currentChoices[index].text;
            Debug.Log("Player selected: " + selectedText);

             
            playerChoice.text = selectedText;

            story.ChooseChoiceIndex(index);

            // Now continue the story and show the reply
            string reply = story.Continue().Trim();
            Debug.Log(reply);
            dialogue.text = reply;
            NextLine();
        }
    }

    void PlayStory()
    {
        while (story.canContinue)
        {
            string text = story.Continue().Trim();
            if (!string.IsNullOrEmpty(text))
                Debug.Log($"Narrative: '{text}'");
        }

        Debug.Log("Choices count: " + story.currentChoices.Count);

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Debug.Log($"Choice {i}: '{story.currentChoices[i].text}'");
            }

            story.ChooseChoiceIndex(0);
            PlayStory();
        }
        else
        {
            Debug.Log("End of story.");
        }
    }
}
