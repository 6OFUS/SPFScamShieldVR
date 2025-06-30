/*
    Author: Kevin Heng
    Date: 09/06/2025
    Description: The SendMessage class is used for the messaging system between sender and player
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagingSystem : MonoBehaviour
{
    /// <summary>
    /// The prefab used for sender message
    /// </summary>
    public GameObject senderMessagePrefab;

    public GameObject playerMessagePrefab;

    public GameObject senderImagePrefab;

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
    }

    public void SenderImage(Sprite image)
    {
        GameObject newImage = Instantiate(senderImagePrefab, messagesContent);
        Transform imageTransform = newImage.transform.Find("bg/Image");
        if (imageTransform == null)
        {
            Debug.LogError("Could not find 'background' in senderImagePrefab.");
            return;
        }

        Image imageComponent = imageTransform.GetComponentInChildren<Image>();

        if (imageComponent != null && image != null)
        {
            imageComponent.sprite = image;         // Set the "Source Image"
            imageComponent.SetNativeSize();         // Optional: match image size
        }
        else
        {
            Debug.LogError("Missing sprite or Image component.");
        }
    }
}
