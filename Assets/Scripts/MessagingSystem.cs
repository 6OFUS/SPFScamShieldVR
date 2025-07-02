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
    public ScenarioController scenarioController;

    [Header("Sender messages")]
    /// <summary>
    /// The prefab used for sender message
    /// </summary>
    public GameObject senderMessagePrefab;

    public GameObject senderImagePrefab;

    [Header("Player messages")]
    public GameObject playerMessagePrefab;
    public GameObject playerStickerPrefab;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip messageReceived;
    public AudioClip messageSent;



    /// <summary>
    /// Sends the next message from the sender
    /// </summary>
    public void SenderNextMessage(string message)
    {
        GameObject newMessage = Instantiate(senderMessagePrefab, scenarioController.whatsupContent);

        TextMeshProUGUI textComponent = newMessage.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            //audioSource.Play();
            textComponent.text = message;
        }
        audioSource.clip = messageReceived;
        audioSource.Play();
    }
    public void SenderImage(Sprite image)
    {
        GameObject newImage = Instantiate(senderImagePrefab, scenarioController.whatsupContent);
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

    public void PlayerNextMessage(string message)
    {
        GameObject newMessage = Instantiate(playerMessagePrefab, scenarioController.whatsupContent);

        TextMeshProUGUI textComponent = newMessage.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            //audioSource.Play();
            textComponent.text = message;
        }
        audioSource.clip = messageSent;
        audioSource.Play();
    }

    public void PlayerSendSticker(Sprite image)
    {
        GameObject newSticker = Instantiate(playerStickerPrefab, scenarioController.whatsupContent);
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
