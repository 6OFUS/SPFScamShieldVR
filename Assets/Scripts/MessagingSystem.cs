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
    /// Reference ScenarioController script
    /// </summary>
    public ScenarioController scenarioController;

    /// <summary>
    /// Prefab used for sender message
    /// </summary>
    [Header("Sender messages")]
    public GameObject senderMessagePrefab;
    /// <summary>
    /// Prefab used for sender image
    /// </summary>
    public GameObject senderImagePrefab;

    /// <summary>
    /// Prefab used for player message
    /// </summary>
    [Header("Player messages")]
    public GameObject playerMessagePrefab;
    /// <summary>
    /// Prefab used for player sticker
    /// </summary>
    public GameObject playerStickerPrefab;

    /// <summary>
    /// Audio source
    /// </summary>
    [Header("Audio")]
    public AudioSource audioSource;
    /// <summary>
    /// Message received audio
    /// </summary>
    public AudioClip messageReceived;
    /// <summary>
    /// Message sent audio
    /// </summary>
    public AudioClip messageSent;

    /// <summary>
    /// Sends the next message from the sender
    /// </summary>
    public void SenderNextMessage(string message)
    {
        GameObject newMessage = Instantiate(senderMessagePrefab, scenarioController.messageContentParent);

        TextMeshProUGUI textComponent = newMessage.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            //audioSource.Play();
            textComponent.text = message;
        }
        audioSource.clip = messageReceived;
        audioSource.Play();
    }
    /// <summary>
    /// Sends the image from the sender
    /// </summary>
    /// <param name="image"></param>
    public void SenderImage(Sprite image)
    {
        GameObject newImage = Instantiate(senderImagePrefab, scenarioController.messageContentParent);
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
        audioSource.clip = messageReceived;
        audioSource.Play();
    }
    /// <summary>
    /// Sends the message selected by player
    /// </summary>
    /// <param name="message"></param>
    public void PlayerNextMessage(string message)
    {
        GameObject newMessage = Instantiate(playerMessagePrefab, scenarioController.messageContentParent);

        TextMeshProUGUI textComponent = newMessage.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            //audioSource.Play();
            textComponent.text = message;
        }
        audioSource.clip = messageSent;
        audioSource.Play();
    }

    /// <summary>
    /// Sends the sticker selected by player
    /// </summary>
    /// <param name="image"></param>
    public void PlayerSendSticker(Sprite image)
    {
        GameObject newSticker = Instantiate(playerStickerPrefab, scenarioController.messageContentParent);
        Transform imageTransform = newSticker.transform.Find("bg/sticker");
        if (imageTransform == null)
        {
            Debug.LogError("Could not find 'background' in senderImagePrefab.");
            return;
        }

        Image imageComponent = imageTransform.GetComponentInChildren<Image>();

        if (imageComponent != null && newSticker != null)
        {
            imageComponent.sprite = image;
        }
        else
        {
            Debug.LogError("Missing sprite or Image component.");
        }
        audioSource.clip = messageSent;
        audioSource.Play();
    }
}
