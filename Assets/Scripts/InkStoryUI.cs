using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InkStoryUI : MonoBehaviour
{
    public TextMeshProUGUI storyText; // narrative/guide text UI
    public GameObject choicesPanel;   // panel to hold choice buttons
    public Button choiceButtonPrefab; // prefab for each choice button

    private Story story;

    void Start()
    {
        // Load your Ink JSON file (make sure you generated it)
        TextAsset inkJSON = Resources.Load<TextAsset>("YourInkFileName");
        story = new Story(inkJSON.text);

        ContinueStory();
    }

    void ContinueStory()
    {
        // Clear previous choices UI
        foreach (Transform child in choicesPanel.transform)
            Destroy(child.gameObject);

        // Continue Ink story until it needs input or ends
        while (story.canContinue)
        {
            string text = story.Continue().Trim();
            storyText.text = text; // show guide/narrative text
        }

        // Show choices
        if (story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                Button button = Instantiate(choiceButtonPrefab, choicesPanel.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text.Trim();

                button.onClick.AddListener(() =>
                {
                    OnClickChoice(choice);
                });
            }
        }
        else
        {
            // No choices = story ended
            storyText.text += "\n\n<color=yellow>THE END</color>";
        }
    }

    void OnClickChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        ContinueStory();
    }
}
