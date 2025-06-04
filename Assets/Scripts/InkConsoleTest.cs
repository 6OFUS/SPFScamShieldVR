using Ink.Runtime;
using UnityEngine;

public class InkConsoleTest : MonoBehaviour
{
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
        story.ChoosePathString("start");
        PlayStory();
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

            int choiceIndex = 1;  // try to pick choice 1

            if (choiceIndex >= story.currentChoices.Count)
            {
                Debug.LogWarning($"Choice index {choiceIndex} out of range. Picking choice 0 instead.");
                choiceIndex = 0;  // fallback to choice 0 if choice 1 not available
            }
            else
            {
                Debug.Log($"Picking choice {choiceIndex}");
            }

            story.ChooseChoiceIndex(choiceIndex);
            PlayStory();
        }
        else
        {
            Debug.Log("End of story.");
        }
    }
}
