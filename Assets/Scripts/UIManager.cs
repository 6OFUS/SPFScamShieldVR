/*
    Author: Kevin Heng
    Date: 23/06/2025
    Description: The UIManager class is used to manage UI related functions
*/
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool screenshotTaken;

    public Image flashImage; 
    public float flashDuration;

    [Header("UI")]
    public GameObject scamshieldScreen;
    public GameObject whatsupHomeButton;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip screenshotClip;

    public void Screenshot()
    {
        if (!screenshotTaken)
        {
            Debug.Log("screenshot taken");
            screenshotTaken = true;
            audioSource.clip = screenshotClip;
            audioSource.Play();
            flashImage.gameObject.SetActive(true);
            StartCoroutine(FlashEffect());
        }
    }

    public void OpenScamShieldApp()
    {
        if (screenshotTaken)
        {
            scamshieldScreen.SetActive(true);
        }
    }

    private IEnumerator FlashEffect()
    {
        // Step 1: Fade in
        float elapsed = 0f;
        while (elapsed < flashDuration / 2)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / (flashDuration / 2));
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // Step 2: Fade out
        elapsed = 0f;
        while (elapsed < flashDuration / 2)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / (flashDuration / 2));
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        flashImage.color = new Color(1, 1, 1, 0);
        flashImage.gameObject.SetActive(false);
        whatsupHomeButton.SetActive(true);
    }
}
