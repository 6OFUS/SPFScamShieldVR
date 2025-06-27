/*
    Author: Kevin Heng
    Date: 09/06/2025
    Description: The SendMessage class is used for the messaging system between sender and player
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagingSystem : MonoBehaviour
{
    /// <summary>
    /// The prefab used for sender message
    /// </summary>
    public GameObject senderMessagePrefab;

    public GameObject playerMessagePrefab;

    /// <summary>
    /// Content child under scroll view
    /// </summary>
    public Transform messagesContent;


    /// <summary>
    /// Sends the next message from the sender
    /// </summary>
    public void SenderNextMessage(string message)
    {

        GameObject newMessage = Instantiate(senderMessagePrefab, messagesContent);

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

    public void PlayerNextMessage(string message)
    {
        GameObject newMessage = Instantiate(playerMessagePrefab, messagesContent);

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
}
