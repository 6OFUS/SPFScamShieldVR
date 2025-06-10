using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    /// <summary>
    /// The prefab used to display messages in the UI
    /// </summary>
    public GameObject senderMessagePrefab;

    /// <summary>
    /// The parent transform in which the messages will be instantiated (usually a scroll view)
    /// </summary>
    public Transform messagesScrollView;


    /// <summary>
    /// Sends the next message from the messages array to the player.
    /// </summary>
    public void SenderNextMessage(string message)
    {

        GameObject newMessage = Instantiate(senderMessagePrefab, messagesScrollView);

        TextMeshProUGUI textComponent = newMessage.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            //audioSource.Play();
            textComponent.text = message;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in messagePrefab!");
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
